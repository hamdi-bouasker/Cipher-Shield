namespace CipherShield
{
    partial class RecoverPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecoverPassword));
            menuBarLbl = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            CloseBtn = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            SecurityQuestion3txtBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            SecurityQuestion2txtBox = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            SecurityQuestion1txtBox = new System.Windows.Forms.TextBox();
            CancelRecoverPwdBtn = new System.Windows.Forms.Button();
            SubmitRecoverPwdBtn = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            hideShowPassword = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
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
            menuBarLbl.Size = new System.Drawing.Size(431, 27);
            menuBarLbl.TabIndex = 15;
            // 
            // label13
            // 
            label13.BackColor = System.Drawing.SystemColors.ControlLight;
            label13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label13.Image = (System.Drawing.Image)resources.GetObject("label13.Image");
            label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label13.Location = new System.Drawing.Point(0, 1);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(116, 26);
            label13.TabIndex = 17;
            label13.Text = "Cipher Shield";
            label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            CloseBtn.Location = new System.Drawing.Point(408, 3);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new System.Drawing.Size(20, 21);
            CloseBtn.TabIndex = 18;
            CloseBtn.UseVisualStyleBackColor = false;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(29, 268);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(134, 18);
            label5.TabIndex = 30;
            label5.Text = "Security Question 3:";
            // 
            // SecurityQuestion3txtBox
            // 
            SecurityQuestion3txtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            SecurityQuestion3txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            SecurityQuestion3txtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            SecurityQuestion3txtBox.Location = new System.Drawing.Point(204, 266);
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
            label3.Location = new System.Drawing.Point(32, 196);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(134, 18);
            label3.TabIndex = 28;
            label3.Text = "Security Question 2:";
            // 
            // SecurityQuestion2txtBox
            // 
            SecurityQuestion2txtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            SecurityQuestion2txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            SecurityQuestion2txtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            SecurityQuestion2txtBox.Location = new System.Drawing.Point(204, 194);
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
            label4.Location = new System.Drawing.Point(32, 128);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(132, 18);
            label4.TabIndex = 27;
            label4.Text = "Security Question 1:";
            // 
            // SecurityQuestion1txtBox
            // 
            SecurityQuestion1txtBox.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            SecurityQuestion1txtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            SecurityQuestion1txtBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            SecurityQuestion1txtBox.Location = new System.Drawing.Point(202, 126);
            SecurityQuestion1txtBox.Name = "SecurityQuestion1txtBox";
            SecurityQuestion1txtBox.PasswordChar = '*';
            SecurityQuestion1txtBox.PlaceholderText = "Your favorite city?";
            SecurityQuestion1txtBox.Size = new System.Drawing.Size(197, 26);
            SecurityQuestion1txtBox.TabIndex = 1;
            SecurityQuestion1txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CancelRecoverPwdBtn
            // 
            CancelRecoverPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            CancelRecoverPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            CancelRecoverPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            CancelRecoverPwdBtn.Location = new System.Drawing.Point(241, 22);
            CancelRecoverPwdBtn.Name = "CancelRecoverPwdBtn";
            CancelRecoverPwdBtn.Size = new System.Drawing.Size(158, 33);
            CancelRecoverPwdBtn.TabIndex = 5;
            CancelRecoverPwdBtn.Text = "Cancel";
            CancelRecoverPwdBtn.UseVisualStyleBackColor = false;
            CancelRecoverPwdBtn.Click += CancelRecoverPwdBtn_Click;
            // 
            // SubmitRecoverPwdBtn
            // 
            SubmitRecoverPwdBtn.BackColor = System.Drawing.Color.PowderBlue;
            SubmitRecoverPwdBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            SubmitRecoverPwdBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            SubmitRecoverPwdBtn.Location = new System.Drawing.Point(29, 22);
            SubmitRecoverPwdBtn.Name = "SubmitRecoverPwdBtn";
            SubmitRecoverPwdBtn.Size = new System.Drawing.Size(158, 33);
            SubmitRecoverPwdBtn.TabIndex = 4;
            SubmitRecoverPwdBtn.Text = "Submit";
            SubmitRecoverPwdBtn.UseVisualStyleBackColor = false;
            SubmitRecoverPwdBtn.Click += SubmitRecoverPwdBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(128, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(179, 23);
            label1.TabIndex = 31;
            label1.Text = "Recover your password";
            // 
            // hideShowPassword
            // 
            hideShowPassword.BackColor = System.Drawing.SystemColors.ControlLight;
            hideShowPassword.BackgroundImage = (System.Drawing.Image)resources.GetObject("hideShowPassword.BackgroundImage");
            hideShowPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            hideShowPassword.FlatAppearance.BorderSize = 0;
            hideShowPassword.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            hideShowPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            hideShowPassword.ForeColor = System.Drawing.Color.FromArgb(32, 33, 36);
            hideShowPassword.Location = new System.Drawing.Point(380, 2);
            hideShowPassword.Name = "hideShowPassword";
            hideShowPassword.Size = new System.Drawing.Size(22, 24);
            hideShowPassword.TabIndex = 32;
            hideShowPassword.UseVisualStyleBackColor = false;
            hideShowPassword.MouseDown += hideShowPassword_MouseDown;
            hideShowPassword.MouseUp += hideShowPassword_MouseUp;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(0, 30);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(431, 46);
            panel1.TabIndex = 33;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.FromArgb(41, 42, 45);
            panel2.Controls.Add(SubmitRecoverPwdBtn);
            panel2.Controls.Add(CancelRecoverPwdBtn);
            panel2.Location = new System.Drawing.Point(0, 345);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(428, 65);
            panel2.TabIndex = 34;
            // 
            // RecoverPassword
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.FromArgb(32, 33, 36);
            ClientSize = new System.Drawing.Size(431, 412);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(hideShowPassword);
            Controls.Add(label5);
            Controls.Add(SecurityQuestion3txtBox);
            Controls.Add(label3);
            Controls.Add(SecurityQuestion2txtBox);
            Controls.Add(label4);
            Controls.Add(SecurityQuestion1txtBox);
            Controls.Add(CloseBtn);
            Controls.Add(label13);
            Controls.Add(menuBarLbl);
            Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RecoverPassword";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label menuBarLbl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox SecurityQuestion3txtBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox SecurityQuestion2txtBox;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox SecurityQuestion1txtBox;
        private System.Windows.Forms.Button CancelRecoverPwdBtn;
        private System.Windows.Forms.Button SubmitRecoverPwdBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button hideShowPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}