using Player.FishStorage;
using Zenject;

namespace DI.Installers
{
    public class FishStorageInstaller : MonoInstaller
    {
        private FishStorage _fishStorage;

        public override void InstallBindings()
        {
            _fishStorage = new FishStorage();
            
            Container.Bind<FishStorage>().FromInstance(_fishStorage).AsSingle().NonLazy();
        }
    }
}