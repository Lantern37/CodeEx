using UnityEngine;

namespace Assets.Scripts.MainWindows.Cart
{
    public struct CartItemViewData
    {
        public int ProductId;

        public string ProductName;
        public string BrandName;

        public string ImageLink;
        
        public float Price;
   
        private int _count;

        public int Count
        {
            get
            {
                return _count;
            }

            set
            {
                if (value <= 0)
                {
                    _count = 0;
                }
                else
                {
                    _count = value;
                }
            }
        }
        
        public Sprite ProductSprite;
    }
}