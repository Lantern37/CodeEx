namespace Assets.Scripts.Utilities
{
    public static class PriceForm
    {
        public static string GetFormatedPrice(string price)
        {
            return "$" + string.Format("{0:F2}", price);
        }
    }
}