using System;
using UnityEngine;

namespace GameProcess.MiniGame.StateUiScreens
{
    [Serializable]
    public class ToggledFishingScreen
    {
        [field: SerializeField] public float UiTransitionDuration { get; private set; } = 0.5f;
        
        [field: SerializeField] public CanvasGroup ScreenCanvasGroup { get; set; }
    }
}