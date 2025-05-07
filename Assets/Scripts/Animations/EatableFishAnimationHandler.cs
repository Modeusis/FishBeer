using Sounds;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using SoundType = Sounds.SoundType;

namespace Animations
{
    public class EatableFishAnimationHandler : MonoBehaviour
    {
        private SoundService _soundService;

        [SerializeField] private Animator _fishAnimator;

        public UnityEvent onFinishEating;
        
        public void StartEating()
        {
            _fishAnimator.SetBool("Eating", true);
        }

        public void SetupEatableFish(SoundService soundService)
        {
            _soundService = soundService;
        }
        
        public void Focus()
        {
            _fishAnimator.SetBool("Focused", true);
        }
        
        public void Unfocus()
        {
            _fishAnimator.SetBool("Focused", false);
        }
        
        public void OnBite()
        {
            _soundService.Play2DSfx(SoundType.Bite, 1f);
        }

        public void OnFocus()
        {
            _soundService.Play3DSfx(SoundType.FishFocus, transform, 10f, 1f);
        }

        public void OnFinishEating()
        {
            onFinishEating?.Invoke();
        }
    }
}