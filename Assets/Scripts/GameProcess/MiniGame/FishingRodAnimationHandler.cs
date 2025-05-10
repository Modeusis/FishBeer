using System;
using DG.Tweening;
using UnityEngine;

namespace GameProcess.MiniGame
{
    [Serializable]
    public class FishingRodAnimationHandler
    {
        [Header("Rod transforms")]
        [SerializeField] private Transform fishingRod;
        [SerializeField] private Transform fishingRodFirstItem;
        [SerializeField] private Transform fishingRodSecondItem;
        [SerializeField] private Transform coil;
        [SerializeField] private Transform coilHandle;
        
        [Header("Settings")]
        [SerializeField] private float idleTransitionDuration = 0.5f;
        [SerializeField] private float toggleTransitionDuration = 0.5f;
        [SerializeField] private float throwTransitionDuration = 0.3f;
        [SerializeField] private float catchTransitionDuration = 0.5f;
        
        [Header("Vectors")]
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 startRotation;

        [SerializeField] private Vector3 togglePosition = new Vector3(1.55f, -1.44f, 0.39f);
        [SerializeField] private Vector3 toggleRotation = new Vector3(337f, 350f, 180f);
        
        [SerializeField] private Vector3 throwPosition = new Vector3(1.55f, -1.44f, 2.242f);
        [SerializeField] private Vector3 throwRotation = new Vector3(285f, 170f, 0f);
        
        private Sequence _throwMoveSequence;
        private Sequence _throwRotateSequence;
        
        public void Idle()
        {
            _throwMoveSequence?.Kill();
            _throwRotateSequence?.Kill();
            fishingRod.DOKill();

            fishingRod.DOLocalMove(startPosition, idleTransitionDuration);
            fishingRod.DOLocalRotate(startRotation, idleTransitionDuration);
        }

        public void Toggle()
        {
            _throwMoveSequence?.Kill();
            _throwRotateSequence?.Kill();
            fishingRod.DOKill();
            
            fishingRod.DOLocalMove(togglePosition, toggleTransitionDuration);
            fishingRod.DOLocalRotate(toggleRotation, toggleTransitionDuration);
        }
        
        public void Throw()
        {
            _throwMoveSequence?.Kill();
            _throwRotateSequence?.Kill();
            fishingRod.DOKill();
            
            _throwMoveSequence = DOTween.Sequence();
            _throwRotateSequence = DOTween.Sequence();
            
            _throwMoveSequence.SetEase(Ease.InOutSine);
            _throwRotateSequence.SetEase(Ease.InOutSine);
            
            _throwMoveSequence.Append(fishingRod.DOLocalMove(throwPosition, throwTransitionDuration));
            _throwMoveSequence.Append(fishingRod.DOLocalMove(togglePosition, throwTransitionDuration));
            
            _throwRotateSequence.Append(fishingRod.DOLocalRotate(throwRotation, throwTransitionDuration));
            _throwRotateSequence.Append(fishingRod.DOLocalRotate(toggleRotation, throwTransitionDuration));
        }

        public void Catch()
        {
            _throwMoveSequence?.Kill();
            _throwRotateSequence?.Kill();
            fishingRod.DOKill();
        }
    }
}