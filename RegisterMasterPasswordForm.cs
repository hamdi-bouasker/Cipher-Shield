using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Drawing;


namespace CipherShield
{
    public partial class RegisterMasterPasswordForm : Form
    {
        public string Password { get; private set; }
        public RegisterMasterPasswordForm()
        {
            InitializeComponent();
            this.menuBarLbl.MakeDraggable(this); // Make the form draggable
        }

        // Submit the master password for registration
        private void SubmitMasterPwdBtn_Click(object sender, EventArgs e)
        {
            if (RegisterMasterPwdTxtBox.Text.Length == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Please enter a password.")
                    .Show();
                return;
            }
            if (RegisterMasterPwdTxtBox.Text != RegisterMasterPwdConfirmTxtBox.Text)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Passwords do not match.")
                    .Show();
                return;
            }
            if (RegisterMasterPwdTxtBox.Text.Length < 8)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Password must be at least 8 characters long.")
                    .Show();
                return;
            }

            if (SecurityQuestion1txtBox.Text.Length == 0 || SecurityQuestion2txtBox.Text.Length == 0 || SecurityQuestion3txtBox.Text.Length == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("All security questions should be filled." + Environment.NewLine + "You will need them in case you forgot your password.")
                    .Show();
                return;
            }
            if (RegisterMasterPwdTxtBox.Text == RegisterMasterPwdConfirmTxtBox.Text && SecurityQuestion1txtBox.Text.Length != 0 && SecurityQuestion2txtBox.Text.Length != 0 && SecurityQuestion3txtBox.Text.Length != 0)
            {
                Password = RegisterMasterPwdTxtBox.Text;
                SecureStorage.SaveMasterPassword(Password);
                SecureStorage.SaveSecurityQuestions(new string[] { SecurityQuestion1txtBox.Text, SecurityQuestion2txtBox.Text, SecurityQuestion3txtBox.Text });
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("The password and security questions have been successfully registered.")
                    .Show();
                this.DialogResult = DialogResult.OK; // Set DialogResult to OK
                Close();
            }

        }

        // Cancel the registration process
        private void CancelRegisterMasterPwdBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set DialogResult to Cancel
            Close();
        }

        // close the form
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Minimize the form
        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            focusBtn.Focus();
            ActiveForm.WindowState = FormWindowState.Minimized;
            
        }

        // method to show inputs

        private void hideShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "EyeBlack.png"));
            RegisterMasterPwdTxtBox.PasswordChar = '\0';
            RegisterMasterPwdConfirmTxtBox.PasswordChar = '\0';
            SecurityQuestion1txtBox.PasswordChar = '\0';
            SecurityQuestion2txtBox.PasswordChar = '\0';
            SecurityQuestion3txtBox.PasswordChar = '\0';
        }

        // method to hide inputs
        private void hideShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "NotEyeBlack.png"));
            RegisterMasterPwdTxtBox.PasswordChar = '*';
            RegisterMasterPwdConfirmTxtBox.PasswordChar = '*';
            SecurityQuestion1txtBox.PasswordChar = '*';
            SecurityQuestion2txtBox.PasswordChar = '*';
            SecurityQuestion3txtBox.PasswordChar = '*';
        }
    }
}
