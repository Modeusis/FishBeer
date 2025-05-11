using System;
using GameProcess.Interactions;
using Player;
using Player.Camera;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.EventBus;
using Zenject;

namespace UI.Pages
{
    public class BuyFishBitePage : Page
    {
        private EventBus _eventBus;
        
        private ResourceManager _resourceManager;

        private BaseInput _input;
        
        [Header("Pricing")]
        [SerializeField] private float oneFishBaitPrice = 8f;

        [Header("Page components")]
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Slider priceSlider;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text quantityText;
        
        private CameraUnblocker _unblocker;
        
        private float _currentPrice;

        private float CurrentPrice
        {
            get => _currentPrice;
            set
            {
                _currentPrice = value;
                
                priceText.text = _currentPrice + "$";
            }
        }
        
        [Inject]
        private void Initialize(EventBus eventBus, ResourceManager resourceManager, BaseInput input)
        {
            _input = input;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<InteractionType>(HandleInteraction);
            
            _resourceManager = resourceManager;
            
            _unblocker = new CameraUnblocker();
        }

        private void OnEnable()
        {
            confirmButton.onClick.AddListener(HandleConfirm);
            closeButton.onClick.AddListener(ClosePage);
            priceSlider.onValueChanged.AddListener(CalculateCurrentPrice);
            
            priceSlider.value = 0;
        }

        private void Update()
        {
            if (_input.gameplay.Break.WasPressedThisFrame())
            {
                ClosePage();
            }
        }
        
        private void OnDisable()
        {
            confirmButton.onClick.RemoveListener(HandleConfirm);
            closeButton.onClick.RemoveListener(ClosePage);
            priceSlider.onValueChanged.RemoveListener(CalculateCurrentPrice);
        }
        
        private void HandleInteraction(InteractionType interaction)
        {
            if (interaction != InteractionType.FishBaitShopping)
                return;

            priceSlider.wholeNumbers = true;
            priceSlider.maxValue = CalculateAvailableForBuy();
            
            Open();   
        }

        private void ClosePage()
        {
            _eventBus.Publish(_unblocker);
            
            Close();
        }

        private void HandleConfirm()
        {
            if (CurrentPrice >= oneFishBaitPrice)
            {
                _resourceManager.SpendMoney(CurrentPrice);
                _resourceManager.AddFishBaits(Mathf.CeilToInt(priceSlider.value));
            }
            
            ClosePage();
        }
        
        private int CalculateAvailableForBuy()
        {
            var availableMoney = _resourceManager.Money;

            int sliderTopLimit = Mathf.FloorToInt(availableMoney / oneFishBaitPrice);

            return sliderTopLimit;
        }

        private void CalculateCurrentPrice(float value)
        {
            quantityText.text = "x" + value;
            
            CurrentPrice = value * oneFishBaitPrice;
        }
    }
}