using System.Collections.Generic;
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

        private List<Transform> _fishBucketChilds;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _fishBucketChilds = new List<Transform>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                _fishBucketChilds.Add(transform.GetChild(i));
            }
        }
        
        public void Interact()
        {
            ToggleChildFocus(false);
            
            _animator.SetBool("Focused", false);
            
            _eventBus.Publish(InteractionType.FishBaitShopping);
        }

        public void Focus()
        {
            ToggleChildFocus(true);
            
            _animator.SetBool("Focused", true);
        }

        public void Unfocus()
        {
            ToggleChildFocus(false);
            
            _animator.SetBool("Focused", false);
        }
        
        private void ToggleChildFocus(bool focus)
        {
            foreach (var child in _fishBucketChilds)
            {
                child.gameObject.layer = focus ? LayerMask.NameToLayer("Interactable") : LayerMask.NameToLayer("Default");
            }
        }
    }
}