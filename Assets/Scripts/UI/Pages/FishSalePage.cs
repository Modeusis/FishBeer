using GameProcess.Interactions;
using Player.Camera;
using Sounds;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace UI.Pages
{
    public class FishSalePage : Page
    {
        private EventBus _eventBus;
        
        private CameraUnblocker _cameraUnblocker;
        
        [Inject]
        private void Initialize(EventBus eventBus)
        {
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
    }
}