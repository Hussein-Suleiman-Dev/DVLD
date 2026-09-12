using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class LicenseDTO
    {

        public int LiceeneseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicensesClassID { get; set; }
        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }
         public string Notes { get; set; }
        public float PaidFess { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }

        public int CreatedByUserID { get; set; }


    }
}
