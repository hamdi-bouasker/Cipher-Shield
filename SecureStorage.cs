using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.Toolkit.Uwp.Notifications;

namespace CipherShield
{
    public static class SecureStorage
    {
        private static readonly byte[] backupKey = { 132, 42, 53, 84, 75, 96, 37, 28, 99, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };

        // Additional entropy for the password encryption
        private static readonly byte[] AdditionalEntropy = new byte[]
        {
        91, 182, 173, 64, 155, 246, 137, 228,
        19, 200, 211, 122, 133, 144, 255, 166,
        177, 188, 199, 210, 221, 232, 243, 254,
        165, 176, 187, 198, 209, 220, 231, 242
        };

        // Save the master password to a file
        public static void SavePassword(string password)
        {
            byte[] encryptedPassword = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(password), AdditionalEntropy, DataProtectionScope.CurrentUser);
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists
            string filePath = Path.Combine(appDataPath, "Master-Password.dat");
            File.WriteAllBytes(filePath, encryptedPassword);
        }


    // Retrieve the master password from the file
    public static string GetPassword()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            string filePath = Path.Combine(appDataPath, "Master-Password.dat");

            byte[] encryptedPassword = File.ReadAllBytes(filePath);
            byte[] decryptedPassword = ProtectedData.Unprotect(
                encryptedPassword, AdditionalEntropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedPassword);
        }

    // Retrieve the answers of the security questions
    public static string[] GetSecurityAnswers()
    {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");

            // Read the encrypted answers
            byte[][] encryptedAnswers = new byte[3][];
            for (int i = 0; i < 3; i++)
            {
                encryptedAnswers[i] = File.ReadAllBytes(Path.Combine(appDataPath, $"Security-Answer-{i + 1}.dat"));
            }

            // Decrypt the answers
            string[] decryptedAnswers = new string[3];
            for (int i = 0; i < 3; i++)
            {
                byte[] decryptedAnswerBytes = ProtectedData.Unprotect(encryptedAnswers[i], AdditionalEntropy, DataProtectionScope.CurrentUser);
                decryptedAnswers[i] = Encoding.UTF8.GetString(decryptedAnswerBytes);
            }

            return decryptedAnswers;
        }

        // Update the master password
        public static void UpdatePassword(string newPassword) 
        { 
            SavePassword(newPassword); 
        }

        // change the master password
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

        // backup the password to user's preferred location
        public static void BackupPassword(string password)
        {           
            byte[] encryptedPassword = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(password), AdditionalEntropy, DataProtectionScope.CurrentUser);

            // Create and configure the SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*",
                FileName = "Lock-Password.dat",
                Title = "Save Master Password"
            };

            // Show the dialog and get the user-selected file path
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllBytes(filePath, encryptedPassword);
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Password saved successfully.")
                    .Show();
                return;
            }
            else
            {
                string WarningIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "warning.png");
                Uri WarningUri = new Uri($"file:///{WarningIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(WarningUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Password save cancelled.")
                    .Show();
                return;
            }
        }

        // save security answers
        public static void SaveSecurityQuestions(string[] answers)
        {
            // Encrypt security answers

            byte[][] encryptedAnswers = new byte[3][];
            for (int i = 0; i < 3; i++)
            {
                encryptedAnswers[i] = ProtectedData.Protect(
                    Encoding.UTF8.GetBytes(answers[i]), AdditionalEntropy, DataProtectionScope.CurrentUser);
            }

            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists

            // Save encrypted answers
            for (int i = 0; i < 3; i++)
            {
                File.WriteAllBytes(Path.Combine(appDataPath, $"Security-Answer-{i + 1}.dat"), encryptedAnswers[i]);
            }
        }

        // recover password using secuirty questions
        public static string RecoverPassword(string[] userAnswers)
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");

            // Read the encrypted answers
            byte[][] encryptedAnswers = new byte[3][];
            for (int i = 0; i < 3; i++)
            {
                encryptedAnswers[i] = File.ReadAllBytes(Path.Combine(appDataPath, $"Security-Answer-{i + 1}.dat"));
            }

            // Decrypt and compare the stored answers with the provided answers
            for (int i = 0; i < 3; i++)
            {
                byte[] decryptedAnswerBytes = ProtectedData.Unprotect(encryptedAnswers[i], AdditionalEntropy, DataProtectionScope.CurrentUser);
                string storedAnswer = Encoding.UTF8.GetString(decryptedAnswerBytes);

                if (userAnswers[i] != storedAnswer)
                {
                    string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                    Uri errorUri = new Uri($"file:///{errorIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                        .AddText("One or more of the provided answers are incorrect.")
                        .Show();
                    return null;
                }
            }

            // If all answers and password are correct, decrypt and return the password
            byte[] encryptedPassword = File.ReadAllBytes(Path.Combine(appDataPath, "Master-Password.dat"));
            byte[] decryptedPasswordBytes = ProtectedData.Unprotect(encryptedPassword, AdditionalEntropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedPasswordBytes);
        }

        public static string DecryptPassword(byte[] encryptedData)
        {
            Aes aes = Aes.Create();
            aes.Key = backupKey;

            // Get the IV from the beginning of the encrypted data
            byte[] iv = new byte[16];
            Array.Copy(encryptedData, 0, iv, 0, iv.Length);
            aes.IV = iv;

            // Get the encrypted password (skip the IV)
            MemoryStream msDecrypt = new MemoryStream(encryptedData, iv.Length, encryptedData.Length - iv.Length);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }


        public static string RecoverEncryptionPassword(string[] userAnswers, string masterPassword)
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");

            // Read the encrypted answers
            byte[][] encryptedAnswers = new byte[3][];
            for (int i = 0; i < 3; i++)
            {
                encryptedAnswers[i] = File.ReadAllBytes(Path.Combine(appDataPath, $"Security-Answer-{i + 1}.dat"));
            }

            // Decrypt and compare the stored answers with the provided answers
            for (int i = 0; i < 3; i++)
            {
                byte[] decryptedAnswerBytes = ProtectedData.Unprotect(encryptedAnswers[i], AdditionalEntropy, DataProtectionScope.CurrentUser);
                string storedAnswer = Encoding.UTF8.GetString(decryptedAnswerBytes);

                if (userAnswers[i] != storedAnswer || masterPassword != GetPassword())
                {
                    string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "error.png");
                    Uri errorUri = new Uri($"file:///{errorIcon}");
                    new ToastContentBuilder()
                        .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                        .AddText("One or more of the provided answers are incorrect.")
                        .Show();
                    return null;
                }

            }

            // If all answers and lock password are correct, decrypt and return the files encryption password
            byte[] encryptedPassword = File.ReadAllBytes(Path.Combine(appDataPath, "FilesEncryptionPassword.pwd"));
            string decryptedPassword = DecryptPassword(encryptedPassword);
            return decryptedPassword;
        }

        // load backup password
        public static void LoadBackupPassword(TextBox textBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*",
                Title = "Open Master Password"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                byte[] encryptedPassword = File.ReadAllBytes(filePath);
                byte[] decryptedPassword = ProtectedData.Unprotect(
                    encryptedPassword, AdditionalEntropy, DataProtectionScope.CurrentUser);
                string password = Encoding.UTF8.GetString(decryptedPassword);
                textBox.Text = password;
                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("The password has been loaded successfully.")
                    .Show();
                return;
            }
            else
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "warning.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Password load cancelled.")
                    .Show();
                return;
            }
        }
    }
}
