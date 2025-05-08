using System.Collections.Generic;
using DG.Tweening;
using GameProcess.MiniGame;
using Player;
using Player.FishStorage;
using UnityEngine;
using Zenject;

namespace GameProcess.Interactions
{
    public class FishingRod : MonoBehaviour, IInteractable
    {
        [Inject] private ResourceManager _resourceManager;

        [SerializeField] private MiniGameSetup miniGames;
        [SerializeField] private FishSetup fishSetup;
        
        [SerializeField] private Transform fishRodTransform;
        [SerializeField] private Transform firstFishingRodElement;
        [SerializeField] private Transform secondFishingRodElement;
        
        [SerializeField] private float focusDuration = 0.5f;
        
        private List<Transform> _rodChilds;

        private Sequence _sequence;
        private Sequence _subSequence;
        
        private Vector3 _startPosition;
        private Vector3 _startRotation;
        
        private Vector3 _startFirstPartPosition;
        private Vector3 _startSecondPartPosition;
        
        private void Awake()
        {
            _startPosition = fishRodTransform.localPosition;
            _startRotation = fishRodTransform.localRotation.eulerAngles;
            
            _startFirstPartPosition = firstFishingRodElement.localPosition;
            _startSecondPartPosition = secondFishingRodElement.localPosition;
            
            _rodChilds = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                _rodChilds.Add(transform.GetChild(i));
            }
        }
        
        public void Interact()
        {
            if (!ValidateMiniGameStart())
                return;
            
            ToggleChildFocus(false);
        }

        public void Focus()
        {
            FocusAnimation();
            
            ToggleChildFocus(true);
        }

        public void Unfocus()
        {
            UnfocusAnimation();
            
            ToggleChildFocus(false);
        }
        
        private void ToggleChildFocus(bool focus)
        {
            foreach (var child in _rodChilds)
            {
                child.gameObject.layer = focus ? LayerMask.NameToLayer("Interactable") : LayerMask.NameToLayer("Default");
            }
        }

        private bool ValidateMiniGameStart()
        {
            if (_resourceManager.FishBaits == 0)
                return false;
            
            return true;
        }

        private void FocusAnimation()
        {
            Debug.Log("focus rod");
            
            fishRodTransform?.DOKill();
            _sequence?.Kill();
            _subSequence?.Kill();
            
            _sequence = DOTween.Sequence();
            _subSequence = DOTween.Sequence();
            
            _sequence.Append(fishRodTransform.DOLocalMove(new Vector3(11.743f, 9.72799969f, 8.55599976f), focusDuration / 2));
            _sequence.Append(fishRodTransform.DOLocalMove(new Vector3(11.7919998f, 9.96899986f, 8.21500015f),
                focusDuration / 2));

            _sequence.OnComplete(() =>
            {
                fishRodTransform.DOLocalRotate(new Vector3(309.829834f,318.94632f,206.364349f), focusDuration / 3);
                
                fishRodTransform.DOLocalMove(new Vector3(10.8649998f,9.84700012f,8.18500042f), focusDuration / 3)
                    .SetDelay(focusDuration / 3)
                    .SetEase(Ease.Linear);
                fishRodTransform.DOLocalRotate(new Vector3(309.829834f,318.94632f,168.154434f), focusDuration / 3)
                    .SetDelay(focusDuration / 3)
                    .SetEase(Ease.Linear);
            });

            _subSequence.Append(firstFishingRodElement.DOLocalMoveZ(_startFirstPartPosition.z + 0.01f, focusDuration / 2));
            _subSequence.Append(secondFishingRodElement.DOLocalMoveZ(_startSecondPartPosition.z + 0.01f, focusDuration / 2));
            
            _sequence.SetEase(Ease.Flash);
            _subSequence.SetEase(Ease.Flash);
        }

        private void UnfocusAnimation()
        {
            Debug.Log("Unfocus rod");
            
            fishRodTransform?.DOKill();
            _sequence?.Kill();
            _subSequence?.Kill();

            fishRodTransform.DOLocalMove(_startPosition, focusDuration);
            fishRodTransform.DOLocalRotate(_startRotation, focusDuration);

            firstFishingRodElement.DOLocalMoveZ(_startFirstPartPosition.z, focusDuration);
            secondFishingRodElement.DOLocalMoveZ(_startSecondPartPosition.z, focusDuration);
        }
    }
}