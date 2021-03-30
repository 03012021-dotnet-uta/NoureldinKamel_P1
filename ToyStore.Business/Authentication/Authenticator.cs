using System;
using System.Collections.Generic;
using ToyStore.Models.DBModels;
using ToyStore.Repository.Models;

namespace ToyStore.Business.Authentication
{
    public class Authenticator
    {
        private Dictionary<Token, Customer> _currentCustomers = new Dictionary<Token, Customer>();
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

        public bool Authenticate(AuthModel authModel, out Customer customerOut)
        {
            Customer customer = null;
            customerOut = null;
            if (_toyRepository.Login(authModel, out customer))
            {
                var token = new Token() { TokenValue = Guid.NewGuid() };
                _assignToken(customer, token);
                customerOut = new Customer();
                getCleanCustomer(customerOut, customer);
                return true;
            }
            return false;
        }

        public bool ValidateToken(Token token, out Customer customer)
        {
            customer = null;
            if (token.CheckExpiration())
            {
                customer = new Customer();
                var c = _currentCustomers[token];
                getCleanCustomer(customer, c);
                return true;
            }
            // if (_currentCustomers[token].TokenValue == token.TokenValue)
            // {
            //     if (token.CheckExpiration())
            //     {
            //         return true;
            //     }
            // }
            return false;
        }

        /// <summary>
        /// put all data in c into customer. <br/>
        /// except the password
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="c"></param>
        private static void getCleanCustomer(Customer customer, Customer c)
        {
            customer.FinishedOrders = c.FinishedOrders;
            customer.CurrentOrder = c.CurrentOrder;
            customer.CustomerToken = c.CustomerToken;
            customer.FirstName = c.FirstName;
            customer.LastName = c.LastName;
            customer.CustomerUName = c.CustomerUName;
        }

        public bool CreateNewCustomer(AuthModel authModel, out Customer customerOut)
        {
            Customer customer = null;
            customerOut = null;
            if (_toyRepository.Register(authModel, out customer))
            {
                customerOut = new Customer();
                var token = new Token() { TokenValue = Guid.NewGuid() };
                _assignToken(customer, token);
                getCleanCustomer(customerOut, customer);
                return true;
            }
            return false;
        }

        private void _assignToken(Customer customer, Token token)
        {
            customer.CustomerToken = token;
            _currentCustomers.Add(token, customer);
        }
    }
}