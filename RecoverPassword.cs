﻿using System;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;
using System.Drawing;
using System.Linq;


namespace CipherShield
{
    public partial class RecoverPassword : Form
    {
        private LoginForm loginForm;
        public RecoverPassword(LoginForm loginForm)
        {
            InitializeComponent();
            this.menuBarLbl.MakeDraggable(this); // Make the form draggable
            this.loginForm = loginForm;
        }

        // submit button
        private void SubmitRecoverPwdBtn_Click(object sender, EventArgs e)
        {
            if (SecurityQuestion1txtBox.Text.Length == 0 || SecurityQuestion2txtBox.Text.Length == 0 || SecurityQuestion3txtBox.Text.Length == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("All security questions should be filled.")
                    .Show();
                return;
            }
            string[] txtBoxes = { SecurityQuestion1txtBox.Text, SecurityQuestion2txtBox.Text, SecurityQuestion3txtBox.Text };
            var answers = SecureStorage.GetSecurityAnswers();
            bool theSame = txtBoxes.SequenceEqual(answers);
            if (theSame == true)
                try
                {
                    {
                        loginForm.LoginMasterPwdTxtBox.Text = SecureStorage.GetMasterPassword();
                        string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                        Uri successUri = new Uri($"file:///{successIcon}");
                        new ToastContentBuilder()
                            .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                            .AddText("Successful password recover.")
                            .AddText("Your password is loaded in the login password tab.")
                            .Show();
                        Close();
                    }
                }

                catch (Exception ex)
                {
                    string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                    Uri errorUri = new Uri($"file:///{errorIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                        .AddText("An error occurred.")
                        .AddText(ex.Message)
                        .Show();
                    return;
                }
            if (theSame == false)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Incorrect security answers.")
                    .AddText("Please try again.")
                    .Show();
                return;
            }
        }

        private void CancelRecoverPwdBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set DialogResult to Cancel
            Close();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        // method to show inputs
        private void hideShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "EyeBlack.png"));
            SecurityQuestion1txtBox.PasswordChar = '\0';
            SecurityQuestion2txtBox.PasswordChar = '\0';
            SecurityQuestion3txtBox.PasswordChar = '\0';
        }

        // method to hide inputs
        private void hideShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "NotEyeBlack.png"));
            SecurityQuestion1txtBox.PasswordChar = '*';
            SecurityQuestion2txtBox.PasswordChar = '*';
            SecurityQuestion3txtBox.PasswordChar = '*';
        }
    }
}
