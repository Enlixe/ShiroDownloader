namespace ShiroDownloader
{
    partial class Checking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Checking));
            this.title = new System.Windows.Forms.Label();
            this.checkTitle = new System.Windows.Forms.Label();
            this.checkJavaText = new System.Windows.Forms.Label();
            this.checkJava = new System.Windows.Forms.Label();
            this.CheckJavaBtn = new System.Windows.Forms.Button();
            this.checkMcText = new System.Windows.Forms.Label();
            this.checkMc = new System.Windows.Forms.Label();
            this.CheckMcBtn = new System.Windows.Forms.Button();
            this.checkForgeText = new System.Windows.Forms.Label();
            this.checkForge = new System.Windows.Forms.Label();
            this.CheckForgeBtn = new System.Windows.Forms.Button();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.ContinueBtn = new System.Windows.Forms.Button();
            this.CheckForgeBtn2 = new System.Windows.Forms.Button();
            this.checkForge2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Sans Serif Collection", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.Indigo;
            this.title.Location = new System.Drawing.Point(12, 12);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(276, 47);
            this.title.TabIndex = 1;
            this.title.Text = "Shiro Downloader";
            // 
            // checkTitle
            // 
            this.checkTitle.AutoSize = true;
            this.checkTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkTitle.ForeColor = System.Drawing.Color.Indigo;
            this.checkTitle.Location = new System.Drawing.Point(15, 85);
            this.checkTitle.Name = "checkTitle";
            this.checkTitle.Size = new System.Drawing.Size(103, 29);
            this.checkTitle.TabIndex = 6;
            this.checkTitle.Text = "Checker";
            // 
            // checkJavaText
            // 
            this.checkJavaText.AutoSize = true;
            this.checkJavaText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkJavaText.ForeColor = System.Drawing.Color.BlueViolet;
            this.checkJavaText.Location = new System.Drawing.Point(21, 124);
            this.checkJavaText.Name = "checkJavaText";
            this.checkJavaText.Size = new System.Drawing.Size(58, 24);
            this.checkJavaText.TabIndex = 7;
            this.checkJavaText.Text = "Java :";
            // 
            // checkJava
            // 
            this.checkJava.AutoSize = true;
            this.checkJava.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkJava.ForeColor = System.Drawing.Color.MediumPurple;
            this.checkJava.Location = new System.Drawing.Point(74, 124);
            this.checkJava.Name = "checkJava";
            this.checkJava.Size = new System.Drawing.Size(105, 24);
            this.checkJava.TabIndex = 8;
            this.checkJava.Text = "Checking...";
            // 
            // CheckJavaBtn
            // 
            this.CheckJavaBtn.BackColor = System.Drawing.Color.Transparent;
            this.CheckJavaBtn.ForeColor = System.Drawing.Color.DimGray;
            this.CheckJavaBtn.Location = new System.Drawing.Point(192, 127);
            this.CheckJavaBtn.Name = "CheckJavaBtn";
            this.CheckJavaBtn.Size = new System.Drawing.Size(75, 23);
            this.CheckJavaBtn.TabIndex = 9;
            this.CheckJavaBtn.Text = "Download";
            this.CheckJavaBtn.UseVisualStyleBackColor = false;
            this.CheckJavaBtn.Visible = false;
            this.CheckJavaBtn.Click += new System.EventHandler(this.CheckJavaBtn_Click);
            // 
            // checkMcText
            // 
            this.checkMcText.AutoSize = true;
            this.checkMcText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMcText.ForeColor = System.Drawing.Color.BlueViolet;
            this.checkMcText.Location = new System.Drawing.Point(21, 153);
            this.checkMcText.Name = "checkMcText";
            this.checkMcText.Size = new System.Drawing.Size(96, 24);
            this.checkMcText.TabIndex = 10;
            this.checkMcText.Text = "Minecraft :";
            // 
            // checkMc
            // 
            this.checkMc.AutoSize = true;
            this.checkMc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMc.ForeColor = System.Drawing.Color.MediumPurple;
            this.checkMc.Location = new System.Drawing.Point(112, 153);
            this.checkMc.Name = "checkMc";
            this.checkMc.Size = new System.Drawing.Size(105, 24);
            this.checkMc.TabIndex = 11;
            this.checkMc.Text = "Checking...";
            // 
            // CheckMcBtn
            // 
            this.CheckMcBtn.BackColor = System.Drawing.Color.Transparent;
            this.CheckMcBtn.ForeColor = System.Drawing.Color.DimGray;
            this.CheckMcBtn.Location = new System.Drawing.Point(230, 156);
            this.CheckMcBtn.Name = "CheckMcBtn";
            this.CheckMcBtn.Size = new System.Drawing.Size(75, 23);
            this.CheckMcBtn.TabIndex = 12;
            this.CheckMcBtn.Text = "Download";
            this.CheckMcBtn.UseVisualStyleBackColor = false;
            this.CheckMcBtn.Visible = false;
            this.CheckMcBtn.Click += new System.EventHandler(this.CheckMcBtn_Click);
            // 
            // checkForgeText
            // 
            this.checkForgeText.AutoSize = true;
            this.checkForgeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkForgeText.ForeColor = System.Drawing.Color.BlueViolet;
            this.checkForgeText.Location = new System.Drawing.Point(21, 182);
            this.checkForgeText.Name = "checkForgeText";
            this.checkForgeText.Size = new System.Drawing.Size(71, 24);
            this.checkForgeText.TabIndex = 13;
            this.checkForgeText.Text = "Forge :";
            // 
            // checkForge
            // 
            this.checkForge.AutoSize = true;
            this.checkForge.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkForge.ForeColor = System.Drawing.Color.MediumPurple;
            this.checkForge.Location = new System.Drawing.Point(87, 182);
            this.checkForge.Name = "checkForge";
            this.checkForge.Size = new System.Drawing.Size(105, 24);
            this.checkForge.TabIndex = 14;
            this.checkForge.Text = "Checking...";
            // 
            // CheckForgeBtn
            // 
            this.CheckForgeBtn.BackColor = System.Drawing.Color.Transparent;
            this.CheckForgeBtn.ForeColor = System.Drawing.Color.DimGray;
            this.CheckForgeBtn.Location = new System.Drawing.Point(205, 183);
            this.CheckForgeBtn.Name = "CheckForgeBtn";
            this.CheckForgeBtn.Size = new System.Drawing.Size(100, 23);
            this.CheckForgeBtn.TabIndex = 15;
            this.CheckForgeBtn.Text = "Download 1.12";
            this.CheckForgeBtn.UseVisualStyleBackColor = false;
            this.CheckForgeBtn.Visible = false;
            this.CheckForgeBtn.Click += new System.EventHandler(this.CheckForgeBtn_Click);
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.BackColor = System.Drawing.Color.Transparent;
            this.RefreshBtn.ForeColor = System.Drawing.Color.DimGray;
            this.RefreshBtn.Location = new System.Drawing.Point(124, 85);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(75, 29);
            this.RefreshBtn.TabIndex = 16;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = false;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueBtn.Location = new System.Drawing.Point(12, 489);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(760, 60);
            this.ContinueBtn.TabIndex = 17;
            this.ContinueBtn.Text = "Continue";
            this.ContinueBtn.UseVisualStyleBackColor = true;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // CheckForgeBtn2
            // 
            this.CheckForgeBtn2.BackColor = System.Drawing.Color.Transparent;
            this.CheckForgeBtn2.ForeColor = System.Drawing.Color.DimGray;
            this.CheckForgeBtn2.Location = new System.Drawing.Point(205, 212);
            this.CheckForgeBtn2.Name = "CheckForgeBtn2";
            this.CheckForgeBtn2.Size = new System.Drawing.Size(100, 23);
            this.CheckForgeBtn2.TabIndex = 18;
            this.CheckForgeBtn2.Text = "Download 1.16";
            this.CheckForgeBtn2.UseVisualStyleBackColor = false;
            this.CheckForgeBtn2.Visible = false;
            this.CheckForgeBtn2.Click += new System.EventHandler(this.CheckForgeBtn2_Click);
            // 
            // checkForge2
            // 
            this.checkForge2.AutoSize = true;
            this.checkForge2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkForge2.ForeColor = System.Drawing.Color.MediumPurple;
            this.checkForge2.Location = new System.Drawing.Point(87, 209);
            this.checkForge2.Name = "checkForge2";
            this.checkForge2.Size = new System.Drawing.Size(105, 24);
            this.checkForge2.TabIndex = 19;
            this.checkForge2.Text = "Checking...";
            // 
            // Checking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.checkForge2);
            this.Controls.Add(this.CheckForgeBtn2);
            this.Controls.Add(this.ContinueBtn);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.CheckForgeBtn);
            this.Controls.Add(this.checkForge);
            this.Controls.Add(this.checkForgeText);
            this.Controls.Add(this.CheckMcBtn);
            this.Controls.Add(this.checkMc);
            this.Controls.Add(this.checkMcText);
            this.Controls.Add(this.CheckJavaBtn);
            this.Controls.Add(this.checkJava);
            this.Controls.Add(this.checkJavaText);
            this.Controls.Add(this.checkTitle);
            this.Controls.Add(this.title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Checking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shiro Downloader - Checker";
            this.Load += new System.EventHandler(this.Checking_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label checkTitle;
        private System.Windows.Forms.Label checkJavaText;
        private System.Windows.Forms.Label checkJava;
        private System.Windows.Forms.Button CheckJavaBtn;
        private System.Windows.Forms.Label checkMcText;
        private System.Windows.Forms.Label checkMc;
        private System.Windows.Forms.Button CheckMcBtn;
        private System.Windows.Forms.Label checkForgeText;
        private System.Windows.Forms.Label checkForge;
        private System.Windows.Forms.Button CheckForgeBtn;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.Button ContinueBtn;
        private System.Windows.Forms.Button CheckForgeBtn2;
        private System.Windows.Forms.Label checkForge2;
    }
}