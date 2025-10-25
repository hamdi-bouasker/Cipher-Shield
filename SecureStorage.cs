using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.Toolkit.Uwp.Notifications;

namespace CipherShield
{
    public static class SecureStorage
    {
        private static readonly DataProtectionScope Scope = DataProtectionScope.CurrentUser;

        // --- DPAPI Wrappers ---
        private static byte[] EncryptWithDPAPI(string plainText)
        {
            byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return ProtectedData.Protect(plainBytes, null, Scope);
        }

        private static string DecryptWithDPAPI(byte[] protectedData)
        {
            byte[] plainBytes = ProtectedData.Unprotect(protectedData, null, Scope);
            return System.Text.Encoding.UTF8.GetString(plainBytes);
        }

        // --- Master Password ---
        public static void SaveMasterPassword(string password)
        {
            byte[] protectedPassword = EncryptWithDPAPI(password);
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath);
            string filePath = Path.Combine(appDataPath, "Master-Password.dat");
            File.WriteAllBytes(filePath, protectedPassword);
        }

        public static string GetMasterPassword()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield", "Master-Password.dat");
            byte[] encryptedData = File.ReadAllBytes(filePath);
            return DecryptWithDPAPI(encryptedData);
        }

        // --- Security Questions ---
        public static void SaveSecurityQuestions(string[] answers)
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath);

            for (int i = 0; i < 3; i++)
            {
                byte[] encrypted = EncryptWithDPAPI(answers[i]);
                File.WriteAllBytes(Path.Combine(appDataPath, $"Security-Answer-{i + 1}.dat"), encrypted);
            }
        }

        public static string[] GetSecurityAnswers()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            string[] answers = new string[3];

            for (int i = 0; i < 3; i++)
            {
                byte[] data = File.ReadAllBytes(Path.Combine(appDataPath, $"Security-Answer-{i + 1}.dat"));
                answers[i] = DecryptWithDPAPI(data);
            }

            return answers;
        }

        // --- File Encryption Password ---
        public static void SaveEncPassword(string password)
        {
            byte[] encryptedPassword = EncryptWithDPAPI(password);
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath);
            string filePath = Path.Combine(appDataPath, "Files-Encryption-Password.pwd");
            File.WriteAllBytes(filePath, encryptedPassword);
        }

        public static string GetEncPassword()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield", "Files-Encryption-Password.pwd");
            byte[] encryptedData = File.ReadAllBytes(filePath);
            return DecryptWithDPAPI(encryptedData);
        }

        // --- Other Utilities ---
        public static void UpdatePassword(string newPassword) => SaveMasterPassword(newPassword);

        public static void ChangeDatabasePassword(string databaseFilePath, string oldPassword, string newPassword)
        {
            using (var connection = new SqliteConnection($"Data Source={databaseFilePath};Password={oldPassword};"))
            {
                connection.Open();
                using (var command = new SqliteCommand($"PRAGMA rekey = '{newPassword}';", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void BackupPassword(string password)
        {
            byte[] protectedPassword = EncryptWithDPAPI(password);
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*",
                FileName = "Lock-Password.dat",
                Title = "Save Master Password"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(saveDialog.FileName, protectedPassword);
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                new ToastContentBuilder()
                    .AddAppLogoOverride(new Uri($"file:///{successIcon}"))
                    .AddText("Password saved successfully.")
                    .Show();
            }
        }

        public static void LoadBackupPassword(TextBox textBox)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*",
                Title = "Open Lock Password"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string password = DecryptWithDPAPI(File.ReadAllBytes(openDialog.FileName));
                textBox.Text = password;

                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                new ToastContentBuilder()
                    .AddAppLogoOverride(new Uri($"file:///{successIcon}"))
                    .AddText("The password has been loaded successfully.")
                    .Show();
            }
        }
    }
}
