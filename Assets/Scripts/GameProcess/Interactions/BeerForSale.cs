using System.Collections.Generic;
using Sounds;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace GameProcess.Interactions
{
    public class BeerForSale : MonoBehaviour, IInteractable
    {
        [Inject] private EventBus _eventBus;
        [Inject] private SoundService _soundService;
        
        [SerializeField] private Animator animator;
        
        private List<Transform> _bottleChilds;
        
        private void Start()
        {
            _bottleChilds = new List<Transform>();

            for (int i = 0; i < animator.transform.childCount; i++)
            {
                _bottleChilds.Add(animator.transform.GetChild(i));
            }
        }
        
        public void Interact()
        {
            ToggleChildFocus(false);
            
            OnInteract();
            
            animator.SetBool("Focused", false);
            
            _eventBus.Publish(InteractionType.BeerShopping);
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

        private void OnInteract()
        {
            _soundService.Play3DSfx(SoundType.SaleBottleFocus, animator.transform, 5f, 1f);
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