using System;
using System.Collections.Generic;
using System.Linq;
using ToyStore.Models.ControllerModels;
using ToyStore.Models.DBModels;
using ToyStore.Repository.Models;

namespace ToyStore.Business.Authentication
{
    public class Authenticator
    {
        private static Dictionary<Token, Customer> _currentTokens = new Dictionary<Token, Customer>();
        // private Authenticator()
        // {
        //     _toyRepository = new ToyRepository();
        // }
        // public Authenticator(ToyRepository _toyRepository)
        // {
        //     this._toyRepository = _toyRepository;
        // }

        public Authenticator()
        {
            this._toyRepository = new ToyRepository();
        }

        // public static Authenticator Instance
        // {
        //     get
        //     {
        //         if (_instance == null)
        //         {
        //             _instance = new Authenticator();
        //         }
        //         return _instance;
        //     }
        // }

        // private static Authenticator _instance;

        private ToyRepository _toyRepository;

        public bool Authenticate(AuthModel authModel, out Customer customerOut)
        {
            Customer customer = null;
            customerOut = null;
            if (_toyRepository.Login(authModel, out customer))
            {
                var token = new Token() { TokenValue = Guid.NewGuid() };
                _assignToken(customer, token);
                System.Console.WriteLine("got customer: " + customer.FirstName + "\norders: " + customer.FinishedOrders);
                customerOut = new Customer();
                getCleanCustomer(customerOut, customer);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Validate the token from the browser
        /// to ensure user is authenticated before.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        /// <returns>true if authenticated</returns>
        public bool ValidateToken(Token token, out Customer customer)
        {
            customer = null;
            try
            {
                var savedToken = _currentTokens.Keys.ToList().Where(t =>
                {
                    System.Console.WriteLine("\nt: " + t.TokenValue + "token: " + token.TokenValue);
                    return t.TokenValue == token.TokenValue;
                }).First();
                System.Console.WriteLine("saved token: " + savedToken.TokenValue);
                if (savedToken.CheckExpiration())
                {
                    var c = _currentTokens[savedToken];
                    c = _toyRepository.GetCustomerWithId(c.CustomerId);
                    // c.CurrentOrder.cart.ToList().ForEach(s =>
                    // {
                    //     s.location.LocationInventory.ToList().ForEach(s =>
                    //     {
                    //         System.Console.WriteLine("testing location");
                    //         System.Console.WriteLine(s.Item);
                    //     });
                    // });
                    c.CustomerToken = savedToken;
                    customer = new Customer();
                    getCleanCustomer(customer, c);
                    return true;
                }
                return false;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("token is false " + e.Message + "\n" + e.StackTrace);
                return false;
            }
        }

        public bool Logout(Token token)
        {
            var savedToken = _currentTokens.Keys.ToList().Where(t =>
            {
                System.Console.WriteLine("\nt: " + t.TokenValue + "token: " + token.TokenValue);
                return t.TokenValue == token.TokenValue;
            }).First();

            _currentTokens.Remove(savedToken);
            if (!_currentTokens.Keys.Contains(savedToken))
            {
                return true;
            }
            return false;
        }

        public void GetUserPageData(Customer customerOut)
        {
            // _toyRepository.Get
        }


        /// <summary>
        /// put all data in c into customer. <br/>
        /// except the password
        /// </summary>
        /// <param name="emptyCustomer"></param>
        /// <param name="savedCustomer"></param>
        private static void getCleanCustomer(Customer emptyCustomer, Customer savedCustomer)
        {
            emptyCustomer.FinishedOrders = savedCustomer.FinishedOrders;
            if (savedCustomer.CurrentOrder == null)
                savedCustomer.CurrentOrder = new Order() { cart = new HashSet<SellableStack>() };
            emptyCustomer.CurrentOrder = savedCustomer.CurrentOrder;
            System.Console.WriteLine(emptyCustomer.CurrentOrder.cart);
            emptyCustomer.CustomerToken = savedCustomer.CustomerToken;
            emptyCustomer.FirstName = savedCustomer.FirstName;
            emptyCustomer.LastName = savedCustomer.LastName;
            emptyCustomer.CustomerUName = savedCustomer.CustomerUName;
        }


        /// <summary>
        /// create a new customer on register
        /// </summary>
        /// <param name="authModel"></param>
        /// <param name="customerOut"></param>
        /// <returns></returns>
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
            _currentTokens.Add(token, customer);
        }


        /// <summary>
        /// This validate method output doesn't go to the user. <br/>
        /// it is only used within the server.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool InnerValidate(Token token, out Customer customer)
        {
            customer = null;
            var savedToken = _currentTokens.Keys.ToList().Where(t =>
            {
                System.Console.WriteLine("\nt: " + t.TokenValue + "token: " + token.TokenValue);
                return t.TokenValue == token.TokenValue;
            }).First();
            if (savedToken.CheckExpiration())
            {
                var c = _currentTokens[savedToken];
                customer = c;
                return true;
            }
            return false;
        }
    }
}