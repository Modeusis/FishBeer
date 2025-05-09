using System;
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

        public void Idle()
        {
            
        }

        public void Throw()
        {
            
        }

        public void Pool()
        {
            
        }

        public void Catch()
        {
            
        }
    }
}