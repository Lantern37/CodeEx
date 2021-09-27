using System.Collections.Generic;

namespace Engenious.MainScene
{
    public class ProductDecriptionViewData
    {
        public string Name;
        public string Sort;

        public float Price;
        public float Thc;
        
        public List<string> Effects;

        public ProductDecriptionViewData(){}
        
        public ProductDecriptionViewData(string name, string sort, float price, List<string> effects)
        {
            Name = name;
            Sort = sort;
            Price = price;
            Effects = effects;
        }
    }
}