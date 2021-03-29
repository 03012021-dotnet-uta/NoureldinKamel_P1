using System;
using System.Collections.Generic;
using ToyStore.Models.DBModels;
using ToyStore.Repository.Models;

namespace ToyStore.Business.Authentication
{
    public class Authenticator
    {
        private Dictionary<Customer, Token> _currentCustomers = new Dictionary<Customer, Token>();
        private Authenticator()
        {
            _toyRepository = new ToyRepository();
        }

        public static Authenticator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Authenticator();
                }
                return _instance;
            }
        }

        private static Authenticator _instance;

        private ToyRepository _toyRepository;

        public bool Authenticate(string username, string password, out Customer customer)
        {
            // customer = null;
            if (_toyRepository.Login(username, password, out customer))
            {
                var token = new Token() { TokenValue = Guid.NewGuid(), TokenCustomer = customer };
                _assignToken(customer, token);
                return true;
            }
            return false;
        }

        public bool ValidateToken(Token token, Customer customer)
        {
            if (_currentCustomers[customer].TokenValue == token.TokenValue)
            {
                if (token.CheckExpiration())
                {
                    return true;
                }
            }
            return false;
        }

        public bool CreateNewCustomer(Customer customerIn, out Customer customerOut)
        {
            // customer = null;
            if (_toyRepository.Register(customerIn, out customerOut))
            {
                var token = new Token() { TokenValue = Guid.NewGuid(), TokenCustomer = customerOut };
                _assignToken(customerOut, token);
                return true;
            }
            return false;
        }

        private void _assignToken(Customer customer, Token token)
        {
            customer.CustomerToken = token;
            _currentCustomers.Add(customer, token);
        }
    }
}