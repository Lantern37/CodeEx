using System;
using UnityEngine;

namespace Engenious.Core
{
    [Serializable]
    public class SelectableProductInfo
    {
        public int Id;
        public string Name;
        
        private ProductResponse _productData;
        public ProductResponse ProductData => _productData;

        public void SetData(ProductResponse productData)
        {
            _productData = productData;
        }
    }
}