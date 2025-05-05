using System;
using DG.Tweening;
using Sounds;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Animations
{
    public class DrinkableBottleAnimationHandle : MonoBehaviour
    {
        [Inject] private SoundService _soundService;
        
        [Header("Sounds")]
        [SerializeField] private float soundRadius = 5f;
        
        [Header("Drink animations")]
        [SerializeField] private Animator _bottleAnimator;
        [SerializeField] private Animator _canAnimator;
        
        public UnityEvent onDrink;

        public void StartDrinking()
        {
            _bottleAnimator?.SetBool("Drinking", true);
        }
        
        public void CanOpen()
        {
            _soundService.Play3DSfx(SoundType.CanOpen, transform, soundRadius, 1f);
            
            _canAnimator?.SetTrigger("Open");
        }

        public void OnGulp()
        {
            _soundService.Play2DSfx(SoundType.Gulp, 1f);
        }
        
        public void HandleDrinkFinish()
        {
            _soundService.Play2DSfx(SoundType.Burp, 1f);
            _soundService.Play2DSfx(SoundType.BottleBreak, 1f);
            
            onDrink?.Invoke();
        }

        public void Focus()
        {
            _bottleAnimator.SetBool("Focused", true);
        }
        
        public void Unfocus()
        {
            _bottleAnimator.SetBool("Focused", false);
        }
    }
}