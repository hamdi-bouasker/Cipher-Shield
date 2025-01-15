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
            string infoIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "info.png");
            Uri infoUri = new Uri($"file:///{infoIcon}");

            if (Password == SecureStorage.GetPassword())
            {
                // Display a toast notification for successful login
                new ToastContentBuilder()
                    .AddAppLogoOverride(infoUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Login Successful")
                    .AddText("Welcome Back to Cipher Shield")
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
                    .AddText("Check Your Password or Click on Load Master Password")
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
            ActiveForm.WindowState = FormWindowState.Minimized;
        }

        // Load the password into the password TextBox
        private void LoadLockPasswordBtn_Click(object sender, EventArgs e)
        {
            SecureStorage.LoadBackupPassword(LoginMasterPwdTxtBox);
        }

        private void hideShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "OpenedEye.png"));
            LoginMasterPwdTxtBox.PasswordChar = '\0';
        }

        private void hideShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "ClosedEye.png"));
            LoginMasterPwdTxtBox.PasswordChar = '*';
        }
    }

}
