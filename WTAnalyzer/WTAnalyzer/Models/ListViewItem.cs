namespace WTAnalyzer.Models
{
    public class ListViewItem
    {
        public ListViewItem(string flag, string role, string vtype, string name, object value, string valueDescription, object br, object id)
        {
            Flag = flag;
            Role = role;
            Vtype = vtype;
            Name = name;
            Value = value;
            ValueDescription = valueDescription;
            Br = br;
            Id = id;
        }

        public string Flag { get; set; }
        public string Role { get; set; }
        public string Vtype { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public string ValueDescription { get; set; }
        public object Br { get; set; }
        public object Id { get; set; }
    }
}
