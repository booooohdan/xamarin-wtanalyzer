using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
                    color = Color.FromHex("#ffecb4");
                    break;
                case "Promotional":
                    color = Color.FromHex("#ffecb4");
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
