using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class DetainedLicenseDTO
    {
       

        public int DetainID         { set; get; }
        public int LicenseID         { set; get; }
    
    public DateTime DetainDate      { set; get; }

       public float FineFees        { set; get; }
         public int CreatedByUserID { set; get; }
     
        public bool IsReleased      { set; get; }
    public DateTime ReleaseDate      { set; get; }
        public int ReleasedByUserID  { set; get; }
      
        public int ReleaseApplicationID { set; get; }


    }
}
