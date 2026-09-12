using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonInfoWithFilter : UserControl
    {

        public event Action<int> OnPersonSelected;

        public virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            { 
            handler(PersonID);
            
            }

        }
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson 
        {
            get {
            return _ShowAddPerson;
            }
            set 
            {
            _ShowAddPerson=value;
                btnAddPerson.Visible = _ShowAddPerson;
            }
        }
        
        private bool _EnabledFilter=true;

        public bool EnabledFilter 
        {
            get { return _EnabledFilter; }
            set {
                _EnabledFilter = value;
            gbFilters.Enabled = _EnabledFilter;
            }
        
        }
        private int _PersonID = -1;
        public int PersonID
        {
            get { return ctrlPersonInfo1.PersonID; }
        }
        public clsPeople SelectPerosnInfo 
        {
            get { return ctrlPersonInfo1.SelectedPersonInfo; }
        }
        public void LoadPersonInfo(int PersonID)
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text=PersonID.ToString();
            _PersonID= PersonID;
            _FindNow();
        }
        private void _FindNow()
        {
            switch (cmbFilterBy.Text)
            {
                case "Person ID":
                    
                    ctrlPersonInfo1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "National No":
                    ctrlPersonInfo1.LoadPersonInfo(txtFilterValue.Text);
                    break;
            }
            if (OnPersonSelected != null && _EnabledFilter)
            {
                OnPersonSelected(ctrlPersonInfo1.PersonID);
            
            }
        }

        public ctrlPersonInfoWithFilter()
        {
            InitializeComponent();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFilterValue.Text != "")
            {
                _FindNow();
                return;            
            }
            MessageBox.Show("Please Enter The Value ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ctrlPersonInfoWithFilter_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "Field Required");

            }
            else { errorProvider1.SetError(txtFilterValue,null); }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm=new frmAddUpdatePerson();
            frm.DataBack += DataBack;
            frm.ShowDialog();
        }
        private void DataBack(object sender,int PeronID)
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = PeronID.ToString();
            ctrlPersonInfo1.LoadPersonInfo(PeronID);


        }
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
    }
}
