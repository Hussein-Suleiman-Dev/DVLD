namespace DVLD.Users
{
    partial class frmUserDetalis
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
            this.ctrlChangePasswordUser1 = new DVLD.Users.ctrlUserCard();
            this.SuspendLayout();
            // 
            // ctrlChangePasswordUser1
            // 
            this.ctrlChangePasswordUser1.Location = new System.Drawing.Point(2, 2);
            this.ctrlChangePasswordUser1.Name = "ctrlChangePasswordUser1";
            this.ctrlChangePasswordUser1.Size = new System.Drawing.Size(954, 486);
            this.ctrlChangePasswordUser1.TabIndex = 0;
            // 
            // frmUserDetalis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 500);
            this.Controls.Add(this.ctrlChangePasswordUser1);
            this.Name = "frmUserDetalis";
            this.Text = "frmUserDetalis";
            this.Load += new System.EventHandler(this.frmUserDetalis_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlUserCard ctrlChangePasswordUser1;
    }
}