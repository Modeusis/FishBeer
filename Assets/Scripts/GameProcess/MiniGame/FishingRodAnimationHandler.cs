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
        [SerializeField] private float throwTransitionDuration = 0.5f;
        [SerializeField] private float catchTransitionDuration = 0.3f;
        
        [Header("Vectors")]
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 startRotation;

        [SerializeField] private Vector3 togglePosition = new Vector3(1.55f, -1.44f, 0.39f);
        [SerializeField] private Vector3 toggleRotation = new Vector3(337f, 350f, 180f);
        
        [SerializeField] private Vector3 throwPosition = new Vector3(1.55f, -1.44f, 2.242f);
        [SerializeField] private Vector3 throwRotation = new Vector3(285f, 170f, 0f);
        
        [SerializeField] private Vector3 catchPosition = new Vector3(1.88f, -1.739f, 2.08f);
        [SerializeField] private Vector3 catchRotation = new Vector3(321f, 14f, 127f);
        
        [Header("Fishing Line floater")]
        [SerializeField] private Floater fishingLineFloater;
        
        private Sequence _moveSequence;
        private Sequence _rotateSequence;
        
        public void Idle()
        {
            _moveSequence?.Kill();
            _rotateSequence?.Kill();
            fishingRod.DOKill();

            fishingRod.DOLocalMove(startPosition, idleTransitionDuration);
            fishingRod.DOLocalRotate(startRotation, idleTransitionDuration);
        }

        public void Toggle()
        {
            _moveSequence?.Kill();
            _rotateSequence?.Kill();
            fishingRod.DOKill();
            
            fishingRod.DOLocalMove(togglePosition, toggleTransitionDuration);
            fishingRod.DOLocalRotate(toggleRotation, toggleTransitionDuration);
            
            fishingLineFloater.Idle();
        }
        
        public void Throw()
        {
            _moveSequence?.Kill();
            _rotateSequence?.Kill();
            fishingRod.DOKill();
            
            _moveSequence = DOTween.Sequence();
            _rotateSequence = DOTween.Sequence();
            
            _moveSequence.SetEase(Ease.InOutSine);
            _rotateSequence.SetEase(Ease.InOutSine);
            
            _moveSequence.Append(fishingRod.DOLocalMove(throwPosition, throwTransitionDuration));
            _moveSequence.Append(fishingRod.DOLocalMove(togglePosition, throwTransitionDuration));
            
            _rotateSequence.Append(fishingRod.DOLocalRotate(throwRotation, throwTransitionDuration));
            _rotateSequence.Append(fishingRod.DOLocalRotate(toggleRotation, throwTransitionDuration));
            
            fishingLineFloater.Throw();
        }

        public void Catch()
        {
            _moveSequence?.Kill();
            _rotateSequence?.Kill();
            fishingRod.DOKill();
            
            _moveSequence = DOTween.Sequence();
            _rotateSequence = DOTween.Sequence();
            
            _moveSequence.SetEase(Ease.InOutSine);
            _rotateSequence.SetEase(Ease.InOutSine);
            
            _moveSequence.Append(fishingRod.DOLocalMove(catchPosition, catchTransitionDuration));
            _moveSequence.Append(fishingRod.DOLocalMove(togglePosition, catchTransitionDuration));
            
            _rotateSequence.Append(fishingRod.DOLocalRotate(catchRotation, catchTransitionDuration));
            _rotateSequence.Append(fishingRod.DOLocalRotate(toggleRotation, catchTransitionDuration));
            
            fishingLineFloater.Catch();
        }
    }
}