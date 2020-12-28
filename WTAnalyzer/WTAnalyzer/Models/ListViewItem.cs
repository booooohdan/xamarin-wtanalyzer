using System;
using System.Collections.Generic;
using System.Text;

namespace WTAnalyzer.Models
{
    public class ListViewItem
    {
        public ListViewItem(string flag, string role, string vtype, string name, object value, object br)
        {
            Flag = flag;
            Role = role;
            Vtype = vtype;
            Name = name;
            Value = value;
            Br = br;
        }

        public string Flag { get; set; }
        public string Role { get; set; }
        public string Vtype { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public object Br { get; set; }
    }
}
