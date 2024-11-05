using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Policy;

namespace dbtest {
    class HashedPassword {
        public string Password { get; set; }
        public string hashSaltDisplay {
           get {
                return stringHashSalt();
            }
        }

        public string hashPasswordDisplay {
            get {
                return stringHashPassword();
            }
        }

        public string hashCombinedDisplay {
            get {
                return hashedPassword();
            }
        }

        public HashedPassword(string password) {
            this.Password = password;
            /*Console.WriteLine($"Password: {Password}");
            Console.WriteLine($"Hashed Salt: {hashSaltDisplay}");
            Console.WriteLine($"Hashed Password: {hashPasswordDisplay}");
            Console.WriteLine($"Combined Password: {hashCombinedDisplay}");*/
        }

        //generates a salt based on the user input. it takes half ot the password and turn it into bytes
        public byte[] generateSalt() {
            int halfLength = Password.Length / 2;
            string setSalt = Password.Substring(halfLength, halfLength);

            byte[] newSalt = Encoding.UTF8.GetBytes(setSalt);

            return newSalt;
        }

        //turn the user input into byte and make it into hash
        public byte[] hashPassword() {
            using (var hash = SHA256.Create()) {
                byte[] newPassword = Encoding.UTF8.GetBytes(Password);
                return hash.ComputeHash(newPassword);
            }
        }

        //turn the salt into hashed byte
        public byte[] hashSalt() {
            byte[] salt = generateSalt();
            using (var hash = SHA256.Create()) {
                return hash.ComputeHash(salt);
            }
        }

        //this gets the whole combined salt and password hashed and make it into a string
        public string hashedPassword() {
            byte[] _hashSalt = hashSalt();
            byte[] _hashPassword = hashPassword();
            byte[] saltedPassword = new byte[_hashSalt.Length + _hashPassword.Length];

            Buffer.BlockCopy(_hashPassword, 0, saltedPassword, 0, _hashPassword.Length);
            Buffer.BlockCopy(_hashSalt, 0, saltedPassword, _hashPassword.Length, _hashSalt.Length);

            return BitConverter.ToString(saltedPassword).Replace("-", "").ToLower();
        }

        //turns the password into a string from byte
        public string stringHashPassword() {
            byte[] _hashPassword = hashPassword();
            return BitConverter.ToString(_hashPassword).Replace("-", "").ToLower();
        }

        //turns the salt into a string from byte
        public string stringHashSalt() {
            byte[] _hashSalt = hashSalt();
            return BitConverter.ToString(_hashSalt).Replace("-", "").ToLower();
        }
    }
}
