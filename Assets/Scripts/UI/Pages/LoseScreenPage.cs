using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Pages
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoseScreenPage : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        [SerializeField] private float showDuration = 0.5f;
        
        [SerializeField] private TMP_Text titleText;
        
        [SerializeField] private Button exitButton;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            
            exitButton.onClick.AddListener(Exit);
        }
        
        public void Show()
        {
            _canvasGroup.DOKill();
            
            _canvasGroup.DOFade(1f, showDuration)
                .SetUpdate(true);
            
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}