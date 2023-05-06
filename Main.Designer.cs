namespace ShiroDownloader
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.title = new System.Windows.Forms.Label();
            this.textStatusMain = new System.Windows.Forms.Label();
            this.textStatus = new System.Windows.Forms.Label();
            this.btnMcDownload = new System.Windows.Forms.Button();
            this.modTitle = new System.Windows.Forms.Label();
            this.mod1Text = new System.Windows.Forms.Label();
            this.mod1Button = new System.Windows.Forms.Button();
            this.mod1Progress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Sans Serif Collection", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.Purple;
            this.title.Location = new System.Drawing.Point(12, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(260, 47);
            this.title.TabIndex = 0;
            this.title.Text = "Shiro Downloader";
            // 
            // textStatusMain
            // 
            this.textStatusMain.AutoSize = true;
            this.textStatusMain.BackColor = System.Drawing.Color.Transparent;
            this.textStatusMain.Font = new System.Drawing.Font("Sans Serif Collection", 6.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStatusMain.ForeColor = System.Drawing.Color.DarkMagenta;
            this.textStatusMain.Location = new System.Drawing.Point(26, 52);
            this.textStatusMain.Name = "textStatusMain";
            this.textStatusMain.Size = new System.Drawing.Size(181, 23);
            this.textStatusMain.TabIndex = 1;
            this.textStatusMain.Text = "Minecraft installation status :";
            // 
            // textStatus
            // 
            this.textStatus.AutoSize = true;
            this.textStatus.BackColor = System.Drawing.Color.Transparent;
            this.textStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStatus.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.textStatus.Location = new System.Drawing.Point(204, 54);
            this.textStatus.Name = "textStatus";
            this.textStatus.Size = new System.Drawing.Size(72, 16);
            this.textStatus.TabIndex = 2;
            this.textStatus.Text = "Checking...";
            // 
            // btnMcDownload
            // 
            this.btnMcDownload.BackColor = System.Drawing.Color.Transparent;
            this.btnMcDownload.Enabled = false;
            this.btnMcDownload.ForeColor = System.Drawing.Color.DimGray;
            this.btnMcDownload.Location = new System.Drawing.Point(207, 73);
            this.btnMcDownload.Name = "btnMcDownload";
            this.btnMcDownload.Size = new System.Drawing.Size(100, 23);
            this.btnMcDownload.TabIndex = 3;
            this.btnMcDownload.Text = "Download Now";
            this.btnMcDownload.UseVisualStyleBackColor = false;
            this.btnMcDownload.Visible = false;
            this.btnMcDownload.Click += new System.EventHandler(this.btnMcDownload_Click);
            // 
            // modTitle
            // 
            this.modTitle.AutoSize = true;
            this.modTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modTitle.ForeColor = System.Drawing.Color.DarkMagenta;
            this.modTitle.Location = new System.Drawing.Point(25, 112);
            this.modTitle.Name = "modTitle";
            this.modTitle.Size = new System.Drawing.Size(111, 25);
            this.modTitle.TabIndex = 5;
            this.modTitle.Text = "Modpacks";
            // 
            // mod1Text
            // 
            this.mod1Text.AutoSize = true;
            this.mod1Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mod1Text.Location = new System.Drawing.Point(33, 146);
            this.mod1Text.Name = "mod1Text";
            this.mod1Text.Size = new System.Drawing.Size(69, 20);
            this.mod1Text.TabIndex = 6;
            this.mod1Text.Text = "RL Craft";
            // 
            // mod1Button
            // 
            this.mod1Button.BackColor = System.Drawing.Color.Transparent;
            this.mod1Button.ForeColor = System.Drawing.Color.DimGray;
            this.mod1Button.Location = new System.Drawing.Point(108, 146);
            this.mod1Button.Name = "mod1Button";
            this.mod1Button.Size = new System.Drawing.Size(71, 23);
            this.mod1Button.TabIndex = 7;
            this.mod1Button.Text = "Download";
            this.mod1Button.UseVisualStyleBackColor = false;
            this.mod1Button.Click += new System.EventHandler(this.mod1Button_Click);
            // 
            // mod1Progress
            // 
            this.mod1Progress.AutoSize = true;
            this.mod1Progress.Location = new System.Drawing.Point(185, 151);
            this.mod1Progress.Name = "mod1Progress";
            this.mod1Progress.Size = new System.Drawing.Size(48, 13);
            this.mod1Progress.TabIndex = 8;
            this.mod1Progress.Text = "Progress";
            this.mod1Progress.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mod1Progress);
            this.Controls.Add(this.mod1Button);
            this.Controls.Add(this.mod1Text);
            this.Controls.Add(this.modTitle);
            this.Controls.Add(this.btnMcDownload);
            this.Controls.Add(this.textStatusMain);
            this.Controls.Add(this.title);
            this.Controls.Add(this.textStatus);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shiro Downloader";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label textStatusMain;
        private System.Windows.Forms.Label textStatus;
        private System.Windows.Forms.Button btnMcDownload;
        private System.Windows.Forms.Label modTitle;
        private System.Windows.Forms.Label mod1Text;
        private System.Windows.Forms.Button mod1Button;
        private System.Windows.Forms.Label mod1Progress;
    }
}

