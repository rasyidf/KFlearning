namespace KFlearning.Views
{
    partial class CreateProjectForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.chkLink = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.cboTemplate = new System.Windows.Forms.ComboBox();
            this.txtLinkName = new System.Windows.Forms.TextBox();
            this.cmdCreate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdBrowse = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nama proyek";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Template";
            // 
            // chkLink
            // 
            this.chkLink.AutoSize = true;
            this.chkLink.Location = new System.Drawing.Point(119, 122);
            this.chkLink.Name = "chkLink";
            this.chkLink.Size = new System.Drawing.Size(137, 19);
            this.chkLink.TabIndex = 2;
            this.chkLink.Text = "Link dengan Laragon";
            this.chkLink.UseVisualStyleBackColor = true;
            this.chkLink.CheckedChanged += new System.EventHandler(this.chkLink_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nama link";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(119, 25);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(189, 23);
            this.txtProjectName.TabIndex = 4;
            this.txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
            // 
            // cboTemplate
            // 
            this.cboTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemplate.FormattingEnabled = true;
            this.cboTemplate.Location = new System.Drawing.Point(119, 54);
            this.cboTemplate.Name = "cboTemplate";
            this.cboTemplate.Size = new System.Drawing.Size(137, 23);
            this.cboTemplate.TabIndex = 5;
            // 
            // txtLinkName
            // 
            this.txtLinkName.Location = new System.Drawing.Point(119, 147);
            this.txtLinkName.Name = "txtLinkName";
            this.txtLinkName.ReadOnly = true;
            this.txtLinkName.Size = new System.Drawing.Size(189, 23);
            this.txtLinkName.TabIndex = 6;
            // 
            // cmdCreate
            // 
            this.cmdCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCreate.Location = new System.Drawing.Point(119, 192);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(111, 32);
            this.cmdCreate.TabIndex = 7;
            this.cmdCreate.Text = "Buat Proyek";
            this.cmdCreate.UseVisualStyleBackColor = true;
            this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Lokasi";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(119, 83);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(167, 23);
            this.txtLocation.TabIndex = 9;
            // 
            // fbd
            // 
            this.fbd.Description = "Pilih lokasi proyek.";
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.AutoSize = true;
            this.cmdBrowse.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(160)))), ((int)(((byte)(230)))));
            this.cmdBrowse.Location = new System.Drawing.Point(292, 86);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(16, 15);
            this.cmdBrowse.TabIndex = 10;
            this.cmdBrowse.TabStop = true;
            this.cmdBrowse.Text = "...";
            this.cmdBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdBrowse_LinkClicked);
            // 
            // CreateProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(345, 243);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmdCreate);
            this.Controls.Add(this.txtLinkName);
            this.Controls.Add(this.cboTemplate);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buat Proyek Baru";
            this.Load += new System.EventHandler(this.CreateProjectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.ComboBox cboTemplate;
        private System.Windows.Forms.TextBox txtLinkName;
        private System.Windows.Forms.Button cmdCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.LinkLabel cmdBrowse;
    }
}