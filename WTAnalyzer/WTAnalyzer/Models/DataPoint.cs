using System;
using System.Collections.Generic;
using System.Text;

namespace WTAnalyzer.Models
{
    public class DataPoint
    {
        public object XValue { get; set; }
        public object YValue { get; set; }

        public DataPoint(object xVal, object yVal)
        {
            XValue = xVal;
            YValue = yVal;
        }
    }
}
