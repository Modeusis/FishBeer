using System;
using System.Collections;
using Animations;
using Player;
using Player.Camera;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace GameProcess.Interactions
{
    [RequireComponent(typeof(DrinkableBottleAnimationHandle))]
    public class DrinkableBeerBottle : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        [Inject] private ResourceManager _resourceManager;

        private bool _isDrinking;
        
        private DrinkableBottleAnimationHandle _animationHandle;
        
        private CameraUnblocker _cameraUnblocker;

        private void Start()
        {
            _animationHandle = GetComponent<DrinkableBottleAnimationHandle>();
            
            _cameraUnblocker = new CameraUnblocker();
        }

        public void Interact()
        {
            if (!ValidateInteraction())
                return;
            
            _isDrinking = true;
            
            _animationHandle.StartDrinking();
            
            _eventBus.Publish(InteractionType.Drinking);
        }

        public void Focus()
        {
            Debug.Log("DrinkableBeerBottle in focus");
        }

        public void Unfocus()
        {
            Debug.Log("DrinkableBeerBottle unfocused");
        }

        private bool ValidateInteraction()
        {
            if (_resourceManager.Beer == 0 || _isDrinking)
                return false;
                
            return true;
        }

        public void FinishDrinking()
        {
            _resourceManager.DrinkBeer();
            
            _isDrinking = false;
            
            _eventBus.Publish(_cameraUnblocker);
            
            Destroy(gameObject);
        }
    }
}