using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        int _PersonID = -1;
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID= PersonID;
            this.Text = "Person License History";
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonInfoWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonInfoWithFilter1.EnabledFilter = false;
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);

                



            }
            else 
            {
                ctrlPersonInfoWithFilter1.EnabledFilter = true;
                ctrlPersonInfoWithFilter1.FilterFocus();
            }
        }

        private void ctrlPersonInfoWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID= obj;
            if (_PersonID == -1)
            { 
            ctrlDriverLicenses1.Clear();
            }
            else { ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID) ; }
        }
    }
}
