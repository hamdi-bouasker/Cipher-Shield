using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CipherShield.Models;
using CipherShield.Helpers;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Text;
using System.Runtime.InteropServices;
using Windows.UI.Notifications;



namespace CipherShield
{
    public partial class MainForm : Form
    {
        private List<string> selectedFiles1 = new List<string>();
        private List<string> selectedFiles2 = new List<string>();
        private readonly byte[] backupKey = { 132, 42, 53, 84, 75, 96, 37, 28, 99, 10, 11, 12, 13, 14, 15, 16 };
        private DatabaseHelper db;
        private int counter = 0;
        string[] hints = { "It's always a great idea to backup your files to the cloud and to an external drive.", "Always backup your passwords to different safe places.", "The more backups you do, the easier to restore.", "Consider backup your important files by printing them.", "Daily system backup to an external drive is your best choice." };
        string[] Hints = { "A stitch in time saves nine.", "Always secure your files from curious eyes.", "Your password should be at least 16 random characters long.", "Don't be lazy, always make a new strong password.", "Daily system backup is your best friend." };
        // mainform method
        public MainForm()
        {
            InitializeComponent();
            this.MakeDraggable(); // Make the form draggable
            this.menuBarLbl.MakeDraggable(this); // Make the form draggable
            timer1.Start(); // timer for the hintlabel in help tab
            timer2.Start(); // timer for the aboutlabel in about tab
            // Initialize SQLitePCL to use SQLCipher
            SQLitePCL.Batteries_V2.Init();
            PasswordManagerDGV.SelectionChanged += DataGridView1_SelectionChanged;

            // Attach the Load event handler
            this.Load += new EventHandler(this.MainForm_Load);
        }


        // if no registered password, show the register form, else show the login form
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetControlsEnabled(false); // Method to disable controls
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            string filePath = Path.Combine(appDataPath, "Master-Password.dat");
            if (!File.Exists(filePath))
            {
                RegisterMasterPasswordForm registerMasterPasswordForm = new RegisterMasterPasswordForm();
                registerMasterPasswordForm.ShowDialog();
                if (registerMasterPasswordForm.DialogResult == DialogResult.OK)
                {
                    string password = registerMasterPasswordForm.RegisterMasterPwdTxtBox.Text; // Ensure you have a public Password property
                    SecureStorage.SavePassword(password);
                    db = new DatabaseHelper(password); // Pass the password to DatabaseHelper constructor
                    SetControlsEnabled(true);
                    LoadData();
                }
                else
                {
                    Close();
                }
            }
            else
            {
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
                if (loginForm.DialogResult == DialogResult.OK)
                {
                    string password = loginForm.LoginMasterPwdTxtBox.Text;
                    db = new DatabaseHelper(password); // Pass the password to DatabaseHelper constructor
                    SetControlsEnabled(true);
                    LoadData();
                }
                else
                {
                    Close();
                }
            }
        }

        // Method to disable controls
        private void SetControlsEnabled(bool enabled)
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = enabled;
            }
        }

        // timer for the hintlabel in help tab

        private void timer1_Tick(object sender, EventArgs e)
        {
            homeCarousselLbl.Text = hints[counter];
            counter = (counter + 1) % hints.Length;
        }

        // timer for the hintlabel in about tab
        private void timer2_Tick(object sender, EventArgs e)
        {
            aboutCarousselLbl.Text = Hints[counter];
            counter = (counter + 1) % Hints.Length;
        }

        // close the form
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        // minimize the form
        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            focusBtn.Focus();
            ActiveForm.WindowState = FormWindowState.Minimized;
        }

        // method to show notification
        private void ShowNotification(string message, string iconFileName)
        {
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", iconFileName);
            Uri iconUri = new Uri($"file:///{iconPath}");
            new ToastContentBuilder()
                .AddAppLogoOverride(iconUri, ToastGenericAppLogoCrop.Default)
                .AddText(message)
                .Show();
        }

        // change master password
        private void SubmitNewPasswordBtn_Click(object sender, EventArgs e)
        {

            string currentPassword = SecureStorage.GetPassword();
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Cipher Shield");
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists
            string dbFilePath = Path.Combine(appDataPath, "credentials.db");

            if (oldPasswordTxtBox.Text != currentPassword)
            {
                ShowNotification("Current password is incorrect.", "error.png");
                return;
            }

            if (NewPasswordTxtBox.Text.Length == 0)
            {
                ShowNotification("Please enter a new password.", "error.png");
                return;
            }

            if (NewPasswordTxtBox.Text != RepeatNewPasswordTxtBox.Text)
            {
                ShowNotification("New passwords do not match.", "error.png");
                return;
            }

            if (NewPasswordTxtBox.Text.Length < 8)
            {
                ShowNotification("Password must be at least 8 characters long.", "error.png");
                return;
            }
            SecureStorage.ChangeDatabasePassword(dbFilePath, currentPassword, RepeatNewPasswordTxtBox.Text);
            SecureStorage.UpdatePassword(RepeatNewPasswordTxtBox.Text);
            ShowNotification("The lock password has been successfully changed." +
                Environment.NewLine + " Cipher Shield will be restarted.", "success.png");
            Application.Restart();
        }

        // method to generate a password
        private string GeneratePassword(int length = 16)
        {
            const string uppercase = "NZAYBXCWDVEUFTGSHRIJPKOLM";
            const string lowercase = "mlokpjirhsgtfuevdwcxbyazn";
            const string numbers = "73281564";
            const string special = "]!@-)#}=/$%>|&*(+{<?[";

            // Ensure length is divisible by 4
            length = length - (length % 4);

            var chars = new char[length];
            var randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            int quarterLength = length / 4;

            // Fill exactly 1/4 with uppercase
            for (int i = 0; i < quarterLength; i++)
            {
                chars[i] = uppercase[randomBytes[i] % uppercase.Length];
            }

            // Fill exactly 1/4 with numbers
            for (int i = 0; i < quarterLength; i++)
            {
                chars[2 * quarterLength + i] = numbers[randomBytes[2 * quarterLength + i] % numbers.Length];
            }

            // Fill exactly 1/4 with lowercase
            for (int i = 0; i < quarterLength; i++)
            {
                chars[quarterLength + i] = lowercase[randomBytes[quarterLength + i] % lowercase.Length];
            }

            // Fill exactly 1/4 with special characters
            for (int i = 0; i < quarterLength; i++)
            {
                chars[3 * quarterLength + i] = special[randomBytes[3 * quarterLength + i] % special.Length];
            }

            // Shuffle the entire password to mix the characters
            return new string(chars.OrderBy(x => randomBytes[new Random().Next(randomBytes.Length)]).ToArray());
        }

        #region Password Generator Tab

        // method to generate passwords
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            PasswordGeneratorGeneratedPwdTextBox.Clear();
            int count = (int)PasswordGeneratorCountNumeric.Value;
            int Length = (int)PasswordGeneratorLengthNumeric.Value;

            for (int i = 0; i < count; i++)
            {
                string password = GeneratePassword(Length);
                PasswordGeneratorGeneratedPwdTextBox.AppendText($"Password {i + 1}: {password}\r\n");
            }

            // Enable buttons if passwords were generated
            bool hasPasswords = PasswordGeneratorGeneratedPwdTextBox.Text.Length > 0;
            PasswordGeneratorCopyPwdBtn.Enabled = hasPasswords;
            PasswordGeneratorExportPwdBtn.Enabled = hasPasswords;
            PasswordGeneratorClearPwdGenBtn.Enabled = hasPasswords;
        }

        // method to copy passwords
        private void copyPwdBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordGeneratorGeneratedPwdTextBox.Text))
            {
                try
                {
                    Clipboard.Clear(); // Clear the clipboard first
                    Clipboard.SetDataObject(PasswordGeneratorGeneratedPwdTextBox.Text, true); // Use the copy retry option

                    PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
                    PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;

                    ShowNotification("Password copied.", "info.png");
                }
                catch (ExternalException ex)
                {
                    // Handle clipboard exception
                    ShowNotification("Clipboard operation failed: " + ex.Message, "error.png");
                }
            }
            else
            {
                ShowNotification("There is no password to copy.", "error.png");
            }
        }


        // export passwords
        private void exportPwdBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (!string.IsNullOrEmpty(PasswordGeneratorGeneratedPwdTextBox.Text) && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.Write(PasswordGeneratorGeneratedPwdTextBox.Text);
                    ShowNotification("The Passwords have been successfully exported to TXT file.", "success.png");
                    PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
                    PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;

                }
            }

        }

        // clear inputs
        private void clearPwdGenBtn_Click(object sender, EventArgs e)
        {
            PasswordGeneratorCountNumeric.Value = PasswordGeneratorCountNumeric.Minimum;
            PasswordGeneratorLengthNumeric.Value = PasswordGeneratorLengthNumeric.Minimum;
            PasswordGeneratorGeneratedPwdTextBox.Clear();
        }

        #endregion

        #region Password Manager Tab

        // load the data into datagridview
        private void LoadData()
        {
            var entries = db.GetAllEntries().ToList();
            PasswordManagerDGV.DataSource = entries;
            if (PasswordManagerDGV.Columns["Id"] != null)
            {
                PasswordManagerDGV.Columns["Id"].Visible = false;
            }

        }

        // method to select an entry and insert it into the textboxes
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = PasswordManagerDGV.SelectedRows[0];
                websiteTxtBox.Text = selectedRow.Cells["Website"].Value?.ToString();
                usernameTxtBox.Text = selectedRow.Cells["Username"].Value?.ToString();
                passwordTxtBox.Text = selectedRow.Cells["Password"].Value?.ToString();
            }
        }

        // add entry
        private void addButton_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(websiteTxtBox.Text) || string.IsNullOrEmpty(usernameTxtBox.Text) || string.IsNullOrEmpty(passwordTxtBox.Text))
            {
                ShowNotification("Please fill in all fields.", "error.png");
                return;
            }
            else
            {
                // Check for duplicate entry
                var existingEntry = db.GetAllEntries().FirstOrDefault(e => e.Website == websiteTxtBox.Text && e.Username == usernameTxtBox.Text && e.Password == passwordTxtBox.Text);

                if (existingEntry != null)
                {
                    ShowNotification("This entry already exists.", "error.png");
                }
                else
                {
                    PasswordEntry entry = new PasswordEntry
                    {
                        Website = websiteTxtBox.Text,
                        Username = usernameTxtBox.Text,
                        Password = passwordTxtBox.Text
                    };
                    db.AddEntry(entry);
                    LoadData();
                    ClearInputFields();
                    ShowNotification("The entry has been successfully added.", "success.png");
                }
            }
        }


        // select entry and edit/update it
        private void editButton_Click_1(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.CurrentRow != null)
            {
                int id = (int)PasswordManagerDGV.CurrentRow.Cells["Id"].Value;
                var entry = db.GetAllEntries().FirstOrDefault(e => e.Id == id);
                if (entry != null)
                {
                    // Get the new values from the text boxes
                    string newWebsite = websiteTxtBox.Text;
                    string newUsername = usernameTxtBox.Text;
                    string newPassword = passwordTxtBox.Text;

                    // Check if any changes were made
                    if (entry.Website != newWebsite || entry.Username != newUsername || entry.Password != newPassword)
                    {
                        // Update the entry with the new values
                        entry.Website = newWebsite;
                        entry.Username = newUsername;
                        entry.Password = newPassword;
                        db.UpdateEntry(entry);

                        ShowNotification("Successfully updated:" + Environment.NewLine + $"Website: {entry.Website}" + Environment.NewLine + $"Username: {entry.Username}" + Environment.NewLine + $"Password: {entry.Password}", "success.png");
                        LoadData();
                        ClearInputFields();
                    }
                    else
                    {
                        ShowNotification("No changes detected. The entry remains the same.", "info.png");
                    }
                }
            }
            else
            {
                ShowNotification("Please select an entry to update.", "error.png");
            }
        }


        // delete entry

        private void deleteButton_Click_1(object sender, EventArgs e)
            {
                if (PasswordManagerDGV.CurrentRow != null)
                {
                    string warningIcon = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "warning.png");
                    Uri warningUri = new Uri($"file:///{warningIcon}");
                    // Create the confirmation toast notification
                    var toastContent = new ToastContentBuilder()
                        .AddAppLogoOverride(warningUri, ToastGenericAppLogoCrop.Default)
                        .AddText("Are you sure you want to delete this entry?")
                        .AddButton(new ToastButton()
                            .SetContent("Yes")
                            .AddArgument("action", "delete")
                            .SetBackgroundActivation())
                        .AddButton(new ToastButton()
                            .SetContent("No")
                            .AddArgument("action", "cancel")
                            .SetBackgroundActivation())
                        .GetToastContent();

                    var toastNotification = new ToastNotification(toastContent.GetXml());

                    // Register the notification
                    ToastNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);

                    // Subscribe to toast activated event
                    ToastNotificationManagerCompat.OnActivated += toastArgs =>
                    {
                        if (toastArgs.Argument == "action=delete")
                        {
                            // Proceed with deletion
                            int id = (int)PasswordManagerDGV.CurrentRow.Cells[0].Value;
                            db.DeleteEntry(id);
                            LoadData();
                            ClearInputFields();
                            ShowNotification("The entry has been deleted.", "info.png");
                        }
                        else if (toastArgs.Argument == "action=cancel")
                        {
                            ShowNotification("Deletion canceled.", "info.png");
                        }
                    };
                }
                else
                {
                    ShowNotification("Please select an entry to delete.", "error.png");
                }
            }


    // clear inputs
    private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        // clear inputs
        private void ClearInputFields()
        {
            websiteTxtBox.Clear();
            usernameTxtBox.Clear();
            passwordTxtBox.Clear();
            LoadData();
        }

        // visualize an entry
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PasswordManagerDGV.SelectionChanged += DataGridView1_SelectionChanged;
        }

        // print button
        private void PasswordManagerPrintBtn_Click(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.Rows.Count == 0)
            {
                ShowNotification("There is no data to print.", "error.png");
                return;
            }

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog { Document = printDocument };
                printPreviewDialog.ShowDialog();

            }
        }

        // method to print all entries
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs ev)
        {
            int y = ev.MarginBounds.Top;
            int x = ev.MarginBounds.Left;
            int columnWidth = 200;
            int rowHeight = 30;

            // Set up colors
            Brush headerBrush = new SolidBrush(SystemColors.Highlight);
            Pen linePen = new Pen(Color.MidnightBlue);
            Brush textBrush = Brushes.Black;
            Brush headerFontBrush = Brushes.White;

            // Set fonts
            Font printFont = new Font("Verdana", 10);
            Font headerFont = new Font("Verdana", 10, FontStyle.Bold);

            // Print headers
            foreach (DataGridViewColumn column in PasswordManagerDGV.Columns)
            {
                if (column.Visible)
                {
                    // Draw header background
                    ev.Graphics.FillRectangle(headerBrush, x, y, columnWidth, rowHeight);
                    // Measure the width of the header text
                    SizeF headerTextSize = ev.Graphics.MeasureString(column.HeaderText, headerFont);
                    // Calculate the X position to center the text
                    float textX = x + (columnWidth - headerTextSize.Width) / 2;
                    // Draw header text
                    ev.Graphics.DrawString(column.HeaderText, headerFont, headerFontBrush, textX, y + 5);
                    // Draw header border
                    ev.Graphics.DrawRectangle(linePen, x, y, columnWidth, rowHeight);
                    x += columnWidth;
                }
            }

            y += rowHeight;
            x = ev.MarginBounds.Left;

            // Print rows
            foreach (DataGridViewRow row in PasswordManagerDGV.Rows)
            {
                x = ev.MarginBounds.Left;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (PasswordManagerDGV.Columns[cell.ColumnIndex].Visible)
                    {
                        // Draw cell border
                        ev.Graphics.DrawRectangle(linePen, x, y, columnWidth, rowHeight);
                        // Draw cell text
                        ev.Graphics.DrawString(cell.Value?.ToString(), printFont, textBrush, x + 5, y + 5);
                        x += columnWidth;
                    }
                }
                y += rowHeight;
            }
        }


        // export all credentials to csv
        private void PasswordManagerExportBtn_Click(object sender, EventArgs e)
        {
            if (PasswordManagerDGV.Rows.Count == 0)
            {
                ShowNotification("There is no data to export.", "error.png");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.Title = "Save an Export File";
                sfd.ShowDialog();

                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    ExportToCsv(sfd.FileName);
                    ShowNotification("The data has been successfully exported.", "success.png");
                }
            }
        }

        // method to export as csv
        private void ExportToCsv(string filePath)
        {
            var sb = new StringBuilder();

            var headers = PasswordManagerDGV.Columns.Cast<DataGridViewColumn>()
                            .Where(c => c.Visible)
                            .Select(column => $"\"{column.HeaderText}\"");
            sb.AppendLine(string.Join(",", headers));

            foreach (DataGridViewRow row in PasswordManagerDGV.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>()
                            .Where(c => PasswordManagerDGV.Columns[c.ColumnIndex].Visible)
                            .Select(cell => $"\"{cell.Value?.ToString()}\"");
                sb.AppendLine(string.Join(",", cells));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        #endregion

        #region Files Encryptor Tab

        // browse files to encrypt
        private void BrowseFiles_Click(object sender, EventArgs e)
        {
            FileEncryptionFilesNumberTxtBox.Clear();
            FilesEncryptionFilesListBox.Items.Clear();
            selectedFiles1.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (FilesEncryptionFilesListBox != null)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        if (!selectedFiles1.Contains(file))
                        {
                            selectedFiles1.Add(file);
                            FilesEncryptionFilesListBox.Items.Add(Path.GetFileName(file));
                        }
                    }
                }
                if (FileEncryptionFilesNumberTxtBox != null)
                    FileEncryptionFilesNumberTxtBox.Text = $"{selectedFiles1.Count} files selected";
            }
        }

        // backup the password used to encrypt the files

        private void BackupPasswordBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                ShowNotification("Please enter a password to backup.", "error.png");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Password files (*.pwd)|*.pwd|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.DefaultExt = "pwd";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] encryptedPassword = EncryptPassword(FilesEncryptionEnterPwdTxtBox.Text);
                    File.WriteAllBytes(sfd.FileName, encryptedPassword);
                    ShowNotification("The password backup file has been saved" + Environment.NewLine + "Keep this file safe.", "success.png");
                    return;
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error saving the password backup file: {ex.Message}", "error.png");
                    return;
                }
            }
        }

        // load the password used to encrypt the files
        private void LoadBackupButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Password files (*.pwd)|*.pwd|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] encryptedPassword = File.ReadAllBytes(ofd.FileName);
                    string decryptedPassword = DecryptPassword(encryptedPassword);
                    if (FilesEncryptionEnterPwdTxtBox != null)
                    {
                        FilesEncryptionEnterPwdTxtBox.Text = decryptedPassword;
                    }
                    ShowNotification("The password has been successfully loaded.", "success.png");
                    return;
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error loading the password backup: {ex.Message}", "error.png");
                    return;
                }
            }
            else
            {
                ShowNotification("No file selected.", "error.png");
                return;
            }
        }

        // method to encrypt the password used to encrypt the files
        private byte[] EncryptPassword(string password)
        {
            Aes aes = Aes.Create();
            aes.Key = backupKey;
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

        // method to decrypt the password used to encrypt the files
        private string DecryptPassword(byte[] encryptedData)
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

        // method to encrypt the selected files
        private async void EncryptButton_Click_1(object sender, EventArgs e)
        {
            if (selectedFiles1.Count == 0)
            {
                ShowNotification("Please select the files first.", "error.png");
                return;
            }

            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                ShowNotification("Please enter a password.", "error.png");
                return;
            }

            foreach (string file in selectedFiles1)
            {
                if (file.EndsWith("_ENCRYPTED" + Path.GetExtension(file)))
                {
                    ShowNotification("Select the files that are not already encrypted.", "error.png");
                    return;
                }
            }

            DisableControls();

            try
            {
                await Task.Run(async () =>
                {
                    await Parallel.ForEachAsync(selectedFiles1, async (file, cancellationToken) =>
                    {
                        try
                        {
                            await ProcessFileInPlace(file, FilesEncryptionEnterPwdTxtBox.Text, true);
                            string encryptedFile = Path.Combine(
                                Path.GetDirectoryName(file),
                                Path.GetFileNameWithoutExtension(file) + "_ENCRYPTED" + Path.GetExtension(file)
                            );
                            File.Move(file, encryptedFile); // Rename the file
                            Invoke(new Action(() => currentFileNameLabel.Text = Path.GetFileName(file))); // Update label with current file name
                            FilesEncryptionFilesListBox.Items.Clear();

                        }
                        catch (Exception ex)
                        {
                            ShowNotification($"Error encrypting {Path.GetFileName(file)}: {ex.Message}", "error.png");
                        }
                    });
                });
                ShowNotification("Your files have been successfully encrypted." + Environment.NewLine + "Please do not change the prefix added at the end of the filename: _ENCRYPTED" + Environment.NewLine + "It will be used in hte decryption process to distinguish the encrypted files.", "success.png");
            }
            catch (Exception ex)
            {
                ShowNotification($"Error during encryption: {ex.Message}", "error.png");
            }
            finally
            {
                EnableControls();
                currentFileNameLabel.Text = string.Empty;
            }
        }


        // method to decrypt the selected files
        private async void DecryptButton_Click_1(object sender, EventArgs e)
        {
            if (selectedFiles1.Count == 0)
            {
                ShowNotification("Please select the files first.", "error.png");
                return;
            }

            if (string.IsNullOrEmpty(FilesEncryptionEnterPwdTxtBox.Text))
            {
                ShowNotification("Please enter a password.", "error.png");
                return;
            }

            foreach (string file in selectedFiles1)
            {
                if (!file.EndsWith("_ENCRYPTED" + Path.GetExtension(file)))
                {
                    ShowNotification("Select the encrypted files only.", "error.png");
                    EnableControls();
                    return;
                }
            }

            DisableControls();

            try
            {
                await Task.Run(async () =>
                {
                    await Parallel.ForEachAsync(selectedFiles1, async (file, cancellationToken) =>
                    {
                        await ProcessFileInPlace(file, FilesEncryptionEnterPwdTxtBox.Text, false);
                        string decryptedFile = file.Substring(0, file.Length - "_ENCRYPTED".Length - Path.GetExtension(file).Length) + Path.GetExtension(file);
                        File.Move(file, decryptedFile); // Rename the file back to original
                        Invoke(new Action(() => currentFileNameLabel.Text = Path.GetFileName(file))); // Update label with current file name
                        FilesEncryptionFilesListBox.Items.Clear();
                    });
                    ShowNotification("Your files are successfully decrypted.", "success.png");
                });
            }
            catch (Exception ex)
            {
                ShowNotification($"Error during decryption: {ex.Message}", "error.png");
            }
            finally
            {
                EnableControls();
                currentFileNameLabel.Text = string.Empty;
            }
        }

        // method to diable controles during encryption and decryption
        private void DisableControls()
        {
            foreach (var button in Controls.OfType<Button>())
            {
                button.Enabled = false;
            }
            if (FilesEncryptionEnterPwdTxtBox.Text != null)
                FilesEncryptionEnterPwdTxtBox.Enabled = false;
        }

        // method to enable controles after encryption or after decryption
        private void EnableControls()
        {
            foreach (var button in Controls.OfType<Button>())
            {
                button.Enabled = true;
            }
            if (FilesEncryptionEnterPwdTxtBox.Text != null)
                FilesEncryptionEnterPwdTxtBox.Enabled = true;
        }

        // asyncronous method to encrypt/decrypt multiple files in parallel
        private async Task ProcessFileInPlace(string filePath, string password, bool encrypt)
        {
            string tempFile = Path.GetTempFileName();
            const int bufferSize = 81920; // 80 KB buffer size

            try
            {
                byte[] salt = new byte[32];
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                if (!encrypt)
                {
                    using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
                    {
                        await fsInput.ReadAsync(salt, 0, salt.Length);
                    }
                }
                else
                {
                    salt = GenerateRandomSalt();
                }

                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 100000, HashAlgorithmName.SHA256))
                {
                    byte[] key = pbkdf2.GetBytes(32);
                    byte[] iv = pbkdf2.GetBytes(16);

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = key;
                        aes.IV = iv;
                        aes.Padding = PaddingMode.PKCS7;

                        if (encrypt)
                        {
                            using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
                            using (FileStream fsTemp = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
                            {
                                await fsTemp.WriteAsync(salt, 0, salt.Length);

                                using (CryptoStream cs = new CryptoStream(fsTemp, aes.CreateEncryptor(), CryptoStreamMode.Write))
                                {
                                    byte[] buffer = new byte[bufferSize];
                                    int bytesRead;
                                    while ((bytesRead = await fsInput.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                    {
                                        await cs.WriteAsync(buffer, 0, bytesRead);
                                    }
                                    await cs.FlushFinalBlockAsync();
                                }
                            }
                        }
                        else
                        {
                            using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
                            using (FileStream fsTemp = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
                            {
                                fsInput.Seek(salt.Length, SeekOrigin.Begin);

                                using (CryptoStream cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                                {
                                    byte[] buffer = new byte[bufferSize];
                                    int bytesRead;
                                    while ((bytesRead = await cs.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                    {
                                        await fsTemp.WriteAsync(buffer, 0, bytesRead);
                                    }
                                    await fsTemp.FlushAsync();
                                }
                            }
                        }
                    }
                }

                File.Delete(filePath);
                File.Move(tempFile, filePath);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        // clear inputs
        private void clear_Click(object sender, EventArgs e)
        {
            FileEncryptionFilesNumberTxtBox.Clear();
            FilesEncryptionFilesListBox.Items.Clear();
            FilesEncryptionEnterPwdTxtBox.Clear();
            selectedFiles1.Clear();
        }

        // generate random salt for encryption key
        private byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // generate random password
        private void GeneratePasswordButton_Click(object sender, EventArgs e)
        {
            FilesEncryptionEnterPwdTxtBox.Text = GeneratePassword();
        }

        #endregion

        #region Files Renamer Tab

        // browse files to rename
        private void BrowseFilesBtnToRename_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedFiles2.Clear();
                RegexFilesListView.Items.Clear();

                try
                {
                    foreach (string filePath in ofd.FileNames)
                    {
                        var fileInfo = new FileInfo(filePath);
                        selectedFiles2.Add(fileInfo.FullName);
                        var item = new ListViewItem(new[] { fileInfo.Name, fileInfo.Name });
                        RegexFilesListView.Items.Add(item);
                    }

                    RegexPreviewChangesBtn.Enabled = selectedFiles2.Count > 0;
                }
                catch (Exception ex)
                {
                    ShowNotification($"Error loading the files: {ex.Message}", "error.png");
                }
            }
            else
            {
                ShowNotification("No files selected.", "error.png");
            }
        }

        // regex method
        private string GetNewFileName(FileInfo file, string pattern, string replacement, ref int counter)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
            string extension = file.Extension;
            string newName;

            if (pattern == "^")
            {
                // Add prefix
                newName = $"{replacement.Replace("{n}", counter.ToString())}{nameWithoutExtension}{extension}";
            }
            else if (pattern == "$")
            {
                // For suffix, we don't use regex replacement at all
                string suffix = replacement.Replace("{n}", counter.ToString());
                if (suffix.StartsWith("_"))
                {
                    newName = $"{nameWithoutExtension}{suffix}{extension}";
                }
                else
                {
                    newName = $"{nameWithoutExtension}_{suffix}{extension}";
                }
            }
            else if (pattern == @"^.*(\..+)$")
            {
                // Replace entire name but keep extension
                newName = $"{replacement.Replace("{n}", counter.ToString())}{extension}";
            }
            else if (pattern == ".+")
            {
                // Replace entire name including extension
                newName = replacement.Replace("{n}", counter.ToString());
            }
            else
            {
                // Custom pattern - apply regex only once
                var regex = new Regex(pattern);
                string replacementWithCounter = replacement.Replace("{n}", counter.ToString());
                newName = regex.Replace(file.Name, replacementWithCounter, 1);
            }

            // Ensure the new name is valid
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                newName = newName.Replace(invalidChar, '_');
            }

            counter += (int)RegexIncrementNumeric.Value;
            return newName;
        }

        // method to preview the renaming
        private void previewBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegexPatternTxtBox.Text))
            {
                ShowNotification("Please enter a regex pattern.", "error.png");
                return;
            }

            if (RegexFilesListView.Items.Count == 0)
            {
                ShowNotification("Please select the files to rename.", "error.png");
                return;
            }

            try
            {
                RegexFilesListView.Items.Clear();
                bool hasChanges = false;

                // Initialize counter
                int counter = 1;
                if (RegexUseIncrementCheckBox.Checked && int.TryParse(RegexStartFromNumeric.Text, out int startNumber))
                {
                    counter = startNumber;
                }

                foreach (var file in selectedFiles2)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    string newName = RegexUseIncrementCheckBox.Checked
                        ? GetNewFileName(fileInfo, RegexPatternTxtBox.Text, RegexReplacementTxtBox.Text, ref counter)
                        : new Regex(RegexPatternTxtBox.Text).Replace(fileInfo.Name, RegexReplacementTxtBox.Text);

                    var item = new ListViewItem(new[] { fileInfo.Name, newName });
                    RegexFilesListView.Items.Add(item);

                    if (fileInfo.Name != newName)
                        hasChanges = true;
                }

                ShowNotification("New filenames have been previewed.", "info.png");
                RegexRenameFilesBtn.Enabled = hasChanges;

            }
            catch (ArgumentException ex)
            {
                ShowNotification($"Invalid regex pattern: {ex.Message}", "error.png");
            }
        }

        // method to rename the files
        private void renameButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegexPatternTxtBox.Text))
            {
                ShowNotification("Please enter a regex pattern", "error.png");
                return;
            }

            if (RegexFilesListView.Items.Count == 0)
            {
                ShowNotification("Please select the files to rename.", "error.png");
                return;
            }
            int successCount = 0;
            List<string> errors = new List<string>();

            // Initialize counter
            int counter = 1;
            if (RegexUseIncrementCheckBox.Checked && int.TryParse(RegexStartFromNumeric.Text, out int startNumber))
            {
                counter = startNumber;
            }

            foreach (var file in selectedFiles2.ToList())
            {
                FileInfo fileInfo = new FileInfo(file);
                try
                {

                    string newName = RegexUseIncrementCheckBox.Checked
                        ? GetNewFileName(fileInfo, RegexPatternTxtBox.Text, RegexReplacementTxtBox.Text, ref counter)
                        : new Regex(RegexPatternTxtBox.Text).Replace(fileInfo.Name, RegexReplacementTxtBox.Text);

                    if (fileInfo.Name != newName)
                    {
                        string newPath = Path.Combine(fileInfo.DirectoryName, newName);

                        // Check if target file already exists
                        if (File.Exists(newPath))
                        {
                            errors.Add($"{fileInfo.Name}: Cannot rename - target file already exists: {newName}");
                            continue;
                        }

                        fileInfo.MoveTo(newPath);
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"{fileInfo.Name}: {ex.Message}");
                }
            }

            // Refresh the file list
            if (successCount > 0)
            {
                selectedFiles2.Clear();
                RegexFilesListView.Items.Clear();
                OpenFileDialog ofd = new OpenFileDialog();
                foreach (string filePath in ofd.FileNames)
                {
                    var fileInfo = new FileInfo(filePath);
                    string fileName = fileInfo.Name;
                    selectedFiles2.Add(fileName);
                    var item = new ListViewItem(new[] { fileInfo.Name, fileInfo.Name });
                    RegexFilesListView.Items.Add(item);
                }
            }

            // Show results
            string message = $"Successfully renamed {successCount} files.";
            if (errors.Any())
            {
                message += $"\n\nErrors occurred with {errors.Count} files:\n" + string.Join("\n", errors);
            }
            ShowNotification($"Renaming results: {errors.Any()}", "error.png");
            RegexRenameFilesBtn.Enabled = false;

        }

        // clear inputs
        private void RegexClearBtn_Click(object sender, EventArgs e)
        {
            RegexPatternTxtBox.Clear();
            RegexReplacementTxtBox.Clear();
            RegexFilesListView.Items.Clear();
            RegexUseIncrementCheckBox.Checked = false;
            RegexStartFromNumeric.Value = 1;
            RegexIncrementNumeric.Value = 1;
        }

        // method for regex help button
        private void RegexHelpBtn_Click(object sender, EventArgs e)
        {
            string url = "https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference";
            Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            }
            );
        }

        #endregion

        // method to update security questions answers
        private void SubmitNewSecurityQuestionsBtn_Click(object sender, EventArgs e)
        {
            if (ChangeSecurityQuestion1TtxBx.Text.Length == 0 || ChangeSecurityQuestion2TtxBx.Text.Length == 0 || ChangeSecurityQuestion3TtxBx.Text.Length == 0)
            {
                ShowNotification("All security questions should be filled.", "error.png");
                return;
            }

            if (QuestionsPasswordTxtBox.Text != SecureStorage.GetPassword())
            {
                ShowNotification("Wrong password.", "error.png");
                return;
            }

            string[] answers = { ChangeSecurityQuestion1TtxBx.Text, ChangeSecurityQuestion2TtxBx.Text, ChangeSecurityQuestion3TtxBx.Text };
            if (QuestionsPasswordTxtBox.Text == SecureStorage.GetPassword())
            {
                SecureStorage.SaveSecurityQuestions(answers);
                ShowNotification("Your security answers are successfully saved.", "success.png");
                return;
            }

        }

        // method to offer the user the ability to save lock password file as backup
        private void BackupLockPasswordbtn_Click(object sender, EventArgs e)
        {
            if (backupPasswordTxtBox.Text == SecureStorage.GetPassword())
            {
                SecureStorage.BackupPassword(backupPasswordTxtBox.Text);
                return;
            }

            else
            {
                ShowNotification("Wrong password.", "error.png");
                return;
            }

        }

        // method to show inputs
        private void hideShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "EyeWhite.png"));
            oldPasswordTxtBox.PasswordChar = '\0';
            NewPasswordTxtBox.PasswordChar = '\0';
            RepeatNewPasswordTxtBox.PasswordChar = '\0';
            backupPasswordTxtBox.PasswordChar = '\0';
            ChangeSecurityQuestion1TtxBx.PasswordChar = '\0';
            ChangeSecurityQuestion2TtxBx.PasswordChar = '\0';
            ChangeSecurityQuestion3TtxBx.PasswordChar = '\0';
            QuestionsPasswordTxtBox.PasswordChar = '\0';

        }

        // method to hide inputs
        private void hideShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            focusBtn.Focus();
            hideShowPassword.BackgroundImage = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", "NotEyeWhite.png"));
            oldPasswordTxtBox.PasswordChar = '*';
            NewPasswordTxtBox.PasswordChar = '*';
            RepeatNewPasswordTxtBox.PasswordChar = '*';
            backupPasswordTxtBox.PasswordChar = '*';
            ChangeSecurityQuestion1TtxBx.PasswordChar = '*';
            ChangeSecurityQuestion2TtxBx.PasswordChar = '*';
            ChangeSecurityQuestion3TtxBx.PasswordChar = '*';
            QuestionsPasswordTxtBox.PasswordChar = '*';
        }

        
    }
}


