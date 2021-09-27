using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Carousel;
using Assets.Scripts.MainSceneContainer.ViewModels;
using Assets.Scripts.MainWindows;
using Assets.Scripts.MainWindows.Cart;
using Assets.Scripts.MainWindows.Tutorial;
using DG.Tweening;
using Engenious.Core.Managers;
using Shop.Core;
using UnityEngine;

namespace Engenious.MainScene.SceneStates.MainSceneStates
{
    //этот идиотизм написан только потому, что был приказ писать говнокод,, дабы успеть в сроки.
    //я искренне прошу прощения за нарушение буквы С в СОЛИД 
    public class MainPageState : BasicMainSceneState<DefaultSceneStateParams>
    {
        [SerializeField] private CircleControllerConfig _circleControllerConfig;

        private CircleController _circleController;
        
        private MainWindow _mainWindow;
        private CartWindow _cartWindow;
        private ProductDescriptionWindow _productDescription;
        private UserMenuWindow _userMenu;        
        
        private CartViewModel _cartVM;
        private DeliveryVM _deliveryVm;

        private UserOrders _orders;
        
        private FilterWindow _filterWindow;
        public override bool SetActivate(bool value)
        {
            if (base.SetActivate(value))
            {
                if (value)
                {
                    ActivateState();
                }
                else
                {
                    DeActivateState();
                    return false;
                }
            }

            return true;
        }

        private void ActivateState()
        {
            InitRefs();
            SubscribeEvents();
            
            CreateProductItems();
            
            CallRequest(StartAr);

            ShowFirstTutorial();
            //StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.StartAR();
            //_filterWindow.Aplly += FilterWindowOnAplly;
        }

        private void ShowFirstTutorial()
        {
            var user = StatesManager.MainSceneContainer.MainSceneModels.UserDataController.UserData;
            
            if (!user.TutorWasShowed)
            {
                StatesManager.WindowsManager.Show<TutorialWindow>();
                StatesManager.MainSceneContainer.MainSceneModels.UserDataController.SetFirstTutorial(true);
            }
        }

        private List<ProductResponse> request;
        private async void FilterWindowOnAplly()
        {
            CallRequest();

            if (request == null || request.Count < 2)
            {
                return;
            }

            if (_circleController != null)
            {
                StopAR();
            }

            StartAr();

            _filterWindow.Aplly -= FilterWindowOnAplly;
            StatesManager.WindowsManager.Close<FilterWindow>();
        }

        private async void CallRequest(Action onComplete = null)
        {
            request = await _cartVM.GetFilterdRequest(_filterWindow.Data);

            if (request != null)
            {
                onComplete?.Invoke();
            }
        }
        
        private void InitRefs()
        {
            _userMenu = StatesManager.WindowsManager.GetWindow<UserMenuWindow>();
            _filterWindow = StatesManager.WindowsManager.GetWindow<FilterWindow>();
            _mainWindow = StatesManager.WindowsManager.Show<MainWindow>();

            _productDescription = StatesManager.WindowsManager.GetWindow<ProductDescriptionWindow>();

            _cartWindow = StatesManager.WindowsManager.GetWindow<CartWindow>();
            
            _cartVM = StatesManager.MainSceneContainer.MainSceneViewModels.CartViewModel;
            _cartVM.SetWindow(_cartWindow);

            _deliveryVm = StatesManager.MainSceneContainer.MainSceneViewModels.DeliveryVM;
            _deliveryVm.SetWindow(StatesManager.WindowsManager.GetWindow<DeliveryWindow>());

            _orders = new UserOrders(StatesManager.CoreApi.NetworkManager.NetworkCartRequests,
                StatesManager.MainSceneContainer.MainSceneModels.UserDataController,
                StatesManager.WindowsManager);
        }

        private void SubscribeEvents()
        {
            _userMenu.LogOutEvent += UserLogOut;
            _mainWindow.SubscribeButtons(SubscribeCartButton,OpenFilterWindow, MainUserButton, TutorialButton);
            _productDescription.AddToCart += ProductDescriptionOnAddToCart;
            _mainWindow.ResetButton.onClick.AddListener(ResetOnlyAr);
            //_cartVM.ClearCart();

            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.RaycastSystem.OnTargetEvent += RaycastSystemOnOnTargetEvent;
            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.RaycastSystem.OnTargetLostEvent += RaycastSystemOnOnTargetLostEvent;
        }

        private void TutorialButton()
        {
            StatesManager.WindowsManager.Show<TutorialWindow>();
        }

        private void MainUserButton()
        {
            StatesManager.WindowsManager.Close<MainWindow>();
            StatesManager.WindowsManager.Show<UserMenuWindow>(StatesManager.MainSceneContainer.MainSceneModels.UserDataController, 
                                        StatesManager.CoreApi.NetworkManager.UserIdHolder,
                                        StatesManager.MainSceneContainer.MainSceneViewModels.SupportVm,
                                        StatesManager.MainSceneContainer.MainSceneViewModels.UserDeliveryDetailsVm, 
                                        _orders);
        }

        private void UnSubscribeEvents()
        {
            _userMenu.LogOutEvent -= UserLogOut;
            _productDescription.AddToCart -= ProductDescriptionOnAddToCart;
            _deliveryVm.OrderSuccess -= OnDeliveryNext;
            _mainWindow.ResetButton.onClick.RemoveListener(ResetOnlyAr);

            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.RaycastSystem.OnTargetEvent -= RaycastSystemOnOnTargetEvent;
            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.RaycastSystem.OnTargetLostEvent -= RaycastSystemOnOnTargetLostEvent;
            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.ReadyCreateItems -= OnReadyCreateItems;
        }

        private void UserLogOut()
        {
            StatesManager.DeactivateState<MainPageState>();
            StatesManager.ActivateState<WelcomeState>(new DefaultSceneStateParams());
        }

        private void OpenFilterWindow()
        {
            _filterWindow = StatesManager.WindowsManager.Show<FilterWindow>();
            _filterWindow.Aplly += FilterWindowOnAplly;
            _mainWindow.DeactivateFilterButton();
            _filterWindow.OnClosed += CloseFilterWindow;
        }

        private void CloseFilterWindow()
        {
            _filterWindow.Aplly -= FilterWindowOnAplly;
            _filterWindow.OnClosed -= CloseFilterWindow;
            _mainWindow.ActivateFilterButton();
        }

        private void RaycastSystemOnOnTargetEvent(ISelectable selected)
        {
            var circleItem = selected as CircleItem;
            
            if (circleItem != null)
            {
                StatesManager.WindowsManager.Show<ProductDescriptionWindow>();
                _productDescription.SetData(circleItem.Item.ProductInfo.ProductData);
            }
        }

        private void RaycastSystemOnOnTargetLostEvent(ISelectable selected)
        {
            StatesManager.WindowsManager.Close<ProductDescriptionWindow>();
        }

        private void ProductDescriptionOnAddToCart(ProductResponse item)
        {
            _cartVM.CreateItemResponce(item);
        }

        private void SubscribeCartButton()
        {
            _cartVM.UpdateWindow();
            _cartWindow = StatesManager.WindowsManager.Show<CartWindow>();
            _cartWindow.SubscribeButtons(OnCartNext, OnCartBack);
        }

        private void OnCartBack()
        {
            StatesManager.WindowsManager.Close<CartWindow>();
        }

        private void OnCartNext()
        {
            StatesManager.WindowsManager.Close<CartWindow>();
            var delivery = StatesManager.WindowsManager.Show<DeliveryWindow>();
            
            delivery.SubscribeButtons(OnDeliveryNext ,OnDeliveryBack);
            //_deliveryVm.OrderSuccess += OnDeliveryNext;
            //
        }

        private void OnDeliveryBack()
        {
            StatesManager.WindowsManager.Close<DeliveryWindow>();
            SubscribeCartButton();
        }

        private void OnDeliveryNext()
        {
            _deliveryVm.OrderSuccess += ShowDeliveryReview;

            _deliveryVm.OnMakeOrder();
        }

        private void ShowDeliveryReview()
        {
            _deliveryVm.OrderSuccess -= ShowDeliveryReview;

            StatesManager.WindowsManager.Close<DeliveryWindow>();

            var delWindow = StatesManager.WindowsManager.Show<DeliveryReviewWindow>();
            
            delWindow.SubscribeButtons(OnDeliveryReviewNext,OnDeliveryReviewBack);
            delWindow.SetCartData(_cartVM.GetDeliveryProducts(), _deliveryVm.GetDeliveryReviewWindowData());
        }

        private void OnDeliveryReviewBack()
        {
            StatesManager.WindowsManager.Close<DeliveryReviewWindow>();
        }

        private void OnDeliveryReviewNext()
        {
            // _deliveryVm.OrderSuccess += OnSuccess;
            //
            // _deliveryVm.OnMakeOrder();
            _deliveryVm.ConfirmOrder(OnDeliverySuccess);
        }

        private void OnDeliverySuccess()
        {
            //_deliveryVm.OrderSuccess -= OnSuccess;

            _cartVM.ClearCart();
            StatesManager.WindowsManager.Close<DeliveryReviewWindow>();
            var orderCreatedWindow = StatesManager.WindowsManager.Show<OrderCreatedWindow>();
            orderCreatedWindow.SubscribeIsOnSaveAddress(() =>
            {
                var address =_deliveryVm.GetResultAddress();
                StatesManager.MainSceneContainer.MainSceneModels.UserDataController.SetAddress(address);
            });
        }

        private void DeActivateState()
        {
            DeleteProducts();
            UnSubscribeEvents();
            //_cartVM.ClearCart();
            StatesManager.WindowsManager.Close<MainWindow>();
            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.ResetAll();
            _circleController.Reset();
            _mainWindow.UnSubscribe();
        }


        // private void ResetAr()
        // {
        //     DeActivateState();
        //     ActivateState();
        // }

        #region AR

        [SerializeField]
        private GetCircleItemListConfig _products;

        private GameObject _itemDefaultParent;

        private void CreateProductItems()
        {
            _itemDefaultParent = new GameObject("Products holder (MainPageState:287)");
            _circleControllerConfig.GetCircleItemListConfig.ItemPrefabs.Clear();
            foreach (var product in _products.ItemPrefabs)
            {
                var item = Instantiate(product);
                item.SetDefaultTransform(_itemDefaultParent.transform);
                item.DisableItem();
                
                _circleControllerConfig.GetCircleItemListConfig.ItemPrefabs.Add(item);
            }
        }

        private void DeleteProducts()
        {
            // foreach (var product in _products.ItemPrefabs)
            // {
            //     product.Delete();
            // }
            //
            // _products.ItemPrefabs.Clear();

            if (_itemDefaultParent != null)
            {
                Destroy(_itemDefaultParent);
            }
        }

        private void CreateArItems()
        {
            var legacy = StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses;
            _circleController = new CircleController(_circleControllerConfig,
                legacy.SwipeDetection,
                legacy.ProductPresenter,
                legacy.ReticleController);

            _circleController.Initizlize(request);

            legacy.ProductPresenter.StartPresentation += OnStartPresentation;
            legacy.ProductPresenter.EndPresentation += OnEndPresentation;
            legacy.ProductPresenter.StartOpenProduct += (item) =>
            {
                StatesManager.MainSceneContainer.MainSceneServices.SoundManager.PlayStartShowProduct();
            };
            legacy.ProductPresenter.StartCloseProduct += (item) =>
            {
                StatesManager.MainSceneContainer.MainSceneServices.SoundManager.PlayEndShowProduct();
            };

            _circleController.CircleSwipeEvent += () =>
                StatesManager.MainSceneContainer.MainSceneServices.SoundManager.PlaySwipeCarousel();
        }

        private void OnStartPresentation(Item item)
        {
            StatesManager.WindowsManager.Show<ProductDescriptionWindow>();
            _productDescription.SetData(item.ProductInfo.ProductData);
            StatesManager.MainSceneContainer.MainSceneServices.SoundManager.PlaySelectProduct();
        }

        private void OnEndPresentation(Item item)
        {
            StatesManager.WindowsManager.Close<ProductDescriptionWindow>();
            
            //var legacy = StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses;

            // legacy.ProductPresenter.StartPresentation -= OnStartPresentation;
            // legacy.ProductPresenter.EndPresentation -= OnEndPresentation;
        }
        
        private void OnReadyCreateItems()
        {
            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.ReadyCreateItems -= OnReadyCreateItems;

            CreateArItems();
        }

        private void StartAr()
        {
            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.StartAR();
            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.ReadyCreateItems += OnReadyCreateItems;
        }

        private void StopAR()
        {
            StatesManager.MainSceneContainer.MainSceneObjects.LegacyArClasses.ResetAll();
            _circleController.Reset();
        }

        public void ResetOnlyAr()
        {
            StopAR();
            StartAr();
        }
        
        #endregion
        
        // private void FixedUpdate()
        // {
        //     _circleController?.Update();
        // }

        private void OnApplicationQuit()
        {
            _cartVM.SaveCart();
        }
        
        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _cartVM.SaveCart();
            }
        }
        
        private void Update()
        {
            _circleController?.Update();

            if (Input.GetKeyDown(KeyCode.L))
            {
                ResetOnlyAr();
            }
        }
    }
}