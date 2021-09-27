using System;
using System.Collections.Generic;
using Assets.Scripts.InputServices;
using Shop.Core;
using UnityEngine;

namespace Assets.Scripts.Carousel
{
    [Serializable]
    public class CircleControllerConfig
    {
        public int MaxRotationStep = 4;
        
        public CircleRotatorConfig CircleRotatorConfig;
        public GetCircleItemListConfig GetCircleItemListConfig;
        public CreateItemsAroundConfig CreateItemsAroundConfig;
        public CircleItemListControllerConfig CircleItemListControllerConfig;
    }

    public class CircleController
    {
        private CircleControllerConfig _config;
        
        private CircleRotator _circleRotator;
        private CircleCreator _circleCreator;
        private CircleItemListController _itemListController;
        private IGetCircleItemList _getItemList;

        private TestSwipeDetection _swipeDetection;
        private ProductPresenter _productPresenter;
        private ReticleController _reticleController;

        public event Action CircleSwipeEvent;
        public CircleController(){}

        private bool _isInitialized;
        
        public CircleController(CircleControllerConfig config,
            TestSwipeDetection swipeDetection,
            ProductPresenter productPresenter,
            ReticleController reticleController)
        {
            _config = config;
            _swipeDetection = swipeDetection;
            _productPresenter = productPresenter;
            _reticleController = reticleController;
        }

        public void Initizlize(List<ProductResponse> products)
        {
            if (_isInitialized)
                return;
            
            _getItemList = new GetCircleItemList(_config.GetCircleItemListConfig);
            _itemListController = new CircleItemListController(_config.CircleItemListControllerConfig);
            _circleCreator = new CircleCreator(_config.CreateItemsAroundConfig);
            _circleRotator = new CircleRotator(_config.CircleRotatorConfig);
            
            (_getItemList as GetCircleItemList).Initialize(products);
            _itemListController.Initialize(_getItemList.GetDictionaryItems());
            _circleCreator.CreateCircle(_itemListController.GetInitItems(),
                _reticleController.ReticleTransform.position);
            _circleRotator.Init(_circleCreator.CircleCenter.transform, _circleCreator.AngleStep);
            
            //Subscribe
            SubscribeEvents();

            _isInitialized = true;
        }

        private void SubscribeEvents()
        {
            _productPresenter.StartPresentation += OnStartItemPresentation;
            _productPresenter.EndPresentation += OnEndItemPresentation;
            
            _swipeDetection.SwipeStartEvent += OnSwipeStartEvent;
            _swipeDetection.SwipeEvent += OnSwipeEvent;
            _swipeDetection.SwipeEndEvent += OnSwipeEndEvent;
            _swipeDetection.ShortSwipeEvent += OnShortSwipeEvent;
            
            _circleRotator.RightLimit += CircleRotatorOnRightLimit;
            _circleRotator.LeftLimit += CircleRotatorOnLeftLimit;
        }

        private void UnSubscribeEvents()
        {
            _productPresenter.StartPresentation -= OnStartItemPresentation;
            _productPresenter.EndPresentation -= OnEndItemPresentation;
            
            _swipeDetection.SwipeStartEvent -= OnSwipeStartEvent;
            _swipeDetection.SwipeEvent -= OnSwipeEvent;
            _swipeDetection.SwipeEndEvent -= OnSwipeEndEvent;
            _swipeDetection.ShortSwipeEvent -= OnShortSwipeEvent;
            
            _circleRotator.RightLimit -= CircleRotatorOnRightLimit;
            _circleRotator.LeftLimit -= CircleRotatorOnLeftLimit;
        }

        private void CircleRotatorOnLeftLimit()
        {
            _circleCreator.AddItemLeft(_itemListController.GetNextLeftItem());
        }

        private void CircleRotatorOnRightLimit()
        {
            _circleCreator.AddItemRight(_itemListController.GetNextRightItem());
        }

        private void OnSwipeStartEvent(Vector2 arg1, float arg2)
        {
            
        }

        private void OnShortSwipeEvent(SwipeSide direction, float power)
        {
            int step = Mathf.RoundToInt(_config.MaxRotationStep * power);

            if (direction == SwipeSide.Left)
            {
                _circleRotator.ToLeftLimit(step);
            }
            else if (direction == SwipeSide.Right)
            {
                _circleRotator.ToRightLimit(step);
            }
            
            CircleSwipeEvent?.Invoke();
        }

        private void OnSwipeEvent(Vector2 pos)
        {
            _circleRotator.Rotate(pos.x);
        }

        private void OnSwipeEndEvent(Vector2 arg1, float arg2)
        {
            _circleRotator.SmoothNearestLimit();
        }

        private void OnStartItemPresentation(Item item)
        {
            _circleRotator.StopRotation();
        }
        
        private void OnEndItemPresentation(Item item)
        {
            _circleRotator.StartRotation();
        }
        
        public void Update()
        {
            _circleRotator?.Update();
        }

        public void Reset()
        {
            if (!_isInitialized)
            {
                return;
            }
            
            _isInitialized = false;
            
            UnSubscribeEvents();
            
            _circleRotator = null;
            _itemListController.DestroyItems();
            _circleCreator.DestroyCircle();

            _circleCreator = null;
            _itemListController = null;
            _getItemList = null;

            CircleSwipeEvent = null;
        }
    }
}