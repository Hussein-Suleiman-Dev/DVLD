using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class LicenseClassDTO
    {

        public int LicenseClassIDDTO { get; set; }
        public string LicenseClassNameDTO { get; set; }
        public string DescriptionDTO { get; set; }
        public int MinimumAgeDTO { get; set; }
        public int DefaultValidityPeriodDTO { get; set; }
        public float ClassFeesDTO { get; set; }
    }
}
