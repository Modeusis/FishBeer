using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameProcess.MiniGame
{
    [Serializable]
    public class FishingLineHandler
    {
        [Header("General")]
        [SerializeField] private LineRenderer fishingLineOrigin;
        
        [SerializeField, Range(0.01f, 0.05f)] private float fishingLineWidth = 0.02f;
        
        [Header("Fishing Line Positions")]
        [SerializeField] private List<Transform> fishingLineDestinations = new List<Transform>();
        
        public void RenderFishingLine()
        {
            if (fishingLineOrigin == null)
            {
                Debug.LogError("FishingLineOrigin is not assigned!");
                return;
            }

            if (fishingLineDestinations == null || fishingLineDestinations.Count == 0)
            {
                Debug.LogError("No fishing line destinations assigned!");
                return;
            }

            fishingLineOrigin.enabled = true;
            fishingLineOrigin.startWidth = fishingLineWidth;
            
            fishingLineOrigin.positionCount = fishingLineDestinations.Count;
            
            for (int i = 0; i < fishingLineDestinations.Count; i++)
            {
                fishingLineOrigin.SetPosition(i, fishingLineDestinations[i].position);
            }
        }

        public void HideFishingLine()
        {
            // if (fishingLineOrigin != null)
            // {
            //     fishingLineOrigin.enabled = false;
            // }
        }
    }
}