namespace CipherShield
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            label1 = new System.Windows.Forms.Label();
            CancelLoginMasterPwdBtn = new System.Windows.Forms.Button();
            SubmitLoginPwdBtn = new System.Windows.Forms.Button();
            LoginMasterPwdTxtBox = new System.Windows.Forms.TextBox();
            LoginPwdLoadBackupBtn = new System.Windows.Forms.Button();
            menuBarLbl = new System.Windows.Forms.Label();
            CloseBtn = new System.Windows.Forms.Button();
            label13 = new System.Windows.Forms.Label();
            MinimizeBtn = new System.Windows.Forms.Button();
            LoadLockPasswordBtn = new System.Windows.Forms.Button();
            hideShowPassword = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(71, 59);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(106, 18);
            label1.TabIndex = 15;
            label1.Text = "Enter Password:";
            // 
            // CancelLoginMasterPwdBtn
            // 
            CancelLoginMasterPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            CancelLoginMasterPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            CancelLoginMasterPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            CancelLoginMasterPwdBtn.Location = new System.Drawing.Point(45, 382);
            CancelLoginMasterPwdBtn.Name = "CancelLoginMasterPwdBtn";
            CancelLoginMasterPwdBtn.Size = new System.Drawing.Size(158, 33);
            CancelLoginMasterPwdBtn.TabIndex = 4;
            CancelLoginMasterPwdBtn.Text = "Cancel";
            CancelLoginMasterPwdBtn.UseVisualStyleBackColor = false;
            CancelLoginMasterPwdBtn.Click += CancelLoginMasterPwdBtn_Click;
            // 
            // SubmitLoginPwdBtn
            // 
            SubmitLoginPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            SubmitLoginPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            SubmitLoginPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            SubmitLoginPwdBtn.Location = new System.Drawing.Point(45, 169);
            SubmitLoginPwdBtn.Name = "SubmitLoginPwdBtn";
            SubmitLoginPwdBtn.Size = new System.Drawing.Size(158, 33);
            SubmitLoginPwdBtn.TabIndex = 2;
            SubmitLoginPwdBtn.Text = "Submit";
            SubmitLoginPwdBtn.UseVisualStyleBackColor = false;
            SubmitLoginPwdBtn.Click += SubmitLoginPwdBtn_Click;
            // 
            // LoginMasterPwdTxtBox
            // 
            LoginMasterPwdTxtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            LoginMasterPwdTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            LoginMasterPwdTxtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            LoginMasterPwdTxtBox.Location = new System.Drawing.Point(45, 110);
            LoginMasterPwdTxtBox.Name = "LoginMasterPwdTxtBox";
            LoginMasterPwdTxtBox.PasswordChar = '*';
            LoginMasterPwdTxtBox.Size = new System.Drawing.Size(158, 26);
            LoginMasterPwdTxtBox.TabIndex = 1;
            LoginMasterPwdTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoginPwdLoadBackupBtn
            // 
            LoginPwdLoadBackupBtn.BackColor = System.Drawing.Color.PowderBlue;
            LoginPwdLoadBackupBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            LoginPwdLoadBackupBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            LoginPwdLoadBackupBtn.Location = new System.Drawing.Point(45, 310);
            LoginPwdLoadBackupBtn.Name = "LoginPwdLoadBackupBtn";
            LoginPwdLoadBackupBtn.Size = new System.Drawing.Size(158, 33);
            LoginPwdLoadBackupBtn.TabIndex = 3;
            LoginPwdLoadBackupBtn.Text = "Recover Password";
            LoginPwdLoadBackupBtn.UseVisualStyleBackColor = false;
            LoginPwdLoadBackupBtn.Click += LoginPwdLoadBackupBtn_Click;
            // 
            // menuBarLbl
            // 
            menuBarLbl.BackColor = System.Drawing.SystemColors.ControlLight;
            menuBarLbl.Dock = System.Windows.Forms.DockStyle.Top;
            menuBarLbl.Location = new System.Drawing.Point(0, 0);
            menuBarLbl.Name = "menuBarLbl";
            menuBarLbl.Size = new System.Drawing.Size(245, 27);
            menuBarLbl.TabIndex = 16;
            // 
            // CloseBtn
            // 
            CloseBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            CloseBtn.FlatAppearance.BorderSize = 0;
            CloseBtn.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar;
            CloseBtn.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLightLight;
            CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            CloseBtn.ForeColor = System.Drawing.SystemColors.ControlLight;
            CloseBtn.Image = (System.Drawing.Image)resources.GetObject("CloseBtn.Image");
            CloseBtn.Location = new System.Drawing.Point(222, 3);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new System.Drawing.Size(20, 21);
            CloseBtn.TabIndex = 17;
            CloseBtn.UseVisualStyleBackColor = false;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // label13
            // 
            label13.BackColor = System.Drawing.SystemColors.ControlLight;
            label13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label13.Image = (System.Drawing.Image)resources.GetObject("label13.Image");
            label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label13.Location = new System.Drawing.Point(0, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(116, 26);
            label13.TabIndex = 18;
            label13.Text = "Cipher Shield";
            label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MinimizeBtn
            // 
            MinimizeBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            MinimizeBtn.FlatAppearance.BorderSize = 0;
            MinimizeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar;
            MinimizeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLightLight;
            MinimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            MinimizeBtn.ForeColor = System.Drawing.SystemColors.ControlLight;
            MinimizeBtn.Image = (System.Drawing.Image)resources.GetObject("MinimizeBtn.Image");
            MinimizeBtn.Location = new System.Drawing.Point(199, 3);
            MinimizeBtn.Name = "MinimizeBtn";
            MinimizeBtn.Size = new System.Drawing.Size(20, 21);
            MinimizeBtn.TabIndex = 19;
            MinimizeBtn.UseVisualStyleBackColor = false;
            MinimizeBtn.Click += MinimizeBtn_Click;
            // 
            // LoadLockPasswordBtn
            // 
            LoadLockPasswordBtn.BackColor = System.Drawing.Color.PowderBlue;
            LoadLockPasswordBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            LoadLockPasswordBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            LoadLockPasswordBtn.Location = new System.Drawing.Point(45, 239);
            LoadLockPasswordBtn.Name = "LoadLockPasswordBtn";
            LoadLockPasswordBtn.Size = new System.Drawing.Size(158, 33);
            LoadLockPasswordBtn.TabIndex = 20;
            LoadLockPasswordBtn.Text = "Load Password";
            LoadLockPasswordBtn.UseVisualStyleBackColor = false;
            LoadLockPasswordBtn.Click += LoadLockPasswordBtn_Click;
            // 
            // hideShowPassword
            // 
            hideShowPassword.BackgroundImage = (System.Drawing.Image)resources.GetObject("hideShowPassword.BackgroundImage");
            hideShowPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            hideShowPassword.FlatAppearance.BorderSize = 0;
            hideShowPassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            hideShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            hideShowPassword.ForeColor = System.Drawing.Color.FromArgb(32, 33, 36);
            hideShowPassword.Location = new System.Drawing.Point(209, 112);
            hideShowPassword.Name = "hideShowPassword";
            hideShowPassword.Size = new System.Drawing.Size(22, 24);
            hideShowPassword.TabIndex = 21;
            hideShowPassword.UseVisualStyleBackColor = true;
            hideShowPassword.MouseDown += hideShowPassword_MouseDown;
            hideShowPassword.MouseUp += hideShowPassword_MouseUp;
            // 
            // LoginForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            ClientSize = new System.Drawing.Size(245, 458);
            Controls.Add(hideShowPassword);
            Controls.Add(LoadLockPasswordBtn);
            Controls.Add(MinimizeBtn);
            Controls.Add(label13);
            Controls.Add(CloseBtn);
            Controls.Add(menuBarLbl);
            Controls.Add(LoginPwdLoadBackupBtn);
            Controls.Add(label1);
            Controls.Add(CancelLoginMasterPwdBtn);
            Controls.Add(SubmitLoginPwdBtn);
            Controls.Add(LoginMasterPwdTxtBox);
            Font = new System.Drawing.Font("Comic Sans MS", 9.75F);
            ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Cipher Shield";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CancelLoginMasterPwdBtn;
        private System.Windows.Forms.Button SubmitLoginPwdBtn;
        private System.Windows.Forms.Button LoginPwdLoadBackupBtn;
        public System.Windows.Forms.TextBox LoginMasterPwdTxtBox;
        private System.Windows.Forms.Label menuBarLbl;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button MinimizeBtn;
        private System.Windows.Forms.Button LoadLockPasswordBtn;
        private System.Windows.Forms.Button hideShowPassword;
    }
}