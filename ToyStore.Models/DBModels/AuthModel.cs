using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace ToyStore.Models.DBModels
{
    /// <summary>
    /// This model is just for receiving user data from the all authentication methods or routes
    /// </summary>
    public class AuthModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

}