using Player.FishStorage;
using UI.ResourceCounters;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class FishStorageInstaller : MonoInstaller
    {
        private FishStorage _fishStorage;

        [SerializeField] private FishCountView fishCountView;
        
        public override void InstallBindings()
        {
            _fishStorage = new FishStorage(fishCountView);
            
            Container.Bind<FishStorage>().FromInstance(_fishStorage).AsSingle().NonLazy();
        }
    }
}