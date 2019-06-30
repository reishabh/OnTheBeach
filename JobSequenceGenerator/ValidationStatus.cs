using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobProcessing
{
    public enum ValidationStatus
    {
        Success = 0,
        No_Jobs = 1,
        Invalid_Job = 2,
        Jobs_Cannot_Depend_On_Themselves = 3,
        Circular_Reference = 4
    }
}
