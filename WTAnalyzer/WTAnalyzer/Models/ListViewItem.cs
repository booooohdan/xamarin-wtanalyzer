using System;
using System.Collections.Generic;
using System.Text;

namespace WTAnalyzer.Models
{
    public class ListViewItem
    {
        public ListViewItem(string flag, string name, object value)
        {
            Flag = flag;
            Name = name;
            Value = value;
        }

        public string Flag { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
