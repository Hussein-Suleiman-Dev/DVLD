namespace DVLD.Tests
{
    partial class frmScheduleTest
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
            this.btnClose = new System.Windows.Forms.Button();
            this.crlScheduleTest1 = new DVLD.Tests.Control.ctrlScheduleTest();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnClose.Location = new System.Drawing.Point(364, 723);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(153, 52);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // crlScheduleTest1
            // 
            this.crlScheduleTest1.Location = new System.Drawing.Point(-2, -2);
            this.crlScheduleTest1.Name = "crlScheduleTest1";
            this.crlScheduleTest1.Size = new System.Drawing.Size(592, 710);
            this.crlScheduleTest1.TabIndex = 0;
            this.crlScheduleTest1.TestType = BusinessLayer.clsTestType.enTestType.WrittenTest;
            this.crlScheduleTest1.Load += new System.EventHandler(this.crlScheduleTest1_Load);
            // 
            // frmScheduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 787);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.crlScheduleTest1);
            this.Name = "frmScheduleTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmScheduleTest";
            this.Load += new System.EventHandler(this.frmScheduleTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Control.ctrlScheduleTest crlScheduleTest1;
        private System.Windows.Forms.Button btnClose;
    }
}