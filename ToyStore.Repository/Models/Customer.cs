using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ToyStory.Repository.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; } = new Guid();
        public string CustomerName { get; set; }
        public string CustomerPass { get; set; }
        public string CustomerUsername { get; set; }

        private int MIN_PASS_LENGTH = 8;

        public bool SetPass(string password)
        {
            if (password.Length < MIN_PASS_LENGTH)
            {
                Console.WriteLine("Password length should at least be " + MIN_PASS_LENGTH);
                return false;
            }

            var pHasher = new PasswordHasher();
            CustomerPass = pHasher.Hash(password);
            return true;
        }

        public static bool ComparePasswords(string rawPass, string entered)
        {
            return new PasswordHasher().ComparePass(rawPass, entered);
        }
    }
}
class PasswordHasher
{
    public string Hash(string pass)
    {
        // STEP 1 Create the salt value with a cryptographic PRNG:
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        // STEP 2 Create the Rfc2898DeriveBytes and get the hash value:
        var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);

        // STEP 3 Combine the salt and password bytes for later use:
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        // STEP 4 Turn the combined salt+hash into a string for storage
        return Convert.ToBase64String(hashBytes);
    }

    public bool ComparePass(string rawSaved, string entered)
    {
        /* Extract the bytes */
        // Console.WriteLine("saved: " + rawSaved);
        byte[] hashBytes = Convert.FromBase64String(rawSaved);
        // lock (hashBytes) ;
        /* Get the salt */
        byte[] salt = new byte[16];
        lock (salt)
        {
            lock (hashBytes)
            {
                lock (this)
                {
                    Array.Copy(hashBytes, 0, salt, 0, 16);
                }
            }
        }
        /* Compute the hash on the password the user entered */
        var pbkdf2 = new Rfc2898DeriveBytes(entered, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
        /* Compare the results */
        for (int i = 0; i < 20; i++)
            if (hashBytes[i + 16] != hash[i])
                return false;

        return true;
    }
}
