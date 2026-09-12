using DVLD.People.Controls;

namespace DVLD
{
    partial class frmAddUpdateLocalDrivingLicense
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnNextTabPage = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbLocalDivingLicense = new Guna.UI2.WinForms.Guna2TabControl();
            this.tbPersonInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlPersonInfoWithFilter2 = new DVLD.People.Controls.ctrlPersonInfoWithFilter();
            this.tbApplicationInfon = new System.Windows.Forms.TabPage();
            this.cmbLicenseClass = new System.Windows.Forms.ComboBox();
            this.lblCreatedUser = new System.Windows.Forms.Label();
            this.lblAppFees = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.lblAppID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLocalDivingLicense.SuspendLayout();
            this.tbPersonInfo.SuspendLayout();
            this.tbApplicationInfon.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(292, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(525, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "New Local Driving License Application";
            // 
            // btnNextTabPage
            // 
            this.btnNextTabPage.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNextTabPage.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNextTabPage.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNextTabPage.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNextTabPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNextTabPage.ForeColor = System.Drawing.Color.White;
            this.btnNextTabPage.Location = new System.Drawing.Point(880, 540);
            this.btnNextTabPage.Name = "btnNextTabPage";
            this.btnNextTabPage.Size = new System.Drawing.Size(174, 38);
            this.btnNextTabPage.TabIndex = 6;
            this.btnNextTabPage.Text = "Next";
            // 
            // btnSave
            // 
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1098, 607);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 36);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(911, 607);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(141, 36);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(435, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(497, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "New Local Driving License Application";
            // 
            // tbLocalDivingLicense
            // 
            this.tbLocalDivingLicense.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tbLocalDivingLicense.Controls.Add(this.tbPersonInfo);
            this.tbLocalDivingLicense.Controls.Add(this.tbApplicationInfon);
            this.tbLocalDivingLicense.ItemSize = new System.Drawing.Size(180, 40);
            this.tbLocalDivingLicense.Location = new System.Drawing.Point(7, 45);
            this.tbLocalDivingLicense.Name = "tbLocalDivingLicense";
            this.tbLocalDivingLicense.SelectedIndex = 0;
            this.tbLocalDivingLicense.Size = new System.Drawing.Size(1274, 554);
            this.tbLocalDivingLicense.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tbLocalDivingLicense.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tbLocalDivingLicense.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tbLocalDivingLicense.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tbLocalDivingLicense.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tbLocalDivingLicense.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tbLocalDivingLicense.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tbLocalDivingLicense.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tbLocalDivingLicense.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tbLocalDivingLicense.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tbLocalDivingLicense.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tbLocalDivingLicense.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tbLocalDivingLicense.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tbLocalDivingLicense.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tbLocalDivingLicense.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tbLocalDivingLicense.TabButtonSize = new System.Drawing.Size(180, 40);
            this.tbLocalDivingLicense.TabIndex = 3;
            this.tbLocalDivingLicense.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            // 
            // tbPersonInfo
            // 
            this.tbPersonInfo.Controls.Add(this.btnNext);
            this.tbPersonInfo.Controls.Add(this.ctrlPersonInfoWithFilter2);
            this.tbPersonInfo.Location = new System.Drawing.Point(184, 4);
            this.tbPersonInfo.Name = "tbPersonInfo";
            this.tbPersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tbPersonInfo.Size = new System.Drawing.Size(1086, 546);
            this.tbPersonInfo.TabIndex = 0;
            this.tbPersonInfo.Text = "Person Info";
            this.tbPersonInfo.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(874, 498);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(141, 36);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click_1);
            // 
            // ctrlPersonInfoWithFilter2
            // 
            this.ctrlPersonInfoWithFilter2.EnabledFilter = true;
            this.ctrlPersonInfoWithFilter2.Location = new System.Drawing.Point(31, 16);
            this.ctrlPersonInfoWithFilter2.Name = "ctrlPersonInfoWithFilter2";
            this.ctrlPersonInfoWithFilter2.ShowAddPerson = true;
            this.ctrlPersonInfoWithFilter2.Size = new System.Drawing.Size(999, 476);
            this.ctrlPersonInfoWithFilter2.TabIndex = 0;
            // 
            // tbApplicationInfon
            // 
            this.tbApplicationInfon.Controls.Add(this.cmbLicenseClass);
            this.tbApplicationInfon.Controls.Add(this.lblCreatedUser);
            this.tbApplicationInfon.Controls.Add(this.lblAppFees);
            this.tbApplicationInfon.Controls.Add(this.lblAppDate);
            this.tbApplicationInfon.Controls.Add(this.lblAppID);
            this.tbApplicationInfon.Controls.Add(this.label6);
            this.tbApplicationInfon.Controls.Add(this.label5);
            this.tbApplicationInfon.Controls.Add(this.label4);
            this.tbApplicationInfon.Controls.Add(this.label3);
            this.tbApplicationInfon.Controls.Add(this.label2);
            this.tbApplicationInfon.Location = new System.Drawing.Point(184, 4);
            this.tbApplicationInfon.Name = "tbApplicationInfon";
            this.tbApplicationInfon.Padding = new System.Windows.Forms.Padding(3);
            this.tbApplicationInfon.Size = new System.Drawing.Size(1086, 546);
            this.tbApplicationInfon.TabIndex = 1;
            this.tbApplicationInfon.Text = "Application Info";
            this.tbApplicationInfon.UseVisualStyleBackColor = true;
            // 
            // cmbLicenseClass
            // 
            this.cmbLicenseClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLicenseClass.FormattingEnabled = true;
            this.cmbLicenseClass.Location = new System.Drawing.Point(470, 243);
            this.cmbLicenseClass.Name = "cmbLicenseClass";
            this.cmbLicenseClass.Size = new System.Drawing.Size(309, 30);
            this.cmbLicenseClass.TabIndex = 9;
            // 
            // lblCreatedUser
            // 
            this.lblCreatedUser.AutoSize = true;
            this.lblCreatedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedUser.Location = new System.Drawing.Point(476, 392);
            this.lblCreatedUser.Name = "lblCreatedUser";
            this.lblCreatedUser.Size = new System.Drawing.Size(68, 22);
            this.lblCreatedUser.TabIndex = 8;
            this.lblCreatedUser.Text = "label10";
            // 
            // lblAppFees
            // 
            this.lblAppFees.AutoSize = true;
            this.lblAppFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppFees.Location = new System.Drawing.Point(476, 328);
            this.lblAppFees.Name = "lblAppFees";
            this.lblAppFees.Size = new System.Drawing.Size(58, 22);
            this.lblAppFees.TabIndex = 7;
            this.lblAppFees.Text = "label9";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppDate.Location = new System.Drawing.Point(466, 187);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(58, 22);
            this.lblAppDate.TabIndex = 6;
            this.lblAppDate.Text = "label8";
            // 
            // lblAppID
            // 
            this.lblAppID.AutoSize = true;
            this.lblAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppID.ForeColor = System.Drawing.Color.Red;
            this.lblAppID.Location = new System.Drawing.Point(466, 123);
            this.lblAppID.Name = "lblAppID";
            this.lblAppID.Size = new System.Drawing.Size(40, 22);
            this.lblAppID.TabIndex = 5;
            this.lblAppID.Text = "???";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(264, 389);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 25);
            this.label6.TabIndex = 4;
            this.label6.Text = "Created By";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(217, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "Application Fees";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(221, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 25);
            this.label4.TabIndex = 2;
            this.label4.Text = "Application Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(239, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "License Class";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(207, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "D.L Application ID";
            // 
            // frmAddUpdateLocalDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 645);
            this.Controls.Add(this.tbLocalDivingLicense);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Name = "frmAddUpdateLocalDrivingLicense";
            this.Text = "frmAddUpdateLocalDrivingLicense";
            this.Activated += new System.EventHandler(this.frmAddUpdateLocalDrivingLicense_Activated);
            this.Load += new System.EventHandler(this.frmAddUpdateLocalDrivingLicense_Load);
            this.tbLocalDivingLicense.ResumeLayout(false);
            this.tbPersonInfo.ResumeLayout(false);
            this.tbApplicationInfon.ResumeLayout(false);
            this.tbApplicationInfon.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnNextTabPage;
        private People.Controls.ctrlPersonInfoWithFilter ctrlPersonInfoWithFilter1;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2TabControl tbLocalDivingLicense;
        private System.Windows.Forms.TabPage tbPersonInfo;
        private System.Windows.Forms.Button btnNext;
        private ctrlPersonInfoWithFilter ctrlPersonInfoWithFilter2;
        private System.Windows.Forms.TabPage tbApplicationInfon;
        private System.Windows.Forms.ComboBox cmbLicenseClass;
        private System.Windows.Forms.Label lblCreatedUser;
        private System.Windows.Forms.Label lblAppFees;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label lblAppID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}