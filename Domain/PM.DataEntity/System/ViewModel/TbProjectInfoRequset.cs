using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.DataEntity.System.ViewModel
{
    public class TbProjectInfoRequset : PageSearchRequest
    {
        public int ID { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
