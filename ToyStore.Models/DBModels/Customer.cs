using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// A user which buys from a location
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The Id that identifies a customer
        /// </summary>
        /// <returns></returns>
        [Key]
        public Guid CustomerId { get; set; } = new Guid();

        /// <summary>
        /// First name of the customer
        /// </summary>
        /// <value></value>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the customer
        /// </summary>
        /// <value></value>
        public string LastName { get; set; }

        /// <summary>
        /// username that customer will use to login
        /// it should be unique
        /// </summary>
        /// <value></value>
        public string CustomerUName { get; set; }

        /// <summary>
        /// password for logging in to customer account
        /// </summary>
        /// <value></value>
        public string CustomerPass { get; set; }

        /// <summary>
        /// Orders checked out or confirmed
        /// by the customer
        /// </summary>
        /// <value></value>
        public List<Order> FinishedOrders { get; set; }

        /// <summary>
        /// The current order of a customer.
        /// This represents the cart and the items in the customer's cart
        /// </summary>
        /// <value></value>
        public Order CurrentOrder { get; set; }

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

        /// <summary>
        /// compare the current customer password
        /// to the entered password in pure visible string
        /// </summary>
        /// <param name="enteredPass"></param>
        /// <returns>true if matching</returns>
        public bool ComparePasswords(string enteredPass)
        {
            return new PasswordHasher().ComparePass(this.CustomerPass, enteredPass);
        }

        /// <summary>
        /// compare a hashed password to another password in pure
        /// visible string
        /// </summary>
        /// <param name="rawPass"></param>
        /// <param name="entered"></param>
        /// <returns>true if matching</returns>
        public static bool ComparePasswords(string rawPass, string entered)
        {
            return new PasswordHasher().ComparePass(rawPass, entered);
        }
    }

    class PasswordHasher
    {
        /// <summary>
        /// Hash the entered password that is in visible string
        /// to a hashed version ready to store in db
        /// </summary>
        /// <param name="pass"></param>
        /// <returns>hashed password</returns>
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

        /// <summary>
        /// compare a saved hashed password with another
        /// just entered in pure visible string
        /// method hashes the entered pass and 
        /// compares it with the saved hashed passwords
        /// </summary>
        /// <param name="savedHash"></param>
        /// <param name="entered"></param>
        /// <returns>true if matching</returns>
        public bool ComparePass(string savedHash, string entered)
        {
            /* Extract the bytes */
            // Console.WriteLine("saved: " + rawSaved);
            byte[] hashBytes = Convert.FromBase64String(savedHash);
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

}