namespace KFlearning.Views
{
    partial class StartupForm
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
            this.cmdNewProject = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdOpenProject = new System.Windows.Forms.Button();
            this.cmdArticles = new System.Windows.Forms.Button();
            this.cmdLaragonLink = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdAbout = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdNewProject
            // 
            this.cmdNewProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.cmdNewProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdNewProject.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmdNewProject.Location = new System.Drawing.Point(41, 143);
            this.cmdNewProject.Name = "cmdNewProject";
            this.cmdNewProject.Size = new System.Drawing.Size(103, 95);
            this.cmdNewProject.TabIndex = 0;
            this.cmdNewProject.Text = "Proyek Baru";
            this.cmdNewProject.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdNewProject.UseVisualStyleBackColor = false;
            this.cmdNewProject.Click += new System.EventHandler(this.cmdNewProject_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(117, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "KFlearning";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "E-learning terpadu by LABKOM.";
            // 
            // cmdOpenProject
            // 
            this.cmdOpenProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOpenProject.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmdOpenProject.Location = new System.Drawing.Point(150, 143);
            this.cmdOpenProject.Name = "cmdOpenProject";
            this.cmdOpenProject.Size = new System.Drawing.Size(162, 43);
            this.cmdOpenProject.TabIndex = 3;
            this.cmdOpenProject.Text = "Buka Proyek";
            this.cmdOpenProject.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdOpenProject.UseVisualStyleBackColor = true;
            this.cmdOpenProject.Click += new System.EventHandler(this.cmdOpenProject_Click);
            // 
            // cmdArticles
            // 
            this.cmdArticles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdArticles.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmdArticles.Location = new System.Drawing.Point(150, 192);
            this.cmdArticles.Name = "cmdArticles";
            this.cmdArticles.Size = new System.Drawing.Size(105, 46);
            this.cmdArticles.TabIndex = 4;
            this.cmdArticles.Text = "Lihat Artikel";
            this.cmdArticles.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdArticles.UseVisualStyleBackColor = true;
            this.cmdArticles.Click += new System.EventHandler(this.cmdArticles_Click);
            // 
            // cmdLaragonLink
            // 
            this.cmdLaragonLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(172)))), ((int)(((byte)(223)))));
            this.cmdLaragonLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdLaragonLink.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmdLaragonLink.Location = new System.Drawing.Point(261, 192);
            this.cmdLaragonLink.Name = "cmdLaragonLink";
            this.cmdLaragonLink.Size = new System.Drawing.Size(119, 46);
            this.cmdLaragonLink.TabIndex = 5;
            this.cmdLaragonLink.Text = "Laragon Link!";
            this.cmdLaragonLink.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdLaragonLink.UseVisualStyleBackColor = false;
            this.cmdLaragonLink.Click += new System.EventHandler(this.cmdLaragonLink_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.label3.Location = new System.Drawing.Point(39, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 28);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tasks.";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 87);
            this.panel1.TabIndex = 12;
            // 
            // cmdAbout
            // 
            this.cmdAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(23)))), ((int)(((byte)(7)))));
            this.cmdAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAbout.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmdAbout.Location = new System.Drawing.Point(318, 143);
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new System.Drawing.Size(62, 43);
            this.cmdAbout.TabIndex = 14;
            this.cmdAbout.Text = "About";
            this.cmdAbout.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cmdAbout.UseVisualStyleBackColor = false;
            this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(424, 276);
            this.Controls.Add(this.cmdAbout);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdLaragonLink);
            this.Controls.Add(this.cmdArticles);
            this.Controls.Add(this.cmdOpenProject);
            this.Controls.Add(this.cmdNewProject);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KFlearning Launcher";
            this.Load += new System.EventHandler(this.StartupForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdNewProject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdOpenProject;
        private System.Windows.Forms.Button cmdArticles;
        private System.Windows.Forms.Button cmdLaragonLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdAbout;
        private System.Windows.Forms.FolderBrowserDialog fbd;
    }
}

