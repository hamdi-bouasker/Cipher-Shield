using System;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;
using System.Drawing;


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
            if (SecureStorage.RecoverPassword(txtBoxes) == SecureStorage.GetPassword())
            {
                loginForm.LoginMasterPwdTxtBox.Text = SecureStorage.GetPassword();
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Your password is successfully loaded in the login password tab.")
                    .Show();
                Close();
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

        private void hideShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "OpenedEyeBlack.png"));
            SecurityQuestion1txtBox.PasswordChar = '\0';
            SecurityQuestion2txtBox.PasswordChar = '\0';
            SecurityQuestion3txtBox.PasswordChar = '\0';
        }

        private void hideShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "ClosedEyeBlack.png"));
            SecurityQuestion1txtBox.PasswordChar = '*';
            SecurityQuestion2txtBox.PasswordChar = '*';
            SecurityQuestion3txtBox.PasswordChar = '*';
        }
    }
}
