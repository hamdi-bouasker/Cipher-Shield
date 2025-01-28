# Cipher Shield

I coded it from the scratch with C# and it's heavily tested for a flawless usability and functionality.

Cipher Shield is a robust and secure solution designed to handle all your password and file management needs. It includes a password generator, password manager, file encryption using **AES-256**, and regex-based file renaming. 
With Cipher Shield, you can ensure that your sensitive data remains protected at all times.

**AES-256** is a symmetric key encryption, which means the same key is used for both encrypting and decrypting data. 
This type of encryption is known for its speed and security, making it a preferred choice for many organizations, including the U.S. government.

When you launch the app for the first time, you will be asked to register a password and security questions which are used to recover the Lock Password and the password used during files encryption.

When login, you have the possibility to load the lock password by loading the Lock-Password.dat file which you already saved in the Help tab.

It's also possible to recover your password and load it directly in the login tab by correctly answering the security questions you already registered during the app's first launch.

![register.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/register.png)                     ![login.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/login.png)


## Features

- **Password Generator:** Generates complex and secure passwords to enhance security.

  
  ![password-generator.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/password-generator.png)

- **Password Manager:** Securely saves passwords to the database using **SQLCipher**.
  Securely saves passwords to the database using SQLCipher to ensure the DB remain encrypted and secured.
  An Entry is composed of: Website, Username or Email address and Passsword.
  You can Add, Update and Delete an entry. You can Export as csv file or print them.
  
  ![password-manager.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/password-manager.png)
  
- **File Encryption:** Files are encrypted using the AES-256 encryption algorithm, and the password is saved and encrypted with the same algorithm to easily decrypt your files using Load Password button.
  The password could be saved to Password.pwd file which is securely encrypted.
  You can recover the password in case you forgot it or lost password.pwd file.

  
  ![enc.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/enc.png)

- **Regex File Rename:** Renames files using C# regex symbols for flexible and powerful file naming.

  
  ![regex-files-rename.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/regex-files-rename.png)

- **Help:** Change your password or the answers of your security questions anytime.
  You can also save the Lock Password so next time you can login easily by clicking on Load Password.
  Lock-Password.dat file is securely encrypted.

  
![help.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/help.png)        

- **User Manual:** Get clear instructions.

![UM.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/UM.png)          

- **About:**

![about.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/about.png)                          
  
- **Example of an encrypted text**

![encrypted-text.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/encrypted-text.png)
  
## Advantages of Encrypting Sensitive Data

Encrypting sensitive data is crucial for maintaining privacy and security in today's digital world. Here are some key advantages:

- **Data Protection:** Encryption ensures that your data remains confidential and protected from unauthorized access. Even if data is intercepted, it remains unreadable without the encryption key.
- **Compliance:** Encryption helps meet regulatory requirements and standards for data protection, such as GDPR, HIPAA, and PCI-DSS, ensuring your organization stays compliant.
- **Integrity:** Encryption protects data integrity by preventing tampering or alteration. Any unauthorized changes to the data can be detected, ensuring the authenticity of the information.
- **Trust:** Encrypting sensitive data fosters trust with your users, clients, and partners, demonstrating your commitment to safeguarding their information.
- **Resilience:** Encrypted data is less vulnerable to cyber-attacks, such as hacking, phishing, and data breaches, providing an additional layer of security.

## Getting Started

To get started with Cipher Shield, follow these steps:

**Using the installer:**

Install Cipher Shield using the provided installer. Your browser will warn you that the file will damage your computer but the reason behind this warning is that the installer is not signed with third-party authority such as [sectigo](https://sectigo.com)
or [digicert](https://digicert.com).

A folder named **Cipher Shield** wil be created in AppData directory which will hold **the database**, the answers of your **security questions**, the password used to encrypt files and **the master password**. Those files are all encrypted.
When you uninstall the app, this folder will be deleted!

 **Clone the repository:**

   ```sh
   git clone https://github.com/hamdi-bouasker/cipher-shield.git
   cd cipher-shield
   ```
**Compile the code:**

- Open the project in Visual Studio
- In *SecureStorage.cs*, insert your own hex keys.The hex key should be 32 bytes. Or for testing, use the ones provided.
- Click on *Build* -> *Build Solution*


## License

This project is licensed under the MIT License.


