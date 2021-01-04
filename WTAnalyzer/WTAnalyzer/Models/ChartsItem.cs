namespace WTAnalyzer.Models
{
    public class ChartsItem
    {
        public object XValue { get; set; }
        public object YValue { get; set; }

        public ChartsItem(object xVal, object yVal)
        {
            XValue = xVal;
            YValue = yVal;
        }
    }
}
