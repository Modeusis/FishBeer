using Player;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class ResourceManagerInstaller : MonoInstaller
    {
        private ResourceManager _resourceManager;

        [SerializeField] private int fishBaitsAmount = 2;
        [SerializeField] private int beerAmount = 2;
        
        [SerializeField] private float moneyAmount = 100f;
        
        public override void InstallBindings()
        {
            _resourceManager = new ResourceManager(fishBaitsAmount, beerAmount, moneyAmount);
            
            Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle().NonLazy();
        }
    }
}