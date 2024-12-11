using MainScenes.FactPanels.FactDataPanels;
using UI.Extentions;
using UI.MainScenes.FactPanels;
using UI.MainScenes.FactPanels.FactDataPanels;
using UI.MainScenes.FactPanels.MainPanels;
using UI.MainScenes.MainMunuPanels.MenuButtons;
using UI.MainScenes.WeatherPanels;
using UI.MainScenes.Windows;
using UnityEngine;
using Zenject;

namespace Infrastructures
{
    public class MainSceneUiInstaller : MonoInstaller
    {
        [field: SerializeField] private Canvas _canvas;

        [Header("Panels")]
        [SerializeField] private WeatherPanelView _weatherPanelView;
        [SerializeField] private FactsPanelView _factsPanelView;
        [SerializeField] private FactDataPanelView _factDataPanelView;
        [SerializeField] private MenuButtonsView _menuButtonsView;

        public override void InstallBindings()
        {
            var canvas = Instantiate(_canvas);
            BindPanels(canvas);
            BindWindows();
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesTo<MenuHudController>().AsSingle();
        }

        private void BindPanels(Canvas canvas)
        {
            Container.BindUiView<WeatherPanelController, WeatherPanelView>(_weatherPanelView, canvas.transform);

            Container.BindUiView<FactsPanelController, FactsPanelView>(_factsPanelView, canvas.transform);
            Container.BindUiView<FactDataPanelController, FactDataPanelView>(_factDataPanelView, canvas.transform);
            
            Container.BindUiView<MenuButtonsController, MenuButtonsView>(_menuButtonsView, canvas.transform, true);
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<WeatherWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<FactWindow>().AsSingle();
        }
    }
}
