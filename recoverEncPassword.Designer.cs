namespace CipherShield
{
    partial class recoverEncPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(recoverEncPassword));
            menuBarLbl = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            hideShowPassword = new System.Windows.Forms.Button();
            CloseBtn = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            SecurityQuestion3txtBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            SecurityQuestion2txtBox = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            SecurityQuestion1txtBox = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            lockPasswordTxtBox = new System.Windows.Forms.TextBox();
            panel2 = new System.Windows.Forms.Panel();
            SubmitRecoverPwdBtn = new System.Windows.Forms.Button();
            CancelRecoverPwdBtn = new System.Windows.Forms.Button();
            focusBtn = new System.Windows.Forms.Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuBarLbl
            // 
            menuBarLbl.BackColor = System.Drawing.SystemColors.ControlLight;
            menuBarLbl.Dock = System.Windows.Forms.DockStyle.Top;
            menuBarLbl.Location = new System.Drawing.Point(0, 0);
            menuBarLbl.Name = "menuBarLbl";
            menuBarLbl.Size = new System.Drawing.Size(378, 29);
            menuBarLbl.TabIndex = 16;
            // 
            // label13
            // 
            label13.BackColor = System.Drawing.SystemColors.ControlLight;
            label13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label13.Image = (System.Drawing.Image)resources.GetObject("label13.Image");
            label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label13.Location = new System.Drawing.Point(0, 1);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(120, 28);
            label13.TabIndex = 18;
            label13.Text = "Cipher Shield";
            label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hideShowPassword
            // 
            hideShowPassword.BackColor = System.Drawing.SystemColors.ControlLight;
            hideShowPassword.BackgroundImage = (System.Drawing.Image)resources.GetObject("hideShowPassword.BackgroundImage");
            hideShowPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            hideShowPassword.FlatAppearance.BorderSize = 0;
            hideShowPassword.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            hideShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            hideShowPassword.ForeColor = System.Drawing.Color.FromArgb(32, 33, 36);
            hideShowPassword.Location = new System.Drawing.Point(322, 3);
            hideShowPassword.Name = "hideShowPassword";
            hideShowPassword.Size = new System.Drawing.Size(25, 25);
            hideShowPassword.TabIndex = 7;
            hideShowPassword.TabStop = false;
            hideShowPassword.UseVisualStyleBackColor = false;
            hideShowPassword.MouseDown += hideShowPassword_MouseDown;
            hideShowPassword.MouseUp += hideShowPassword_MouseUp;
            // 
            // CloseBtn
            // 
            CloseBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            CloseBtn.BackgroundImage = (System.Drawing.Image)resources.GetObject("CloseBtn.BackgroundImage");
            CloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            CloseBtn.FlatAppearance.BorderSize = 0;
            CloseBtn.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar;
            CloseBtn.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLightLight;
            CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            CloseBtn.ForeColor = System.Drawing.SystemColors.ControlLight;
            CloseBtn.Location = new System.Drawing.Point(350, 5);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new System.Drawing.Size(23, 22);
            CloseBtn.TabIndex = 8;
            CloseBtn.TabStop = false;
            CloseBtn.UseVisualStyleBackColor = false;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(0, 32);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(378, 46);
            panel1.TabIndex = 35;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(105, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(179, 23);
            label1.TabIndex = 31;
            label1.Text = "Recover your password";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(10, 265);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(130, 18);
            label5.TabIndex = 41;
            label5.Text = "Security Question 3";
            // 
            // SecurityQuestion3txtBox
            // 
            SecurityQuestion3txtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            SecurityQuestion3txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            SecurityQuestion3txtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            SecurityQuestion3txtBox.Location = new System.Drawing.Point(166, 263);
            SecurityQuestion3txtBox.Name = "SecurityQuestion3txtBox";
            SecurityQuestion3txtBox.PasswordChar = '*';
            SecurityQuestion3txtBox.PlaceholderText = "Your favorite sport?";
            SecurityQuestion3txtBox.Size = new System.Drawing.Size(197, 26);
            SecurityQuestion3txtBox.TabIndex = 3;
            SecurityQuestion3txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 193);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(130, 18);
            label3.TabIndex = 40;
            label3.Text = "Security Question 2";
            // 
            // SecurityQuestion2txtBox
            // 
            SecurityQuestion2txtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            SecurityQuestion2txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            SecurityQuestion2txtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            SecurityQuestion2txtBox.Location = new System.Drawing.Point(166, 191);
            SecurityQuestion2txtBox.Name = "SecurityQuestion2txtBox";
            SecurityQuestion2txtBox.PasswordChar = '*';
            SecurityQuestion2txtBox.PlaceholderText = "Your favorite cartoon?";
            SecurityQuestion2txtBox.Size = new System.Drawing.Size(197, 26);
            SecurityQuestion2txtBox.TabIndex = 2;
            SecurityQuestion2txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 125);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(128, 18);
            label4.TabIndex = 39;
            label4.Text = "Security Question 1";
            // 
            // SecurityQuestion1txtBox
            // 
            SecurityQuestion1txtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            SecurityQuestion1txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            SecurityQuestion1txtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            SecurityQuestion1txtBox.Location = new System.Drawing.Point(166, 123);
            SecurityQuestion1txtBox.Name = "SecurityQuestion1txtBox";
            SecurityQuestion1txtBox.PasswordChar = '*';
            SecurityQuestion1txtBox.PlaceholderText = "Your favorite city?";
            SecurityQuestion1txtBox.Size = new System.Drawing.Size(197, 26);
            SecurityQuestion1txtBox.TabIndex = 1;
            SecurityQuestion1txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 334);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(134, 18);
            label2.TabIndex = 43;
            label2.Text = "Enter Lock Password";
            // 
            // lockPasswordTxtBox
            // 
            lockPasswordTxtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            lockPasswordTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lockPasswordTxtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            lockPasswordTxtBox.Location = new System.Drawing.Point(166, 332);
            lockPasswordTxtBox.Name = "lockPasswordTxtBox";
            lockPasswordTxtBox.PasswordChar = '*';
            lockPasswordTxtBox.Size = new System.Drawing.Size(197, 26);
            lockPasswordTxtBox.TabIndex = 4;
            lockPasswordTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            panel2.Controls.Add(SubmitRecoverPwdBtn);
            panel2.Controls.Add(CancelRecoverPwdBtn);
            panel2.Location = new System.Drawing.Point(0, 402);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(378, 62);
            panel2.TabIndex = 44;
            // 
            // SubmitRecoverPwdBtn
            // 
            SubmitRecoverPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            SubmitRecoverPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            SubmitRecoverPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            SubmitRecoverPwdBtn.Location = new System.Drawing.Point(28, 17);
            SubmitRecoverPwdBtn.Name = "SubmitRecoverPwdBtn";
            SubmitRecoverPwdBtn.Size = new System.Drawing.Size(133, 33);
            SubmitRecoverPwdBtn.TabIndex = 5;
            SubmitRecoverPwdBtn.Text = "Submit";
            SubmitRecoverPwdBtn.UseVisualStyleBackColor = false;
            SubmitRecoverPwdBtn.Click += SubmitRecoverPwdBtn_Click;
            // 
            // CancelRecoverPwdBtn
            // 
            CancelRecoverPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            CancelRecoverPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            CancelRecoverPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            CancelRecoverPwdBtn.Location = new System.Drawing.Point(214, 17);
            CancelRecoverPwdBtn.Name = "CancelRecoverPwdBtn";
            CancelRecoverPwdBtn.Size = new System.Drawing.Size(133, 33);
            CancelRecoverPwdBtn.TabIndex = 6;
            CancelRecoverPwdBtn.Text = "Cancel";
            CancelRecoverPwdBtn.UseVisualStyleBackColor = false;
            CancelRecoverPwdBtn.Click += CancelRecoverPwdBtn_Click;
            // 
            // focusBtn
            // 
            focusBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            focusBtn.CausesValidation = false;
            focusBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(32, 33, 36);
            focusBtn.FlatAppearance.BorderSize = 0;
            focusBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            focusBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            focusBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            focusBtn.ForeColor = System.Drawing.Color.FromArgb(32, 33, 36);
            focusBtn.Location = new System.Drawing.Point(8, 84);
            focusBtn.Name = "focusBtn";
            focusBtn.Size = new System.Drawing.Size(1, 1);
            focusBtn.TabIndex = 58;
            focusBtn.TabStop = false;
            focusBtn.UseVisualStyleBackColor = true;
            // 
            // recoverEncPassword
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            ClientSize = new System.Drawing.Size(378, 465);
            ControlBox = false;
            Controls.Add(focusBtn);
            Controls.Add(panel2);
            Controls.Add(label2);
            Controls.Add(lockPasswordTxtBox);
            Controls.Add(label5);
            Controls.Add(SecurityQuestion3txtBox);
            Controls.Add(label3);
            Controls.Add(SecurityQuestion2txtBox);
            Controls.Add(label4);
            Controls.Add(SecurityQuestion1txtBox);
            Controls.Add(panel1);
            Controls.Add(CloseBtn);
            Controls.Add(hideShowPassword);
            Controls.Add(label13);
            Controls.Add(menuBarLbl);
            Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "recoverEncPassword";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "recoverEncPassword";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label menuBarLbl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button hideShowPassword;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox SecurityQuestion3txtBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox SecurityQuestion2txtBox;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox SecurityQuestion1txtBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox lockPasswordTxtBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button SubmitRecoverPwdBtn;
        private System.Windows.Forms.Button CancelRecoverPwdBtn;
        private System.Windows.Forms.Button focusBtn;
    }
}