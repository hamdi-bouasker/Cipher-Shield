using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Drawing;

namespace CipherShield
{
    public partial class LoginForm : Form
    {
        public string Password { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            this.menuBarLbl.MakeDraggable(this); // Make the form draggable           
        }

        public TextBox LoginPassTxtBox
        {
            get { return LoginMasterPwdTxtBox; }
        }

        // Submit the master password for login
        private void SubmitLoginPwdBtn_Click(object sender, EventArgs e)
        {
            Password = LoginMasterPwdTxtBox.Text;
            string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
            Uri successUri = new Uri($"file:///{successIcon}");

            if (Password == SecureStorage.GetPassword())
            {
                // Display a toast notification for successful login
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Login Successful")
                    .AddText("Welcome back to Cipher Shield.")
                    .Show();
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                // Display a toast notification for failed login
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                     .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Login failed!")
                    .AddText("Check your password or click on Load Password.")
                    .Show();
                return;
            }
        }

        // Cancel the login process
        private void CancelLoginMasterPwdBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        // Load the master password from the backup file
        private void LoginPwdLoadBackupBtn_Click(object sender, EventArgs e)
        {
            RecoverPassword recoverPass = new RecoverPassword(this);
            recoverPass.Show();
        }
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            focusBtn.Focus();
            ActiveForm.WindowState = FormWindowState.Minimized;
        }

        // Load the password into the password TextBox
        private void LoadLockPasswordBtn_Click(object sender, EventArgs e)
        {
            SecureStorage.LoadBackupPassword(LoginMasterPwdTxtBox);
        }

        // method to show input
        private void hideShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "EyeWhite.png"));
            LoginMasterPwdTxtBox.PasswordChar = '\0';
        }

        // method to hide input
        private void hideShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "NotEyeWhite.png"));
            LoginMasterPwdTxtBox.PasswordChar = '*';
        }
    }

}
