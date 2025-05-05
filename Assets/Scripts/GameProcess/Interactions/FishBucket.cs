using UI.Pages;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace GameProcess.Interactions
{
    [RequireComponent(typeof(Animator))]
    public class FishBucket : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void Interact()
        {
            _eventBus.Publish(InteractionType.FishSaling);
        }

        public void Focus()
        {
            _animator.SetBool("Focused", true);
        }

        public void Unfocus()
        {
            _animator.SetBool("Focused", false);
        }
    }
}