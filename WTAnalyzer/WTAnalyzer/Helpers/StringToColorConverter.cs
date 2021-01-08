using System;
using System.Globalization;
using Xamarin.Forms;

namespace WTAnalyzer.Helpers
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.White;
            string colorStr = value.ToString();
            switch (colorStr)
            {
                case "Premium":
                    color = Color.FromHex("#fcf1d2");
                    break;
                case "Promotional":
                    color = Color.FromHex("#fbdede");
                    break;
                case "Squadron":
                    color = Color.FromHex("#e2fadf");
                    break;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.White;
        }
    }
}
