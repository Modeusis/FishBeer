using System.Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars
{
    public class BaseBar : MonoBehaviour
    {
        [SerializeField] private Material bar;
        [SerializeField, Range(0, 100)] private float startFill = 20f;
        
        private float _fill;

        public float Fill
        {
            get => _fill;
            private set
            {
                if (_fill == value)
                    return;

                if (_fill < 0)
                {
                    _fill = 0f;
                }

                if (_fill > 100)
                {
                    _fill = 100f;
                }
                
                _fill = value;
                
                bar.DOFloat(_fill / 100f, "_Progress", 0.1f);
            }
        }

        private void Start()
        {
            Fill = startFill;
        }
        
        public void UpdateBar(float decreaseValue)
        {
            if (decreaseValue <= 0)
                return;
            
            Fill -= decreaseValue * Time.deltaTime;
        }

        public void OneTimeDecrease(float amount)
        {
            if (amount < 0)
                return;
            
            Fill -= amount;
        }
        
        public void OneTimeIncrease(float amount)
        {
            if (amount < 0)
                return;
            
            Fill += amount;
        }
    }
}