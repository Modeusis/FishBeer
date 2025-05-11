using System;
using System.Collections.Generic;
using System.Linq;
using GameProcess.Interactions;
using Player;
using Player.Camera;
using Player.FishStorage;
using Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.EventBus;
using Zenject;

namespace UI.Pages.FishSaleTable
{
    public class FishSalePage : Page
    {
        private EventBus _eventBus;
        
        private FishStorage _fishStorage;
        
        private ResourceManager _resourceManager;
        
        private CameraUnblocker _cameraUnblocker;

        [SerializeField] private GameObject fishesTable;
        
        [SerializeField] private FishElementView tableItemPrefab;
        
        [SerializeField] private TMP_Text noFishTextField;
        [SerializeField] private string noFishText = "Эх не ловится";
        
        [SerializeField] private Button saleButton;
        [SerializeField] private Button closeButton;
        
        private int _totalQuantity = 0;
        
        private float _totalPrice = 0f;
        
        private FishElementView _lastTableItem;
        
        private List<FishElementView> _currentFishesView;
        
        [Inject]
        private void Initialize(EventBus eventBus, FishStorage fishStorage, ResourceManager resourceManager)
        {
            _fishStorage = fishStorage;
            
            _resourceManager = resourceManager;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<InteractionType>(HandleInteraction);
            
            _cameraUnblocker = new CameraUnblocker();

            _currentFishesView = new List<FishElementView>();
        }
        
        private void OnEnable()
        {
            saleButton.onClick.AddListener(Sale);
            closeButton.onClick.AddListener(ClosePage);

            if (_fishStorage.GetFishesAmount() > 0)
            {
                HideNoFishText();
                saleButton.interactable = true;
            }
            else
            {
                ShowNoFishText();
                saleButton.interactable = false;
            }
        }
        
        private void OnDisable()
        {
            saleButton.onClick.RemoveListener(Sale);
            closeButton.onClick.RemoveListener(ClosePage);
            
            _totalQuantity = 0;
            _totalPrice = 0f;

            foreach (var fishView in _currentFishesView)
            {
                Destroy(fishView.gameObject);
            }
            
            _currentFishesView.Clear();

            if (_lastTableItem != null)
            {
                Destroy(_lastTableItem.gameObject);
            }       
        }

        private void HandleInteraction(InteractionType interaction)
        {
            if (interaction != InteractionType.FishSaling)
                return;

            if (_fishStorage.GetFishesAmount() > 0)
            {
                GenerateViewTable();
                CalculateTotalValues();
            }
            
            Open();   
        }

        private void ClosePage()
        {
            _eventBus.Publish(_cameraUnblocker);
            
            Close();
        }

        private void CalculateTotalValues()
        {
            for (int i = 0; i < _fishStorage.GetFishesAmount(); i++)
            {
                _totalPrice += _fishStorage.Fishes[i].Price;
            }
            
            _totalQuantity = _fishStorage.GetFishesAmount();
            
            _lastTableItem = Instantiate(tableItemPrefab, fishesTable.transform);
            
            _lastTableItem.SetFishNameText("Всего");
            _lastTableItem.SetFishQuantityText(_totalQuantity.ToString());
            _lastTableItem.SetTotalPriceText(_totalPrice.ToString());
        }

        private void GenerateViewTable()
        {
            List<FishTotal> fishes = new List<FishTotal>();

            for (int i = 0; i < _fishStorage.GetFishesAmount(); i++)
            {
                var isFishTotalExists = fishes.Exists(total => total.FishName == _fishStorage.Fishes[i].Name);

                if (isFishTotalExists)
                {
                    var existingFishTotal = fishes.FirstOrDefault(total => total.FishName == _fishStorage.Fishes[i].Name);
                    
                    existingFishTotal?.AddFish();
                    
                    continue;
                }
                
                var fishTotal = new FishTotal(_fishStorage.Fishes[i].Name, _fishStorage.Fishes[i].Price);
                
                fishes.Add(fishTotal);
            }

            foreach (var fishTotal in fishes)
            {
                var fishView = Instantiate(tableItemPrefab, fishesTable.transform);
                
                fishView.SetFishNameText(fishTotal.FishName);
                fishView.SetFishQuantityText(fishTotal.FishQuantity);
                fishView.SetTotalPriceText(fishTotal.FishPrice);
                
                _currentFishesView.Add(fishView);
            }
        }

        private void Sale()
        {
            _resourceManager.AddMoney(_fishStorage.SellFishes());
            
            ClosePage();
        }
        
        private void ShowNoFishText()
        {
            noFishTextField.text = noFishText;
            
            noFishTextField.gameObject.SetActive(true);
        }

        private void HideNoFishText()
        {
            noFishTextField.gameObject.SetActive(false);
        }
    }
}