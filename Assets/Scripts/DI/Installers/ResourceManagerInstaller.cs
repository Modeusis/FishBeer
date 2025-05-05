using Player;
using UI.ResourceCounters;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class ResourceManagerInstaller : MonoInstaller
    {
        private ResourceManager _resourceManager;

        [Header("Starting values")]
        [SerializeField] private int fishBaitsAmount = 2;
        [SerializeField] private int beerAmount = 2;
        [SerializeField] private float moneyAmount = 100f;
        
        [Header("Views")]
        [SerializeField] private MoneyView moneyView;
        [SerializeField] private BeerView beerView;
        [SerializeField] private FishBaitsView fishBaitsView;
        
        public override void InstallBindings()
        {
            _resourceManager = new ResourceManager(fishBaitsAmount, beerAmount, moneyAmount,
                moneyView, fishBaitsView, beerView);
            
            Container.Bind<ResourceManager>().FromInstance(_resourceManager).AsSingle().NonLazy();
        }
    }
}