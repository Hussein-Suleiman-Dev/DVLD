namespace DVLD
{
    partial class frmListLocalDrivingLicense
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvLocalDrivingLicense = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showApplicationDetailsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editApplicationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteApplicationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cancleApplicationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sechduleTestsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.visionTestToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.writtenTestToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.streetTestToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.issueDrivingLicenseFirstTimeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicense)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLocalDrivingLicense
            // 
            this.dgvLocalDrivingLicense.AllowUserToAddRows = false;
            this.dgvLocalDrivingLicense.AllowUserToDeleteRows = false;
            this.dgvLocalDrivingLicense.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalDrivingLicense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalDrivingLicense.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvLocalDrivingLicense.Location = new System.Drawing.Point(0, 254);
            this.dgvLocalDrivingLicense.Name = "dgvLocalDrivingLicense";
            this.dgvLocalDrivingLicense.ReadOnly = true;
            this.dgvLocalDrivingLicense.RowHeadersWidth = 51;
            this.dgvLocalDrivingLicense.RowTemplate.Height = 24;
            this.dgvLocalDrivingLicense.Size = new System.Drawing.Size(1357, 320);
            this.dgvLocalDrivingLicense.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showApplicationDetailsToolStripMenuItem1,
            this.editApplicationToolStripMenuItem1,
            this.deleteApplicationToolStripMenuItem1,
            this.cancleApplicationToolStripMenuItem1,
            this.sechduleTestsToolStripMenuItem1,
            this.issueDrivingLicenseFirstTimeToolStripMenuItem1,
            this.showLicenseToolStripMenuItem1,
            this.showPersonLicenseHistoryToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(293, 196);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.cmsApplication);
            // 
            // showApplicationDetailsToolStripMenuItem1
            // 
            this.showApplicationDetailsToolStripMenuItem1.Name = "showApplicationDetailsToolStripMenuItem1";
            this.showApplicationDetailsToolStripMenuItem1.Size = new System.Drawing.Size(292, 24);
            this.showApplicationDetailsToolStripMenuItem1.Text = "Show Application Details";
            this.showApplicationDetailsToolStripMenuItem1.Click += new System.EventHandler(this.showApplicationDetailsToolStripMenuItem1_Click);
            // 
            // editApplicationToolStripMenuItem1
            // 
            this.editApplicationToolStripMenuItem1.Name = "editApplicationToolStripMenuItem1";
            this.editApplicationToolStripMenuItem1.Size = new System.Drawing.Size(292, 24);
            this.editApplicationToolStripMenuItem1.Text = "Edit Application";
            this.editApplicationToolStripMenuItem1.Click += new System.EventHandler(this.editApplicationToolStripMenuItem1_Click);
            // 
            // deleteApplicationToolStripMenuItem1
            // 
            this.deleteApplicationToolStripMenuItem1.Name = "deleteApplicationToolStripMenuItem1";
            this.deleteApplicationToolStripMenuItem1.Size = new System.Drawing.Size(292, 24);
            this.deleteApplicationToolStripMenuItem1.Text = "Delete Application";
            this.deleteApplicationToolStripMenuItem1.Click += new System.EventHandler(this.deleteApplicationToolStripMenuItem1_Click);
            // 
            // cancleApplicationToolStripMenuItem1
            // 
            this.cancleApplicationToolStripMenuItem1.Name = "cancleApplicationToolStripMenuItem1";
            this.cancleApplicationToolStripMenuItem1.Size = new System.Drawing.Size(292, 24);
            this.cancleApplicationToolStripMenuItem1.Text = "Cancle Application";
            this.cancleApplicationToolStripMenuItem1.Click += new System.EventHandler(this.cancleApplicationToolStripMenuItem1_Click);
            // 
            // sechduleTestsToolStripMenuItem1
            // 
            this.sechduleTestsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visionTestToolStripMenuItem1,
            this.writtenTestToolStripMenuItem1,
            this.streetTestToolStripMenuItem1});
            this.sechduleTestsToolStripMenuItem1.Name = "sechduleTestsToolStripMenuItem1";
            this.sechduleTestsToolStripMenuItem1.Size = new System.Drawing.Size(292, 24);
            this.sechduleTestsToolStripMenuItem1.Text = "Sechdule Tests";
            // 
            // visionTestToolStripMenuItem1
            // 
            this.visionTestToolStripMenuItem1.Enabled = false;
            this.visionTestToolStripMenuItem1.Image = global::DVLD.Properties.Resources.Vision_Test_32;
            this.visionTestToolStripMenuItem1.Name = "visionTestToolStripMenuItem1";
            this.visionTestToolStripMenuItem1.Size = new System.Drawing.Size(171, 26);
            this.visionTestToolStripMenuItem1.Text = "Vision Test";
            this.visionTestToolStripMenuItem1.Click += new System.EventHandler(this.visionTestToolStripMenuItem1_Click);
            // 
            // writtenTestToolStripMenuItem1
            // 
            this.writtenTestToolStripMenuItem1.Enabled = false;
            this.writtenTestToolStripMenuItem1.Image = global::DVLD.Properties.Resources.Written_Test_321;
            this.writtenTestToolStripMenuItem1.Name = "writtenTestToolStripMenuItem1";
            this.writtenTestToolStripMenuItem1.Size = new System.Drawing.Size(171, 26);
            this.writtenTestToolStripMenuItem1.Text = "Written Test";
            this.writtenTestToolStripMenuItem1.Click += new System.EventHandler(this.writtenTestToolStripMenuItem1_Click);
            // 
            // streetTestToolStripMenuItem1
            // 
            this.streetTestToolStripMenuItem1.Enabled = false;
            this.streetTestToolStripMenuItem1.Image = global::DVLD.Properties.Resources.Street_Test_321;
            this.streetTestToolStripMenuItem1.Name = "streetTestToolStripMenuItem1";
            this.streetTestToolStripMenuItem1.Size = new System.Drawing.Size(171, 26);
            this.streetTestToolStripMenuItem1.Text = "Street Test";
            this.streetTestToolStripMenuItem1.Click += new System.EventHandler(this.streetTestToolStripMenuItem1_Click);
            // 
            // issueDrivingLicenseFirstTimeToolStripMenuItem1
            // 
            this.issueDrivingLicenseFirstTimeToolStripMenuItem1.Enabled = false;
            this.issueDrivingLicenseFirstTimeToolStripMenuItem1.Name = "issueDrivingLicenseFirstTimeToolStripMenuItem1";
            this.issueDrivingLicenseFirstTimeToolStripMenuItem1.Size = new System.Drawing.Size(292, 24);
            this.issueDrivingLicenseFirstTimeToolStripMenuItem1.Text = "Issue Driving License (First Time)";
            this.issueDrivingLicenseFirstTimeToolStripMenuItem1.Click += new System.EventHandler(this.issueDrivingLicenseFirstTimeToolStripMenuItem1_Click);
            // 
            // showLicenseToolStripMenuItem1
            // 
            this.showLicenseToolStripMenuItem1.Enabled = false;
            this.showLicenseToolStripMenuItem1.Name = "showLicenseToolStripMenuItem1";
            this.showLicenseToolStripMenuItem1.Size = new System.Drawing.Size(292, 24);
            this.showLicenseToolStripMenuItem1.Text = "Show License";
            this.showLicenseToolStripMenuItem1.Click += new System.EventHandler(this.showLicenseToolStripMenuItem1_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem1
            // 
            this.showPersonLicenseHistoryToolStripMenuItem1.Enabled = false;
            this.showPersonLicenseHistoryToolStripMenuItem1.Name = "showPersonLicenseHistoryToolStripMenuItem1";
            this.showPersonLicenseHistoryToolStripMenuItem1.Size = new System.Drawing.Size(292, 24);
            this.showPersonLicenseHistoryToolStripMenuItem1.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem1.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter By";
            // 
            // cmbFilter
            // 
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "None",
            "L.D.L AppID",
            "National No",
            "Full Name",
            "Status"});
            this.cmbFilter.Location = new System.Drawing.Point(83, 199);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(155, 24);
            this.cmbFilter.TabIndex = 3;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(273, 202);
            this.txtFilterValue.Multiline = true;
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(320, 24);
            this.txtFilterValue.TabIndex = 4;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 588);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "#Record";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(126, 588);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(58, 22);
            this.lblCount.TabIndex = 6;
            this.lblCount.Text = "label3";
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Items.AddRange(new object[] {
            "New",
            "Cancled",
            "Completed"});
            this.cmbFilterStatus.Location = new System.Drawing.Point(273, 199);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(209, 24);
            this.cmbFilterStatus.TabIndex = 7;
            this.cmbFilterStatus.Visible = false;
            this.cmbFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStatus_SelectedIndexChanged);
            // 
            // frmListLocalDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 613);
            this.Controls.Add(this.cmbFilterStatus);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvLocalDrivingLicense);
            this.Name = "frmListLocalDrivingLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frnListLocalDrivingLicense";
            this.Load += new System.EventHandler(this.frmListLocalDrivingLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicense)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLocalDrivingLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showApplicationDetailsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editApplicationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteApplicationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cancleApplicationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sechduleTestsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem visionTestToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem writtenTestToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem streetTestToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem issueDrivingLicenseFirstTimeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem1;
    }
}