using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameProcess.MiniGame.StateUiScreens
{
    [Serializable]
    public class FishingActiveScreen
    {
        [Header("Page canvas group")]
        [field: SerializeField] public CanvasGroup ActiveScreen { get; private set; }
        
        [field: SerializeField] public float CanvasShowSpeed { get; private set; }
        
        [Header("Images")]
        [field: SerializeField] private Image ProgressBar { get; set; }
        [field: SerializeField] private Image ProgressBarCatchZone { get; set; }
        [field: SerializeField] private Image FishIcon { get; set; }
        
        [Header("Text")]
        [field: SerializeField] private TMP_Text ToolTip { get; set; }

        [SerializeField] private float textScale = 1.6f;
        [SerializeField] private float textScaleDuration = 0.4f;
        
        [SerializeField] private string notNowText;
        [SerializeField] private string rightNowText;
        
        private bool _isToolTipActive;
        
        private Sequence _sequence;
        
        private string ToolTipText
        {
            get => ToolTip.text;
            set
            {
                if (ToolTipText == value)
                    return;
                
                _sequence?.Kill();
                ToolTip.transform.DOKill();
                
                _sequence = DOTween.Sequence();
                
                if (_isToolTipActive)
                {
                    _sequence.Append(ToolTip.transform.DOScale(textScale, textScaleDuration));
                    _sequence.Append(ToolTip.transform.DOScale(1f, textScaleDuration));
                }
                else
                {
                    ToolTip.transform.DOScale(1f, textScaleDuration);
                }
                
                
                ToolTip.text = value;
            }
        }

        public void SetRandomCatchZone(Vector2 percentageBounds)
        {
            var progressBarPos = ProgressBar.rectTransform.anchoredPosition;
            var progressBarRect = ProgressBar.rectTransform.rect;
            
            var zoneRectPosition = ProgressBarCatchZone.rectTransform.anchoredPosition;
            
            var minBound =  progressBarPos.x + progressBarRect.xMin * percentageBounds.x;
            var maxBound =  progressBarPos.x + progressBarRect.xMax * percentageBounds.y;
            
            zoneRectPosition.x = Random.Range(minBound, maxBound);
            
            ProgressBarCatchZone.rectTransform.anchoredPosition = zoneRectPosition;
        }
        
        public void UpdateProgress(float progress)
        {
            ProgressBar.fillAmount = progress;

            var progressBarPos = ProgressBar.rectTransform.anchoredPosition;
            var progressBarRect = ProgressBar.rectTransform.rect;
            
            var fishRectPosition = FishIcon.rectTransform.anchoredPosition;
            
            fishRectPosition.x = Mathf.Lerp(progressBarPos.x + progressBarRect.xMin, progressBarPos.x + progressBarRect.xMax, progress);

            FishIcon.rectTransform.anchoredPosition = fishRectPosition;
        }

        public bool CallCatch()
        {
            Rect catchZoneRect = ProgressBarCatchZone.rectTransform.rect;
            Vector2 catchZoneCenter = ProgressBarCatchZone.rectTransform.position;
    
            Rect fishIconRect = FishIcon.rectTransform.rect;
            Vector2 fishIconCenter = FishIcon.rectTransform.position;

            if (RectOverlap(fishIconRect, fishIconCenter, catchZoneRect, catchZoneCenter))
            {
                return true;
            }
            
            return false;
        }

        public void ToggleTooltip(bool isActive)
        {
            _isToolTipActive = isActive;
            
            ToolTipText = isActive ? rightNowText : notNowText;
        }
        
        private bool RectOverlap(Rect rect1, Vector2 pos1, Rect rect2, Vector2 pos2)
        {
            return pos1.x + rect1.xMin < pos2.x + rect2.xMax &&
                   pos1.x + rect1.xMax > pos2.x + rect2.xMin &&
                   pos1.y + rect1.yMin < pos2.y + rect2.yMax &&
                   pos1.y + rect1.yMax > pos2.y + rect2.yMin;
        }
    }
}