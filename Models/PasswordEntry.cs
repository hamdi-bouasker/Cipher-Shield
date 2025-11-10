using System;

namespace CipherShield.Models
{
    internal class PasswordEntry
    {
        public int Id { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
