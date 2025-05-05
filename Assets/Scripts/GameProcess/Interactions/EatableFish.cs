using System;
using Animations;
using Player.Camera;
using Player.FishStorage;
using UI.Cursor;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace GameProcess.Interactions
{
    [RequireComponent(typeof(EatableFishAnimationHandler))]
    public class EatableFish : MonoBehaviour, IInteractable
    {
        [Inject] private FishStorage _fishStorage;
        [Inject] private EventBus _eventBus;

        private EatableFishAnimationHandler _handler;
        
        private void Start()
        {
            _handler = GetComponent<EatableFishAnimationHandler>();
        }

        public void Interact()
        {
            _eventBus.Publish(InteractionType.Eating);
            
            _fishStorage.EatFish();
            
            _eventBus.Publish(CursorType.Idle);
            
            gameObject.layer = LayerMask.NameToLayer("Default");
            
            _handler.StartEating();
        }

        public void Focus()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            
            _eventBus.Publish(CursorType.Click);
            
            _handler.Focus();
        }

        public void Unfocus()
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            
            _eventBus.Publish(CursorType.Idle);
            
            _handler.Unfocus();
        }

        public void HandleFinishEating()
        {
            _eventBus.Publish(new CameraUnblocker());
            
            Destroy(gameObject);
        }
    }
}