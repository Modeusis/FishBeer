using UnityEngine.EventSystems;

namespace GameProcess.Interactions
{
    public interface IInteractable
    {
        public void Interact();
        
        public void Focus();
        
        public void Unfocus();
    }
}