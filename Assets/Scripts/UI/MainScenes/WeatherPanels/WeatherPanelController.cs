using Cysharp.Threading.Tasks;
using DataSenders.Managers;
using DataSenders.Requests.Imps;
using Models.ServerAnswers.Weathers;
using Models.SO.NetworkSettings;
using System;
using System.Threading;
using UI.Abstractions.Controllers;
using UniRx;
using UnityEngine;
using Utils.AsyncExtensions;

namespace UI.MainScenes.WeatherPanels
{
    public class WeatherPanelController : UiController<WeatherPanelView>, IDisposable
    {
        private readonly IRequestsManager _requestsManager;
        private readonly int _getDataRepeatDelay;

        private CancellationTokenSource _repeatGetDataCancellationTokenSource;
        private CancellationTokenSource _cancellationTokenRequest;
        private IDisposable _disposable;

        public WeatherPanelController(IRequestsManager requestsManager, INetworkSettingSo networkSettingSo)
        {
            _requestsManager = requestsManager;
            _getDataRepeatDelay = Mathf.RoundToInt(networkSettingSo.WeatherRepeateDelay * 1000);
        }

        public override void Show()
        {
            _repeatGetDataCancellationTokenSource = new();
            SendingRequests(_repeatGetDataCancellationTokenSource.Token).Forget();
            base.Show();
        }

        public override void Hide()
        {
            ClearRequest();
            CancelMainToken();
            base.Hide();
        }

        private async UniTaskVoid SendingRequests(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                ClearRequest();

                _cancellationTokenRequest = CancellationTokenSource
                    .CreateLinkedTokenSource(token);

                var weatherRequest = new GetWeatherRequest(_cancellationTokenRequest);
                _requestsManager.AddRequest(weatherRequest);
                _disposable = weatherRequest.OnRequestDone.Subscribe(OnLoadWeatherData);

                await UniTask.Delay(_getDataRepeatDelay, cancellationToken: token);
            }
        }

        private void OnLoadWeatherData(WeatherServerAnswer answer)
        {
            if (answer.Properties.Periods.Count == 0)
            {
                UnityEngine.Debug.LogError("Not found periods.");
                return;
            }
            var period = answer.Properties.Periods[0];
            View.WeatherText.text = $"{period.Name} {period.Temperature} {period.TemperatureUnit}";
            GetWeatherIcon(period.Icon);
        }

        private void GetWeatherIcon(string iconUrl)
        {
            ClearRequest();
            _cancellationTokenRequest = CancellationTokenSource
                .CreateLinkedTokenSource(_repeatGetDataCancellationTokenSource.Token);

            var iconRequest = new GetRemoteSpriteRequest(iconUrl, _cancellationTokenRequest);
            _requestsManager.AddRequest(iconRequest);
            _disposable = iconRequest.OnRequestDone.Subscribe(SetWeatherIcon);
        }

        private void SetWeatherIcon(Sprite icon)
        {
            View.WeatherImage.sprite = icon;
        }

        private void ClearRequest()
        {
            if (_disposable != null)
            {
                _disposable.Dispose();
                _disposable = null;
            }
            _cancellationTokenRequest.TryCancel();
        }

        private void CancelMainToken()
        {
            _repeatGetDataCancellationTokenSource.TryCancel();
        }

        public void Dispose()
        {
            ClearRequest();
            CancelMainToken();
        }
    }
}