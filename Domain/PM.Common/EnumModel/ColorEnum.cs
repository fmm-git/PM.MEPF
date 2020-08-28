using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Common.EnumModel
{
    public enum ColorEnum
    {
        [Description("Green")]
        绿色,
        [Description("Yellow")]
        黄色,
        [Description("Red")]
        红色,
        [Description("blue")]
        蓝色,
        [Description("#F75000")]
        橘黄色,
        [Description("#8E8E8E")]
        灰色,
    }
}
