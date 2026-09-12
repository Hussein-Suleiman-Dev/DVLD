using BusinessLayer;
using DVLD.Global_Classes;
using DVLD.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.Control
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        int _ApplicationID;
        clsApplication _App;
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
         
        }
        public int ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
        }
        public void LoadApplicationInfo( int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            _App = clsApplication.Find(_ApplicationID);
            if (_App == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("Not Found Application To Fill It", "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            _Load();
        }

        private void _Load()
        {

        
            lblApplicationID.Text = _ApplicationID.ToString();
            lblStatus.Text = _App.ApplicationStatusText.ToString();
            lblFees.Text = _App.PaidFees.ToString();
            lblType.Text = _App.ApplicationInfo.ApplicationTypeName;
            lblDate.Text=  clsFormat.DateToShort( _App.ApplicationDate).ToString();
            lblStatusDate.Text= clsFormat.DateToShort(  _App.LastStatusDate).ToString();
            lblCreatedByUser.Text = _App.CreateByUserInfo.UserName;
            lblApplicant.Text=_App.ApplicationFullName.ToString();
        }
        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;

            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_App.ApplicationPersonID);
            frm.ShowDialog();

            //Refresh
            LoadApplicationInfo(_ApplicationID);
        }
    }
}
