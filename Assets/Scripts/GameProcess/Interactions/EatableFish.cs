using System;
using System.Collections.Generic;
using Animations;
using Player.Camera;
using Player.FishStorage;
using UI.Cursor;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities.EventBus;
using Zenject;

namespace GameProcess.Interactions
{
    public class EatableFish : MonoBehaviour, IInteractable
    {
        [Inject] private FishStorage _fishStorage;
        [Inject] private EventBus _eventBus;

        public EatableFishAnimationHandler animationHandler;

        public void Interact()
        {
            if (animationHandler == null)
                return;
            
            _eventBus.Publish(InteractionType.Eating);
            
            _fishStorage.EatFish();
            
            _eventBus.Publish(CursorType.Idle);
            
            animationHandler.gameObject.layer = LayerMask.NameToLayer("Default");
            
            animationHandler.StartEating();
        }

        public void Focus()
        {
            if (animationHandler == null)
                return;
            
            animationHandler.gameObject.layer = LayerMask.NameToLayer("Interactable");
            
            _eventBus.Publish(CursorType.Click);
            
            animationHandler.Focus();
        }

        public void Unfocus()
        {
            if (animationHandler == null)
                return;
            
            animationHandler.gameObject.layer = LayerMask.NameToLayer("Default");
            
            _eventBus.Publish(CursorType.Idle);
            
            animationHandler.Unfocus();
        }

        public void HandleFinishEating()
        {
            _eventBus.Publish(new CameraUnblocker());
            
            Destroy(gameObject);
        }
    }
}