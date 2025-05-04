using Sounds;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Animations
{
    public class DrinkableBottleAnimationHandle : MonoBehaviour
    {
        [Inject] private SoundService _soundService;
        
        [SerializeField] private float soundRadius = 5f;
        
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
    }
}