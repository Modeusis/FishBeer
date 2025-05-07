using System.Resources;
using GameProcess.Interactions;
using Player.Camera;
using Player.FishStorage;
using Sounds;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace UI.Pages.FishSaleTable
{
    public class FishSalePage : Page
    {
        private EventBus _eventBus;
        
        private FishStorage _fishStorage;
        
        private ResourceManager _resourceManager;
        
        private CameraUnblocker _cameraUnblocker;

        private GameObject _itemPrefab;
        
        [Inject]
        private void Initialize(EventBus eventBus, FishStorage fishStorage, ResourceManager resourceManager)
        {
            _fishStorage = fishStorage;
            
            _resourceManager = resourceManager;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<InteractionType>(HandleInteraction);
            
            _cameraUnblocker = new CameraUnblocker();
        }

        private void HandleInteraction(InteractionType interaction)
        {
            if (interaction != InteractionType.FishSaling)
                return;
            
            Open();   
        }

        private void ClosePage()
        {
            _eventBus.Publish(_cameraUnblocker);
            
            Close();
        }

        private void CalculateTotalPrice()
        {
            
        }

        private void GenerateViewTable()
        {
            
        }
        
        private void Sale()
        {
            
        }
    }
}