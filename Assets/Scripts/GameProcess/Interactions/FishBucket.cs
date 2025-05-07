using System.Collections.Generic;
using UI.Pages;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities.EventBus;
using Zenject;

namespace GameProcess.Interactions
{
    public class FishBucket : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        
        [SerializeField] private Animator animator;

        private List<Transform> _fishBucketChilds;
        
        private void Awake()
        {
            _fishBucketChilds = new List<Transform>();
            
            for (int i = 0; i < animator.transform.childCount; i++)
            {
                _fishBucketChilds.Add(animator.transform.GetChild(i));
            }
        }
        
        public void Interact()
        {
            ToggleChildFocus(false);
            
            animator.SetBool("Focused", false);
            
            _eventBus.Publish(InteractionType.FishBaitShopping);
        }

        public void Focus()
        {
            ToggleChildFocus(true);
            
            animator.SetBool("Focused", true);
        }

        public void Unfocus()
        {
            ToggleChildFocus(false);
            
            animator.SetBool("Focused", false);
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