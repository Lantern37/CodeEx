using System;
using System.Collections;
using System.Collections.Generic;
using Engenious.Core;
using Engenious.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// What im doing????
/// </summary>
public class FilterWindow : WindowController
{
    [SerializeField]
    private FilterWindowData _data;
    public FilterWindowData Data => _data;


    [SerializeField] private InputField _search;
    
    [SerializeField] private List<FilterToggle> _typesTriggers;
    [SerializeField] private List<FilterToggle> _strainTriggers;
    [SerializeField] private List<FilterToggle> _brandTriggers;

    [SerializeField] private Button _apply;
    [SerializeField] private Button _back;
    [SerializeField] private Button _clear;

    public event Action Aplly;
    protected override void Show(params object[] _params)
    {
        base.Show(_params);

        _data = new FilterWindowData();
        _apply.onClick.AddListener((() => ApplyFilters()));
        _clear.onClick.AddListener((() => ClearFilters()));
        _back.onClick.AddListener(()=> Close());
        
        foreach (var item in _typesTriggers)
        {
            item.ResetToggle();
            item.On += OnTypes;
            item.Off += OffTypes;
        }
        
        foreach (var item in _strainTriggers)
        {
            item.ResetToggle();
            item.On += OnStrain;
            item.Off += OffStrain;
        }
        
        foreach (var item in _brandTriggers)
        {
            item.ResetToggle();
            item.On += OnBrands;
            item.Off += OffBrands;
        }
    }

    private void OnTypes(FilterToggle type)
    {
        if (!_data.Types.Contains(type.Index))
        {
            _data.Types.Add(type.Index);
        }
    }
    
    private void OffTypes(FilterToggle type)
    {
        if (_data.Types.Contains(type.Index))
        {
            _data.Types.Remove(type.Index);
        }
    }

    private void OnBrands(FilterToggle type)
    {
        if (!_data.Brands.Contains(type.Index))
        {
            _data.Brands.Add(type.Index);
        }
    }

    private void OffBrands(FilterToggle type)
    {
        if (_data.Brands.Contains(type.Index))
        {
            _data.Brands.Remove(type.Index);
        }
    }

    private void OnStrain(FilterToggle type)
    {
        if (!_data.Strain.Contains(type.Index))
        {
            _data.Strain.Add(type.Index);
        }
    }

    private void OffStrain(FilterToggle type)
    {
        if (_data.Strain.Contains(type.Index))
        {
            _data.Strain.Remove(type.Index);
        }
    }

    private void ApplyFilters()
    {
        _data.Search = _search.text;
        Aplly?.Invoke();
    }
    
    private void ClearFilters()
    {
        _data.Clear();
        
        foreach (var item in _typesTriggers)
        {
            item.ResetToggle();
        }
        
        foreach (var item in _strainTriggers)
        {
            item.ResetToggle();
        }
        
        foreach (var item in _brandTriggers)
        {
            item.ResetToggle();
        }
    }
    
    protected override void Closed()
    {
        _apply.onClick.RemoveAllListeners();
        //Aplly = null;
        _back.onClick.RemoveAllListeners();
        _clear.onClick.RemoveAllListeners();
        
        foreach (var item in _typesTriggers)
        {
            item.On -= OnTypes;
            item.Off -= OffTypes;
        }
        
        foreach (var item in _strainTriggers)
        {
            item.On -= OnStrain;
            item.Off -= OffStrain;
        }
        
        foreach (var item in _brandTriggers)
        {
            item.On -= OnBrands;
            item.Off -= OffBrands;
        }
        base.Closed();
    }
}

[Serializable]
public class FilterWindowData
{
    public string Search;
    public List<int> Types;
    public List<int> Strain;
    public List<int> Brands;

    public FilterWindowData()
    {
        Types = new List<int>();
        Strain = new List<int>();
        Brands = new List<int>();
    }
    
    public void Clear()
    {
        Search = String.Empty;
        Types = null;
        Strain = null;
        Brands = null;
    }
}
