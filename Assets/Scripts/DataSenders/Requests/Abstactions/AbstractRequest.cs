using Assets.Scripts.Common;
using Cysharp.Threading.Tasks;
using DataSenders.Messages.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading;
using UniRx;

namespace DataSenders.Requests.Abstactions
{
    public abstract class AbstractRequest<T> : IRequestCommand<T>
    {
        protected readonly ReactiveCommand<T> OnResultDone = new();
        protected CancellationTokenSource IternalCancellationTokenSource;

        protected abstract string Route { get; }
        public IObservable<T> OnRequestDone => OnResultDone;
        public CancellationTokenSource CancellationTokenSource => IternalCancellationTokenSource;

        public virtual async UniTask Execute(IRequestSender requestSender, CancellationToken token)
        {
            var result = await requestSender.GetData(Route, token);
            DeserializeAnswer(result);
        }

        protected void DeserializeAnswer(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                UnityEngine.Debug.LogError($"Request {GetType()} on route {Route} complete with error.");
                return;
            }

            var answer = JsonConvert.DeserializeObject<T>(json, ProjectConstants.Common.SerializerSettings);
            if (answer == null)
            {
                UnityEngine.Debug.LogError($"Failed to deserialize object");
                return;
            }
            OnResultDone.Execute(answer);
        }
    }
}
