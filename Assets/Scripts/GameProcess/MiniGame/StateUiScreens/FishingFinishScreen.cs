using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameProcess.MiniGame.StateUiScreens
{
    [Serializable]
    public class FishingFinishScreen
    {
        [field: SerializeField] public CanvasGroup FinishFishingScreen {get; private set; }
        
        [field: SerializeField] public float ToggleDuration {get; private set; }
        [field: SerializeField] public float SpinDuration {get; private set; }
        
        [field: SerializeField] public Transform FishViewTransform {get; private set; }
        
        [field: SerializeField] public TMP_Text FishNameTextField {get; private set; }
        
        [field: SerializeField] public Button LeaveButton {get; private set; }
        [field: SerializeField] public Button RestartMiniGameButton {get; private set; }
    }
}