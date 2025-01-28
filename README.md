# Cipher Shield

Cipher Shield is coded from the scratch and heavily tested for a flawless usability and functionality.

Cipher Shield is a robust and secure solution designed to handle all your password and file management needs. It includes a password generator, password manager, file encryption using **AES-256**, and regex-based file renaming. 
With Cipher Shield, you can ensure that your sensitive data remains protected at all times.

**AES-256** is a symmetric key encryption, which means the same key is used for both encrypting and decrypting data. 
This type of encryption is known for its speed and security, making it a preferred choice for many organizations, including the U.S. government.

![register.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/register.png)                     ![login.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/login.png)


## Features

- **Password Generator:** Generates complex and secure passwords to enhance security.

  
  ![password-generator.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/password-generator.png)

- **Password Manager:** Securely saves passwords to the database using **SQLCipher**.

  
  ![password-manager.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/password-manager.png)
  
- **File Encryption:** Files are encrypted using the **AES-256** encryption algorithm, and the password is saved and encrypted with the same algorithm.

  
  ![enc.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/enc.png)

- **Regex File Rename:** Renames files using C# regex symbols for flexible and powerful file naming.

  
  ![regex-files-rename.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/regex-files-rename.png)

- **Help:** Change your password or your security questions answers anytime.

  
![help.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/help.png)        

- **User Manual:** Get clear instructions.

![UM.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/UM.png)          

- **About:**

![about.png](https://github.com/hamdi-bouasker/Cipher-Shield/blob/master/about.png)                          
  

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

A folder named **Cipher Shield** wil be created in AppData directory which will hold **the database**, the answers of your **security questions** and **the master password**. Those files are all encrypted.
When you uninstall the app, this folder will be deleted!

 **Clone the repository:**

   ```sh
   git clone https://github.com/hamdi-bouasker/cipher-shield.git
   cd cipher-shield
   ```
## Usage

### Password Generator
Use the password generator feature to create complex and secure passwords.

### Password Manager
Securely save and manage your passwords using SQLCipher for encryption.

### File Encryption
Encrypt files using the AES-256 encryption algorithm, ensuring that both the files and their associated passwords are protected.

### Regex File Rename
Utilize the regex-based file renaming feature to organize and rename files flexibly. If you want to use the counter please add this symbol **{n}**

## License

This project is licensed under the MIT License.


