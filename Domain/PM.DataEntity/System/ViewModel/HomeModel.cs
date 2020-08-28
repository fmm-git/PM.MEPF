using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.DataEntity.System.ViewModel
{
    public class OrgLabData
    {
        public string OrgCode { get; set; }
        public string pCode { get; set; }
        public int LeveL { get; set; }
        public List<OrgLab> LabList { get; set; }
    }
    public class OrgLab
    {
        public OrgLab()
        {
        }
        public OrgLab(string name, int value, string color, int Order = 0)
        {
            this.Name = name;
            this.Value = value;
            this.Color = color;
            this.Order = Order;
        }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Color { get; set; }
        public int Order { get; set; }
    }
}
