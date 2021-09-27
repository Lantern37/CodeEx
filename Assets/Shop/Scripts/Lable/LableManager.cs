using System.Collections;
using System.Collections.Generic;
using Shop.Core;
using UnityEngine;

public class LableManager : MonoBehaviour
{
    private List<ItemPathParent> m_CurrentLabel;
    
    [SerializeField] private GameObject m_CurePrefab;
    [SerializeField] private GameObject m_BloomPrefab;
    [SerializeField] private GameObject m_PlugPrefab;
    [SerializeField] private GameObject m_AlmoraPrefab;

    public List<ItemPathParent> GetCurrentLable(LableType lableType)
    {
        return m_CurrentLabel;
    }
}
