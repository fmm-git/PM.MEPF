using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.DataEntity.System.ViewModel
{
    public class TbBzhGjInfoRequest : PageSearchRequest
    {
        public string ProCode { get; set; }
        public string ProName { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
    }
}
