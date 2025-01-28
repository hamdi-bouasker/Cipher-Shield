using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Linq;
using System.Windows.Input;

namespace CipherShield
{
    public static class SecureStorage
    {
        private static string hexStringLockKey = "f3a4b5c6d7e8091a2b3c4d5e6f7181920a1b2c3d4e5f60718293a4b5c6d7e8f9";
        private static readonly byte[] backupLockKey = HexStringToByteArray(hexStringLockKey);
        private static readonly byte[] hashedLockKey = HashKeyWithSHA256(backupLockKey);

        private static string hexStringSecurityQuestionsKey = "d3e4f5a6b7c8091a2b3c4d5e6f7081920a1b2c3d4e5f60718293a4b5c6d7e8f9";
        private static readonly byte[] backupSecurityQuestionsKey = HexStringToByteArray(hexStringSecurityQuestionsKey);
        private static readonly byte[] hashedSecurityQuestionsKey = HashKeyWithSHA256(backupSecurityQuestionsKey);

        private static string hexStringFilesEncKey = "a1b2c3d4e5f60718293a4b5c6d7e8f90a1b2c3d4e5f60718293a4b5c6d7e8f90";
        private static readonly byte[] backupFilesEncKey = HexStringToByteArray(hexStringFilesEncKey);
        private static readonly byte[] hashedFilesEncKey = HashKeyWithSHA256(backupFilesEncKey);

        // method to convert hex string of 64 bytes to byte array
        static byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                string byteValue = hex.Substring(i, 2);
                bytes[i / 2] = Convert.ToByte(byteValue, 16);
            }

            return bytes;
        }

        // method to hash the key using SHA256
        private static byte[] HashKeyWithSHA256(byte[] key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(key);
            }
        }


        // method to encrypt the master password
        private static byte[] EncryptLockPassword(string password)
        {
            Aes aes = Aes.Create();
            aes.Key = hashedLockKey;
            aes.GenerateIV(); // Generate a new IV for each encryption
            byte[] iv = aes.IV;

            MemoryStream msEncrypt = new MemoryStream();
            // Write the IV to the beginning of the file
            msEncrypt.Write(iv, 0, iv.Length);

            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(password);
            }

            return msEncrypt.ToArray();
        }

        // method to decrypt the master password
        private static string DecryptLockPassword(byte[] encryptedData)
        {
            Aes aes = Aes.Create();
            aes.Key = hashedLockKey;

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

        // method to encrypt the password used to encrypt files
        public static byte[] EncryptFilesPassword(string password)
        {
            Aes aes = Aes.Create();
            aes.Key = hashedFilesEncKey;
            aes.GenerateIV(); // Generate a new IV for each encryption
            byte[] iv = aes.IV;

            MemoryStream msEncrypt = new MemoryStream();
            // Write the IV to the beginning of the file
            msEncrypt.Write(iv, 0, iv.Length);

            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(password);
            }

            return msEncrypt.ToArray();
        }

        // method to decrypt the password used to decrypt files
        public static string DecryptFilesPassword(byte[] encryptedData)
        {
            Aes aes = Aes.Create();
            aes.Key = hashedFilesEncKey;

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

        // method to encrypt the password used to encrypt the answers of security questions
        private static byte[] EncryptSecurityQuestionsPassword(string password)
        {
            Aes aes = Aes.Create();
            aes.Key = hashedSecurityQuestionsKey;
            aes.GenerateIV(); // Generate a new IV for each encryption
            byte[] iv = aes.IV;

            MemoryStream msEncrypt = new MemoryStream();
            // Write the IV to the beginning of the file
            msEncrypt.Write(iv, 0, iv.Length);

            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(password);
            }

            return msEncrypt.ToArray();
        }

        // method to decrypt the password used to encrypt the answers of security questions
        private static string DecryptSecurityQuestionsPassword(byte[] encryptedData)
        {
            Aes aes = Aes.Create();
            aes.Key = hashedSecurityQuestionsKey;

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

        // Save the master password to a file

        public static void SaveMasterPassword(string password)
        {
            // Encrypt the password using the new encryption method
            byte[] encryptedPassword = EncryptLockPassword(password);

            // Save the encrypted password to a file
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists
            string filePath = Path.Combine(appDataPath, "Master-Password.dat");

            File.WriteAllBytes(filePath, encryptedPassword);
        }

        // Retrieve the master password from the file
        public static string GetMasterPassword()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            string filePath = Path.Combine(appDataPath, "Master-Password.dat");

            byte[] encryptedData = File.ReadAllBytes(filePath);

            string decryptedPassword = DecryptLockPassword(encryptedData);

            return decryptedPassword;
        }

        // save security answers
        public static void SaveSecurityQuestions(string[] answers)
        {
            byte[][] encryptedAnswers = new byte[3][];

            for (int i = 0; i < 3; i++)
            {
                encryptedAnswers[i] = EncryptSecurityQuestionsPassword(answers[i]);
            }

            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists

            for (int i = 0; i < 3; i++)
            {
                File.WriteAllBytes(Path.Combine(appDataPath, $"Security-Answer-{i + 1}.dat"), encryptedAnswers[i]);
            }
        }


        // Retrieve the answers of the security questions
        public static string[] GetSecurityAnswers()
        {
            string[] answers = new string[3];
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");

            for (int i = 0; i < 3; i++)
            {
                string filePath = Path.Combine(appDataPath, $"Security-Answer-{i + 1}.dat");
                byte[] encryptedData = File.ReadAllBytes(filePath);

                answers[i] = DecryptSecurityQuestionsPassword(encryptedData);
            }

            return answers;
        }

        // Update the master password
        public static void UpdatePassword(string newPassword)
        {
            SaveMasterPassword(newPassword);
        }

        // change the database password
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

        // backup the master password to user's preferred location
        public static void BackupPassword(string password)
        {
            // Encrypt the password using the new encryption method
            byte[] encryptedPassword = EncryptLockPassword(password);

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
                
            }
            else
            {
                string warningIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "warning.png");
                Uri warningUri = new Uri($"file:///{warningIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(warningUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Password save cancelled.")
                    .Show();
            }
        }

        // load backup lock password
        public static void LoadBackupPassword(TextBox textBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*",
                Title = "Open Lock Password"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                byte[] encryptedData = File.ReadAllBytes(filePath);

                string password = DecryptLockPassword(encryptedData);
                textBox.Text = password;

                string successIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "success.png");
                Uri successUri = new Uri($"file:///{successIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(successUri, ToastGenericAppLogoCrop.Default)
                    .AddText("The password has been loaded successfully.")
                    .Show();
            }
            else
            {
                string errorIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "warning.png");
                Uri errorUri = new Uri($"file:///{errorIcon}");
                new ToastContentBuilder()
                    .AddAppLogoOverride(errorUri, ToastGenericAppLogoCrop.Default)
                    .AddText("Password load cancelled.")
                    .Show();
            }
        }

        // Save the password used to encrypt files
        public static void SaveEncPassword(string password)
        {
            // Encrypt the password using the new encryption method
            byte[] encryptedPassword = EncryptFilesPassword(password);

            // Save the encrypted password to a file
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists
            string filePath = Path.Combine(appDataPath, "Files-Encryption-Password.pwd");
            File.WriteAllBytes(filePath, encryptedPassword);
        }


        // Retrieve the password used to encrypt files
        public static string GetEncPassword()
        {
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            string filePath = Path.Combine(appDataPath, "Files-Encryption-Password.pwd");
            byte[] encryptedData = File.ReadAllBytes(filePath);

            string decryptedPassword = DecryptFilesPassword(encryptedData);

            return decryptedPassword;
        }
    }
}
