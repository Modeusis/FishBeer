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
        [SerializeField, Range(0.005f, 0.05f)] private float fishingLineEndWidth = 0.005f;
        
        [Header("Fishing Line Positions")]
        [SerializeField] private List<Transform> fishingLineDestinations = new List<Transform>();
        

        public void RenderFishingLine()
        {
            fishingLineOrigin.enabled = true;
            
            fishingLineOrigin.startWidth = fishingLineWidth;
            fishingLineOrigin.endWidth = fishingLineEndWidth;
            
            fishingLineOrigin.positionCount = fishingLineDestinations.Count;
            
            for (int i = 0; i < fishingLineDestinations.Count; i++)
            {
                fishingLineOrigin.SetPosition(i, fishingLineDestinations[i].position);
            }
        }

        public void HideFishingLine()
        {
            fishingLineOrigin.enabled = false;
        }
    }
}