using GameProcess.MiniGame;
using Player.FishStorage;
using UnityEngine;
using Zenject;

namespace GameProcess.Interactions
{
    [RequireComponent(typeof(Animator))]
    public class FishingRod : MonoBehaviour, IInteractable
    {
        [Inject] private Animator _animator;

        [SerializeField] private MiniGameSetup miniGames;
        [SerializeField] private FishSetup fishSetup;
        
        private Transform _cameraTransform;
        
        public void Interact()
        {
            Debug.Log("Interacted with rod");
        }

        public void Focus()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }

        public void Unfocus()
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        
        
    }
}