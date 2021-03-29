using System;

namespace ToyStore.Models.DBModels
{
    public class Token
    {
        public Token()
        {
            SetExpiration();
        }

        /// <summary>
        /// The value that would be used to authenticate the user
        /// </summary>
        /// <value></value>
        public Guid TokenValue { get; set; }

        /// <summary>
        /// The customer that owns the token
        /// </summary>
        /// <value></value>
        public Customer TokenCustomer { get; set; }

        /// <summary>
        /// The time at which the token becomes invalid
        /// </summary>
        /// <value></value>
        public DateTime TokenExpiration { get; set; }

        public void SetExpiration()
        {
            TokenExpiration = DateTime.Now.AddHours(1);
        }

        /// <summary>
        /// Returns true if valid token
        /// </summary>
        /// <returns></returns>
        public bool CheckExpiration()
        {
            return TokenExpiration > DateTime.Now;
        }
    }
}