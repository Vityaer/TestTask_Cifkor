using Assets.Scripts.Common;
using Cysharp.Threading.Tasks;
using DataSenders.Requests.Interfaces;
using DataSenders.Senders;
using System;
using System.Collections.Generic;
using System.Threading;
using Utils.AsyncExtensions;

namespace DataSenders.Managers
{
    public class RequestsManager : IRequestsManager, IDisposable
    {
        private readonly IRequestSender _requestSender;

        private readonly Queue<IBaseRequestCommand> _requests = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private bool _inProggress;

        public RequestsManager(IRequestSender requestSender)
        {
            _requestSender = requestSender;
        }

        public void AddRequest(IBaseRequestCommand request)
        {
            _requests.Enqueue(request);

            if (!_inProggress)
                ExecuteRequests();
        }

        private void ExecuteRequests()
        {
            if (_requests.Count == 0)
                return;

            SendRequests(_cancellationTokenSource.Token).Forget();
        }


        private async UniTaskVoid SendRequests(CancellationToken token)
        {
            _inProggress = true;

            while (_requests.Count > 0)
            {
                var request = _requests.Dequeue();
                if (request.CancellationTokenSource.IsCancellationRequested)
                    continue;

                var requestToken = request.CancellationTokenSource.Token;

                await Execute(request, requestToken).SuppressCancellationThrow();

                _inProggress = false;
            }
        }

        private async UniTask Execute(IBaseRequestCommand request, CancellationToken token)
        {
#if UNITY_EDITOR
            //Delay for testing on cancel operations.
            var delay = UnityEngine.Mathf.RoundToInt(ProjectConstants.Debug.DelayBeforeSendRequest * 1000);
            await UniTask.Delay(delay, cancellationToken: token);
#endif
            await request.Execute(_requestSender, token);
        }

        public void Dispose()
        {
            _cancellationTokenSource.TryCancel();
        }
    }
}
