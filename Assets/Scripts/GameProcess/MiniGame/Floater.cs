using System;
using DG.Tweening;
using UnityEngine;

namespace GameProcess.MiniGame
{
    public class Floater : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform handTransform;
        [SerializeField] private Transform waterDestinationTransform;
        [SerializeField] private Transform fishingLineOriginTransform;
        
        
        [Header("Settings")]
        [SerializeField] private float floaterTransitionTime = 1f;

        public void Throw()
        {
            transform.DOKill();
            
            transform.position = fishingLineOriginTransform.position;

            transform.DOMove(waterDestinationTransform.position, floaterTransitionTime)
                .SetEase(Ease.Linear);
        }

        public void Catch()
        {
            transform.DOKill();
            
            transform.position = waterDestinationTransform.position;

            transform.DOMove(waterDestinationTransform.position, floaterTransitionTime)
                .SetEase(Ease.Linear);
        }
        
        public void Idle()
        {
            transform.DOKill();
            
            transform.position = fishingLineOriginTransform.position;
        }
    }
}