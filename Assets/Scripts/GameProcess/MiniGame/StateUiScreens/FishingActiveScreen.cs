using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameProcess.MiniGame.StateUiScreens
{
    [Serializable]
    public class FishingActiveScreen
    {
        [field: SerializeField] private CanvasGroup ActiveScreen { get; set; }
        
        [field: SerializeField] private Image ProgressBar { get; set; }
        [field: SerializeField] private Image ProgressBarCatchZone { get; set; }
        [field: SerializeField] private Image FishIcon { get; set; }
        
        public void UpdateProgress(float progress)
        {
            var clampedProgress = Mathf.Clamp(progress, 0f, 1f);
            
            ProgressBar.fillAmount = clampedProgress;
        }
    }
}