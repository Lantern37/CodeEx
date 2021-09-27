using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Carousel;
using Shop.Core;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class CircleCreator
{
    public float AngleStep => _circlePlacer.AngleStep;
    
    private CreateItemsAroundConfig _config;

    private CirclePlacer _circlePlacer;
    
    private List<CircleItem> _items = new List<CircleItem>();

    private CircleCenter _circleCenter;
    public CircleCenter CircleCenter => _circleCenter;

    private CirclePortal _rightPortal;
    private CirclePortal _leftPortal;

    private CircleMiddlePoint _middlePoint;
    public CircleMiddlePoint MiddlePoint => _middlePoint;

    public CircleCreator(){}

    public CircleCreator(CreateItemsAroundConfig config)
    {
        _config = config;
    }
    
    /// <summary>
    /// Create circle of items  
    /// </summary>
    /// <param name="items">List of items to create on circle (first items)</param>
    /// <param name="centerPosition">Position of circle center</param>
    public void CreateCircle(List<Item> items, Vector3 centerPosition)
    {
        _circlePlacer = new CirclePlacer();
        _circlePlacer.Initialize(_config.PlacerConfig);

        centerPosition.y = _config.CircleHeight;
        
        _circleCenter = Object.Instantiate(_config.CircleCenter, centerPosition, Quaternion.identity);
         
        List<Transform> _circleItemsTransforms = new List<Transform>();

        for (int i = 0; i < items.Count; i++)
        {
            _items.Add(CreateItem(items[i]));
            _items[i].transform.SetParent(_circleCenter.transform);
            _circleItemsTransforms.Add(_items[i].transform);
        }
        
        _circlePlacer.PlaceItems(_circleItemsTransforms, _circleCenter.transform, Camera.main.transform.rotation.eulerAngles.y);
        
        _rightPortal = Object.Instantiate(_config.RightPortal);
        _leftPortal = Object.Instantiate(_config.LeftPortal);
        _middlePoint = Object.Instantiate(_config.MiddlePoint);
        
        _circlePlacer.AddItemAtIndex(_rightPortal.transform, _circleCenter.transform, _circlePlacer.RightIndex - 1);
        _circlePlacer.AddItemAtIndex(_leftPortal.transform, _circleCenter.transform, _circlePlacer.LeftIndex + 1);
        _circlePlacer.AddItemAtIndex(_middlePoint.transform, _circleCenter.transform, 0);
    }

    public void AddItemRight(Item item)
    {
        var oldItem = _items[_items.Count - 1];
        oldItem.DisableCircleItem();
        _items.Remove(oldItem);
        // RemoveItemLeft();
        // var newItem = CreateItem(item);
        oldItem.SetItem(item);

        _items.Insert(0, oldItem);

        //_items[0].transform.SetParent(_circleCenter.transform);

        _circlePlacer.AddItemRight(oldItem.transform, _circleCenter.transform);
    }

    public void AddItemLeft(Item item)
    {
        // RemoveItemRight();
        //
        // _items = _items.Where(x => x != null).ToList();
        
        // var newItem = CreateItem(item);
        var oldItem = _items[0];
        oldItem.DisableCircleItem();
        _items.Remove(oldItem);
        
        oldItem.SetItem(item);

        _items.Add(oldItem);
        
        //newItem.transform.SetParent(_circleCenter.transform);

        _circlePlacer.AddItemLeft(oldItem.transform, _circleCenter.transform);
    }

    private void RemoveItemRight()
    {
        var oldItem = _items[0];
        _items.Remove(oldItem);
        //oldItem.Delete();
        oldItem.DisableCircleItem();
    }

    private void RemoveItemLeft()
    {
        var oldItem = _items[_items.Count - 1];
        _items.Remove(oldItem);
        //oldItem.Delete();
        oldItem.DisableCircleItem();
    }

    public void DestroyCircle()
    {
        foreach (var item in _items)
        {
            item.Delete();
        }

        _items.Clear();
     
        if (_middlePoint != null)
            Object.Destroy(_middlePoint.gameObject);
        if (_rightPortal != null)

            Object.Destroy(_rightPortal.gameObject);

        if (_leftPortal != null)
            Object.Destroy(_leftPortal.gameObject);

        if (_circleCenter != null)
            Object.Destroy(_circleCenter.gameObject);
    }

    private CircleItem CreateItem()
    {
        return Object.Instantiate(_config.ItemPrefab);
    }
    
    private CircleItem CreateItem(Item item)
    {
        var circleItem = CreateItem();
        circleItem.SetItem(item);
        
        return circleItem;
    }
}

[Serializable]
public class CreateItemsAroundConfig
{
    public float CircleHeight;
    
    public CircleItem ItemPrefab;
        
    public CirclePortal RightPortal;
    public CirclePortal LeftPortal;

    public CircleMiddlePoint MiddlePoint;
    public CircleCenter CircleCenter;
    
    public CirclePlacerConfig PlacerConfig;
}

