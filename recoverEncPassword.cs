using System;
using System.Windows.Forms;
using Microsoft.Toolkit.Uwp.Notifications;
using System.IO;
using System.Drawing;
using System.Linq;

namespace CipherShield
{
    public partial class recoverEncPassword : Form
    {
        private MainForm mainForm;
        public recoverEncPassword(MainForm mainForm)
        {
            InitializeComponent();
            this.menuBarLbl.MakeDraggable(this); // Make the form draggable
            this.mainForm = mainForm;
        }

        // recover the password used in files encryption
        private void SubmitRecoverPwdBtn_Click(object sender, EventArgs e)
        {
            if (SecurityQuestion1txtBox.Text.Length == 0 || SecurityQuestion2txtBox.Text.Length == 0 || SecurityQuestion3txtBox.Text.Length == 0 || lockPasswordTxtBox.Text.Length == 0)
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("All inputs are mandatory.")
                    .Show();
                return;
            }

            string[] txtBoxes = { SecurityQuestion1txtBox.Text, SecurityQuestion2txtBox.Text, SecurityQuestion3txtBox.Text };
            string filesEncpass = lockPasswordTxtBox.Text;
           
            if (txtBoxes.SequenceEqual(SecureStorage.GetSecurityAnswers()) && filesEncpass == SecureStorage.GetPassword())
            {
                mainForm.FilesEncryptionEnterPwdTxtBox.Text = SecureStorage.RecoverEncryptionPassword(txtBoxes, filesEncpass);
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Successful password recover.")
                    .AddText("Your password is loaded in the password tab. Save it to a safe place.")
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
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "EyeBlack.png"));
            SecurityQuestion1txtBox.PasswordChar = '\0';
            SecurityQuestion2txtBox.PasswordChar = '\0';
            SecurityQuestion3txtBox.PasswordChar = '\0';
            lockPasswordTxtBox.PasswordChar = '\0';
        }

        private void hideShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "NotEyeBlack.png"));
            SecurityQuestion1txtBox.PasswordChar = '*';
            SecurityQuestion2txtBox.PasswordChar = '*';
            SecurityQuestion3txtBox.PasswordChar = '*';
            lockPasswordTxtBox.PasswordChar = '*';
        }
    }
}
