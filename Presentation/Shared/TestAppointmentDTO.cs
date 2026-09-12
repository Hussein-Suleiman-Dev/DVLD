using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class TestAppointmentDTO
    {

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int  LocalDrivingLicenseAppID { get; set; }

        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
    }
}
