using Models.SO.NetworkSettings;
using Models.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;

namespace Assets.Scripts.Infrastructures
{
    [CreateAssetMenu(fileName = nameof(ProjectSettingsInstaller), menuName = "Custom Installers/" +  nameof(ProjectSettingsInstaller))]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
    {
        [SerializeField] private MenuButtonsDataSo _menuButtonsDataSo;
        [SerializeField] private NetworkSettingsSo _networkSettingsSo;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<NetworkSettingsSo>().FromInstance(_networkSettingsSo).AsSingle();
            Container.BindInterfacesTo<MenuButtonsDataSo>().FromInstance(_menuButtonsDataSo).AsSingle();
        }
    }
}
