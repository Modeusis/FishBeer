using System;
using Sounds;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
         private SoundService _soundService;
        
        private Button _button;
        
        public UnityEvent onClick;
        
        [Inject]
        private void Initialize(SoundService soundService)
        {
            _soundService = soundService;
        }

        private void OnMouseEnter()
        {
            
        }

        private void OnMouseExit()
        {
            _soundService.Play2DSfx(SoundType.ButtonClick, 1f);
        }
        
        private void OnMouseDown()
        {
            _soundService.Play2DSfx(SoundType.ButtonClick, 1f);
            
            onClick?.Invoke();
        }
    }
}