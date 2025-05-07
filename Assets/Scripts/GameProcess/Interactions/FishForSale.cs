using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace GameProcess.Interactions
{
    public class FishForSale : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        
        [SerializeField] private Animator animator;
        
        
        public void Interact()
        {
            animator.gameObject.layer = LayerMask.NameToLayer("Default");
            
            animator.SetBool("Focused", false);
            
            _eventBus.Publish(InteractionType.FishBaitShopping);
        }

        public void Focus()
        {
            animator.gameObject.layer = LayerMask.NameToLayer("Interactable");
            
            animator.SetBool("Focused", true);
        }

        public void Unfocus()
        {
            animator.gameObject.layer = LayerMask.NameToLayer("Default");
            
            animator.SetBool("Focused", false);
        }
    }
}