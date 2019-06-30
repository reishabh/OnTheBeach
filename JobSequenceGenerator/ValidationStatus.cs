using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSequenceGenerator
{
    public enum ValidationStatus
    {
        Success = 0,
        No_Jobs = 1,
        Invalid_Job = 2,
        Jobs_cannot_depend_on_themselves = 3,
        Jobs_cannot_have_circular_dependencies = 4,
        Invalid_job_input_format = 5
    }
}
