using System;
using System.Collections;
using System.Collections.Generic;
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

        private List<Transform> _bottleChilds;
        
        private void Start()
        {
            _animationHandle = GetComponent<DrinkableBottleAnimationHandle>();
            
            _cameraUnblocker = new CameraUnblocker();
            
            _bottleChilds = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                _bottleChilds.Add(transform.GetChild(i));
            }
        }

        public void Interact()
        {
            if (!ValidateInteraction())
                return;
            
            _isDrinking = true;
            
            ToggleChildFocus(false);
            
            _animationHandle.StartDrinking();
            
            _eventBus.Publish(InteractionType.Drinking);
        }

        public void Focus()
        {
            ToggleChildFocus(true);
            
            _animationHandle.Focus();
        }

        public void Unfocus()
        {
            ToggleChildFocus(false);
            
            _animationHandle.Unfocus();
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

        private void ToggleChildFocus(bool focus)
        {
            foreach (var child in _bottleChilds)
            {
                child.gameObject.layer = focus ? LayerMask.NameToLayer("Interactable") : LayerMask.NameToLayer("Default");
            }
        }
    }
}