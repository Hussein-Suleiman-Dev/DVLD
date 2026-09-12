namespace DVLD
{
    partial class frmManagePeople
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
            this.dvgAllPeople = new System.Windows.Forms.DataGridView();
            this.UpdateToolsStripItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.shToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCountRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnFrmAdd = new System.Windows.Forms.Button();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AddNewPersonItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dvgAllPeople)).BeginInit();
            this.UpdateToolsStripItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.AddNewPersonItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvgAllPeople
            // 
            this.dvgAllPeople.AllowUserToAddRows = false;
            this.dvgAllPeople.AllowUserToDeleteRows = false;
            this.dvgAllPeople.AllowUserToOrderColumns = true;
            this.dvgAllPeople.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dvgAllPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgAllPeople.ContextMenuStrip = this.UpdateToolsStripItem;
            this.dvgAllPeople.Location = new System.Drawing.Point(29, 310);
            this.dvgAllPeople.Name = "dvgAllPeople";
            this.dvgAllPeople.ReadOnly = true;
            this.dvgAllPeople.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dvgAllPeople.RowHeadersWidth = 51;
            this.dvgAllPeople.RowTemplate.Height = 24;
            this.dvgAllPeople.Size = new System.Drawing.Size(1444, 287);
            this.dvgAllPeople.TabIndex = 1;
            this.dvgAllPeople.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgAllPeople_CellContentClick);
            // 
            // UpdateToolsStripItem
            // 
            this.UpdateToolsStripItem.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.UpdateToolsStripItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.phoneToolStripMenuItem,
            this.sendEmailToolStripMenuItem});
            this.UpdateToolsStripItem.Name = "contextMenuStrip1";
            this.UpdateToolsStripItem.Size = new System.Drawing.Size(161, 148);
            // 
            // shToolStripMenuItem
            // 
            this.shToolStripMenuItem.Name = "shToolStripMenuItem";
            this.shToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.shToolStripMenuItem.Text = "ShowDetails";
            this.shToolStripMenuItem.Click += new System.EventHandler(this.shToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // phoneToolStripMenuItem
            // 
            this.phoneToolStripMenuItem.Name = "phoneToolStripMenuItem";
            this.phoneToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.phoneToolStripMenuItem.Text = "Phone";
            // 
            // sendEmailToolStripMenuItem
            // 
            this.sendEmailToolStripMenuItem.Name = "sendEmailToolStripMenuItem";
            this.sendEmailToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.sendEmailToolStripMenuItem.Text = "Send Email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 625);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "# Records";
            // 
            // lblCountRecords
            // 
            this.lblCountRecords.AutoSize = true;
            this.lblCountRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountRecords.Location = new System.Drawing.Point(185, 625);
            this.lblCountRecords.Name = "lblCountRecords";
            this.lblCountRecords.Size = new System.Drawing.Size(17, 18);
            this.lblCountRecords.TabIndex = 5;
            this.lblCountRecords.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(633, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 38);
            this.label2.TabIndex = 6;
            this.label2.Text = "Manage People";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DVLD.Properties.Resources.group;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(584, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(367, 156);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // btnFrmAdd
            // 
            this.btnFrmAdd.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnFrmAdd.BackgroundImage = global::DVLD.Properties.Resources.add_group__1_;
            this.btnFrmAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFrmAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFrmAdd.Location = new System.Drawing.Point(1295, 224);
            this.btnFrmAdd.Name = "btnFrmAdd";
            this.btnFrmAdd.Size = new System.Drawing.Size(117, 52);
            this.btnFrmAdd.TabIndex = 2;
            this.btnFrmAdd.UseVisualStyleBackColor = false;
            this.btnFrmAdd.Click += new System.EventHandler(this.btnFrmAdd_Click);
            // 
            // cmbFilter
            // 
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "None",
            "Person Id",
            "National No.",
            "First Name",
            "Second Name",
            "Last Name",
            "Nationality",
            "Gender",
            "Phone",
            "Email"});
            this.cmbFilter.Location = new System.Drawing.Point(177, 264);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(171, 24);
            this.cmbFilter.TabIndex = 8;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(370, 266);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(179, 22);
            this.txtFilter.TabIndex = 9;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(77, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Filter By";
            // 
            // AddNewPersonItem
            // 
            this.AddNewPersonItem.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.AddNewPersonItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewPersonToolStripMenuItem});
            this.AddNewPersonItem.Name = "AddNewPersonItem";
            this.AddNewPersonItem.Size = new System.Drawing.Size(188, 28);
            // 
            // addNewPersonToolStripMenuItem
            // 
            this.addNewPersonToolStripMenuItem.Name = "addNewPersonToolStripMenuItem";
            this.addNewPersonToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.addNewPersonToolStripMenuItem.Text = "Add New Person";
            // 
            // frmManagePeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1499, 702);
            this.ContextMenuStrip = this.AddNewPersonItem;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCountRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFrmAdd);
            this.Controls.Add(this.dvgAllPeople);
            this.Name = "frmManagePeople";
            this.Text = "frmManagePeople";
            this.Load += new System.EventHandler(this.frmManagePeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dvgAllPeople)).EndInit();
            this.UpdateToolsStripItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.AddNewPersonItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dvgAllPeople;
        private System.Windows.Forms.Button btnFrmAdd;
        private System.Windows.Forms.ContextMenuStrip UpdateToolsStripItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCountRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip AddNewPersonItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem phoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shToolStripMenuItem;
    }
}