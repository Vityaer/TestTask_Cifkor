using Common;
using DataSenders;
using Zenject;

namespace Infrastructures
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<RequestSender>().AsSingle();
            Container.BindInterfacesTo<RequestsManager>().AsSingle();
        }
    }
}
