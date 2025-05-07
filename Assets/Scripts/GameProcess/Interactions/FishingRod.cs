using System.Collections.Generic;
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

        [SerializeField] private Transform firstFishingRodElement;
        [SerializeField] private Transform secondFishingRodElement;
        
        private Transform _cameraTransform;
        
        private List<Transform> _rodChilds;

        private void Awake()
        {
            _rodChilds = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                _rodChilds.Add(transform.GetChild(i));
            }
        }
        
        public void Interact()
        {
            ToggleChildFocus(false);
        }

        public void Focus()
        {
            ToggleChildFocus(true);
        }

        public void Unfocus()
        {
            ToggleChildFocus(false);
        }
        
        private void ToggleChildFocus(bool focus)
        {
            foreach (var child in _rodChilds)
            {
                child.gameObject.layer = focus ? LayerMask.NameToLayer("Interactable") : LayerMask.NameToLayer("Default");
            }
        }
    }
}