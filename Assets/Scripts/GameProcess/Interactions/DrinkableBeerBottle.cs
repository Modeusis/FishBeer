using System;
using System.Collections;
using System.Collections.Generic;
using Animations;
using Player;
using Player.Camera;
using UI.Bars;
using UI.Cursor;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities.EventBus;
using Zenject;

namespace GameProcess.Interactions
{
    public class DrinkableBeerBottle : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        [Inject] private ResourceManager _resourceManager;

        private bool _isDrinking;
        
        public DrinkableBottleAnimationHandle animationHandle;
        
        private CameraUnblocker _cameraUnblocker = new CameraUnblocker();

        private List<Transform> _bottleChilds;
        
        public void OnSpawnBottle()
        {
            _bottleChilds = new List<Transform>();

            for (int i = 0; i < animationHandle.transform.childCount; i++)
            {
                _bottleChilds.Add(animationHandle.transform.GetChild(i));
            }
        }

        public void Interact()
        {
            if (animationHandle == null)
                return;
            
            if (!ValidateInteraction())
                return;
            
            _isDrinking = true;
            
            ToggleChildFocus(false);
            
            animationHandle.StartDrinking();
            
            _eventBus.Publish(CursorType.Idle);
            
            _eventBus.Publish(InteractionType.Drinking);
        }

        public void Focus()
        {
            if (animationHandle == null)
                return;
            
            ToggleChildFocus(true);
            
            _eventBus.Publish(CursorType.Click);
            
            animationHandle.Focus();
        }

        public void Unfocus()
        {
            if (animationHandle == null)
                return;
            
            ToggleChildFocus(false);
            
            _eventBus.Publish(CursorType.Idle);
            
            animationHandle.Unfocus();
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
            _eventBus.Publish(PlayerActionType.BeerDrink);
            
            Destroy(animationHandle.gameObject);
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