using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToyStore.Models.DBModels;
using ToyStore.Models.Abstracts;
using ToyStore.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace ToyStore.Repository.Models
{
    public class ToyRepository
    {
        public ToyRepository()
        {

        }
        /* #region Authentications */

        public bool Login(string username, string password, out Customer customer)
        {
            customer = null;
            try
            {
                using (var db = new DbContextClass())
                {
                    customer = db.Customers.Where(c => c.CustomerUName == username).First();
                }
                if (customer.ComparePasswords(password))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                PrintError(e);
                customer = null;
                return false;
            }
            customer = null;
            return false;
        }

        public bool Register(Customer customerIn, out Customer customerOut)
        {
            customerOut = null;
            try
            {
                using (var db = new DbContextClass())
                {
                    db.Customers.Where(c => c.CustomerUName == customerIn.CustomerUName).First();
                }
                return false;
            }
            catch (System.Exception)
            {
                using (var db = new DbContextClass())
                {
                    try
                    {
                        db.Customers.Add(customerIn);
                        customerOut = db.Customers.Where(c => c.CustomerId == customerIn.CustomerId).First();
                        return db.SaveChanges() > 0;
                    }
                    catch (System.Exception e2)
                    {
                        Console.WriteLine("error: " + e2.Message + "\n" + e2.StackTrace);
                        return false;
                    }
                }
            }
        }

        /* #endregion */

        public bool AddTag(string name)
        {
            try
            {
                using (var db = new DbContextClass())
                {
                    db.Tags.Where(t => t.TagName == name).First();
                }
                return false;
            }
            catch (System.Exception)
            {
                try
                {
                    using (var db = new DbContextClass())
                    {
                        db.Tags.Add(new Tag() { TagName = name });
                        return db.SaveChanges() > 0;
                    }
                }
                catch (System.Exception e)
                {
                    PrintError(e);
                    return false;
                }
            }
        }



        /* #region Order Management */

        /// <summary>
        /// Add a new order to the DB
        /// </summary>
        /// <param name="order"></param>
        /// <returns>true if success</returns>
        public bool SaveNewOrder(Order order)
        {
            using (var db = new DbContextClass())
            {
                db.Orders.Add(order);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Save all current customer changes
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>true if success</returns>
        public bool SaveCustomerChanges(Customer customer)
        {
            using (var db = new DbContextClass())
            {
                if (!db.Orders.Contains(customer.CurrentOrder))
                {
                    SaveNewOrder(customer.CurrentOrder);
                }
                else
                {
                    var cart = customer.CurrentOrder.cart;
                    foreach (var pair in cart)
                    {
                        // if (!db.Sellables.Contains(pair.Key))
                        // {
                        //     // db.Add(pair.)
                        // }
                    }
                    // customer.CurrentOrder.cart.ForEach(pizza =>
                    // {
                    // if (!db.Pizzas.Contains(pizza))
                    // {
                    //     db.Pizzas.Add(pizza);
                    // }
                    // else
                    // {
                    //     pizza.ToppingList.ForEach(topping =>
                    //     {
                    //         if (!db.Toppings.Contains(topping))
                    //         {
                    //             db.Toppings.Add(topping);
                    //         }
                    //     });
                    //     if (!db.Crusts.Contains(pizza.PizzaCrust))
                    //     {
                    //         db.Crusts.Add(pizza.PizzaCrust);
                    //     }
                    //     if (!db.Sizes.Contains(pizza.PizzaSize))
                    //     {
                    //         db.Sizes.Add(pizza.PizzaSize);
                    //     }
                    // }
                    // });
                    db.SaveChanges();
                }
                try
                {
                    db.Update(customer.CurrentOrder);
                    db.Update(customer);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    Console.WriteLine("an error occurred while saving your changes: " + e.Message + "\n" + e.StackTrace);
                }
                catch (System.InvalidOperationException e)
                {
                    Console.WriteLine("an error occurred while saving your changes: " + e.Message + "\n" + e.StackTrace);
                }
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Removes a sellable item from an order.
        /// The change is done in the database.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="sellable"></param>
        /// <returns>true if success</returns>
        public bool RemoveSellableFromOrder(Order order, Sellable sellable)
        {
            using (var db = new DbContextClass())
            {
                if (!db.Orders.Contains(order))
                {
                    return true;
                }
                if (!db.Sellables.Contains(sellable))
                {
                    return true;
                }
                db.Sellables.Remove(sellable);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Check out a customer <br/>
        /// remove the customer's current order.
        /// Add the order to customer's finished orders.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="order"></param>
        /// <returns>true if successful</returns>
        public bool CheckoutCustomer(Customer customer, Order order)
        {
            using (var db = new DbContextClass())
            {
                customer.CurrentOrder = null;
                order.OrderDate = DateTime.Now;
                if (db.Orders.Contains(order))
                {
                    db.Remove(order);
                    db.Update(customer);
                    // db.Database.ExecuteSqlRaw("UPDATE dbo.Customers SET CurrentOrderOrderId = null WHERE CustomerId = '" + customer.CustomerId + "'");
                    db.SaveChanges();
                    // Console.WriteLine("change? :" + );
                }
                db.Add(order);
                customer.FinishedOrders.Add(order);
                db.Update(customer);
                return db.SaveChanges() > 0;
            }
        }

        /* #endregion */




        public List<SellableStack> GetAllSellableItems()
        {
            List<SellableStack> stackList = new List<SellableStack>();
            // using (var db = new DbContextClass())
            // {
            //     var locationList = db.Locations.Include(l => l.LocationInventory).ToList();
            //     locationList.ForEach(location =>
            //     {
            //         if (location.LocationInventory != null && location.LocationInventory.Count < 0)
            //         {
            //             stackList.AddRange(location.LocationInventory);
            //         }
            //     });
            // }
            // return stackList;
            return new List<SellableStack>()
            {
                new SellableStack()
                {
                    Item  = new Product()
                    {
                        SellableName = "Toy1",
                        SellablePrice = 20,
                        SellableImagePath = @"https://media.istockphoto.com/photos/brown-teddy-bear-isolated-in-front-of-a-white-background-picture-id909772478?k=6&m=909772478&s=612x612&w=0&h=X55jzpsKboa_jUjbEN8eqAn0gjt696ldbeJMEqmNrcU=",
                        TagList = new List<Tag>()
                        {
                            new Tag()
                            {
                                TagName = "Magnet"
                            }
                        }
                    },
                    Count = 2
                },
                new SellableStack()
                {
                    Item  = new Product()
                    {
                        SellableName = "Toy2",
                        SellablePrice = 18,
                        SellableImagePath = @"https://media.istockphoto.com/photos/brown-teddy-bear-isolated-in-front-of-a-white-background-picture-id909772478?k=6&m=909772478&s=612x612&w=0&h=X55jzpsKboa_jUjbEN8eqAn0gjt696ldbeJMEqmNrcU=",
                        TagList = new List<Tag>()
                        {
                            new Tag()
                            {
                                TagName = "Doll"
                            }
                        }
                    },
                    Count = 4
                },
                new SellableStack()
                {
                    Item  = new Offer()
                    {
                        SellableName = "toy1+toy2",
                        SellablePrice = 30,
                        SellableImagePath = @"https://media.istockphoto.com/photos/brown-teddy-bear-isolated-in-front-of-a-white-background-picture-id909772478?k=6&m=909772478&s=612x612&w=0&h=X55jzpsKboa_jUjbEN8eqAn0gjt696ldbeJMEqmNrcU=",
                        Products = new List<Product>()
                        {
                            new Product()
                            {
                                SellableName = "Toy1",
                                SellablePrice = 20,
                                SellableImagePath = @"https://media.istockphoto.com/photos/brown-teddy-bear-isolated-in-front-of-a-white-background-picture-id909772478?k=6&m=909772478&s=612x612&w=0&h=X55jzpsKboa_jUjbEN8eqAn0gjt696ldbeJMEqmNrcU=",
                                TagList = new List<Tag>()
                                {
                                    new Tag()
                                    {
                                        TagName = "Magnet"
                                    }
                                }
                            },
                            new Product()
                            {
                                SellableName = "Toy2",
                                SellablePrice = 18,
                                SellableImagePath = @"https://media.istockphoto.com/photos/brown-teddy-bear-isolated-in-front-of-a-white-background-picture-id909772478?k=6&m=909772478&s=612x612&w=0&h=X55jzpsKboa_jUjbEN8eqAn0gjt696ldbeJMEqmNrcU=",
                                TagList = new List<Tag>()
                                {
                                    new Tag()
                                    {
                                        TagName = "Doll"
                                    }
                                }
                            }
                        },
                        TagList = new List<Tag>()
                        {
                            new Tag()
                            {
                                TagName = "Doll"
                            },
                            new Tag()
                            {
                                TagName = "Magnet"
                            }
                        },
                    },
                    Count = 4,
                }
            };
        }

        public HashSet<Tag> GetAvailableTags()
        {
            var stackList = new List<SellableStack>();
            var tagSet = new HashSet<Tag>();
            using (var db = new DbContextClass())
            {
                stackList = GetAllSellableItems();
            }
            if (stackList != null)
            {
                stackList.ForEach(stack =>
                {
                    var ts = stack.GetTags();
                    if (ts != null)
                    {
                        ts.ForEach(tag =>
                        {
                            tagSet.Add(tag);
                        });
                    }
                });
            }
            return tagSet;
        }

        public List<SellableStack> GetSellablesByTag(Tag neededTag)
        {
            var tagSellables = new List<SellableStack>();
            var allSellableStacks = GetAllSellableItems();
            if (allSellableStacks != null && allSellableStacks.Count != 0)
            {
                tagSellables = allSellableStacks.Where(stack => stack.GetTags().Any(t => t.TagName == neededTag.TagName)).ToList();
            }
            else
            {
                Console.WriteLine("no items in db");
            }
            return tagSellables;
        }

        private static void PrintError(Exception e)
        {
            Console.WriteLine("error: " + e.Message + "\n" + e.StackTrace);
        }




        // Clear() removes the reference to the entity, not the entity itself.

        // If you intend this to be always the same operation, you could handle AssociationChanged:

        // Entity.Children.AssociationChanged += 
        //     new CollectionChangeEventHandler(EntityChildrenChanged);
        // Entity.Children.Clear(); 

        // private void EntityChildrenChanged(object sender, CollectionChangeEventArgs e)
        // {
        //     // Check for a related reference being removed. 
        //     if (e.Action == CollectionChangeAction.Remove)
        //     {
        //         using (var db = new DbContextClass())
        //         {

        //             db.DeleteObject(e.Element);
        //         }
        //     }
        // }

        /* #region Customer Methods */

        // public bool CreateCustomer(Customer customer)
        // {
        //     using (var db = new DbContextClass())
        //     {
        //         if (db.Customers.Contains(customer))
        //             return false;
        //         db.Customers.Add(customer);
        //     }
        //     return true;
        // }

        // public bool Login(string username, string password, out Customer customer)
        // {
        //     using (var db = new DbContextClass())
        //     {
        //         Customer c = null;
        //         try
        //         {
        //             c = db.Customers.Include(c => c.CurrentOrder)
        //             .Where(c => c.Username == username)
        //             .Include(c => c.FinishedOrders)
        //             .Include(c => c.CurrentOrder.Store)
        //             .Include(c => c.CurrentOrder.Pizzas)
        //             .First();

        //             if (c.CurrentOrder != null)
        //             {
        //                 c.CurrentOrder.Pizzas.ForEach(
        //                     pizza =>
        //                     {
        //                         db.Entry(pizza).Reference(p => p.PizzaCrust).Load();
        //                         db.Entry(pizza).Reference(p => p.PizzaSize).Load();
        //                         db.Entry(pizza).Collection(p => p.ToppingList).Load();
        //                     }
        //                 );
        //             }
        //             if (c.FinishedOrders.Count > 0)
        //             {
        //                 foreach (var order in c.FinishedOrders)
        //                 {
        //                     db.Entry(order).Reference(o => o.Store).Load();
        //                     db.Entry(order).Collection(o => o.Pizzas).Load();
        //                     order.Pizzas.ForEach(
        //                         pizza =>
        //                         {
        //                             db.Entry(pizza).Reference(p => p.PizzaCrust).Load();
        //                             db.Entry(pizza).Reference(p => p.PizzaSize).Load();
        //                             db.Entry(pizza).Collection(p => p.ToppingList).Load();
        //                         }
        //                     );
        //                 }
        //             }
        //             // .Include(c => c.CurrentOrder.Pizzas)
        //             // .Include(c => c.CurrentOrder.Pizzas.FirstOrDefault().PizzaSize)
        //             // .Include(c => c.CurrentOrder.Pizzas.FirstOrDefault().PizzaCrust)
        //             // .Include(c => c.CurrentOrder.Pizzas.FirstOrDefault().ToppingList)
        //         }
        //         // catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        //         // {
        //         //     Console.WriteLine("couldn't ");
        //         // }
        //         catch (System.Exception e)
        //         {
        //             Console.WriteLine("customer not found" + e.Message + "\n" + e.StackTrace);
        //             customer = null;
        //             return false;
        //         }
        //         if (c == null)
        //         {
        //             Console.WriteLine("customer not found");
        //             customer = null;
        //             return false;
        //         }
        //         else if (Customer.Compare(c.Password, password))
        //         {
        //             Console.WriteLine("login success");
        //             customer = c;
        //             // if (c.CurrentOrder != null)
        //             // {
        //             //     c.CurrentOrder.Pizzas.ForEach(pizza => pizza.PizzaSize = db.Sizes.Where(s => s.ComponentId == pizza.Property()));
        //             //     db.Sizes.Where(s => )
        //             // }
        //             return true;
        //         }
        //         else
        //         {
        //             Console.WriteLine("passwords not matching");
        //             customer = null;
        //             return false;
        //         }
        //     }
        // }

        // public bool CheckUserExists(string username)
        // {
        //     using (var db = new DbContextClass())
        //     {

        //         Customer c = null;
        //         try
        //         {
        //             c = db.Customers.Where(c => c.Username == username).First();
        //         }
        //         catch (System.Exception)
        //         {
        //             return false;
        //         }
        //         return true;
        //     }
        // }

        // /// <summary>
        // /// check if customer has an order saved in the db
        // /// if yes, delete that order
        // /// if no, just do nothing
        // /// </summary>
        // /// <param name="customer"></param>
        // /// <returns></returns>
        // internal bool DeleteOrderIfExists(Customer customer)
        // {
        //     using (var db = new DbContextClass())
        //     {
        //         Order o = null;
        //         try
        //         {
        //             o = db.Orders.Where(o => o.OrderId == db.Customers.Where(c => c.CustomerId == customer.CustomerId).First().CurrentOrder.OrderId).First();
        //         }
        //         catch (System.Exception)
        //         {
        //             customer.CurrentOrder = null;
        //             return true;
        //         }
        //         customer.CurrentOrder = null;
        //         // db.Database.ExecuteSqlRaw("UPDATE dbo.Customers SET CurrentOrderOrderId = null WHERE CustomerId = '" + customer.CustomerId + "'");
        //         db.Update(customer);
        //         // db.SaveChanges();
        //         db.Orders.Remove(o);
        //         return db.SaveChanges() > 0;
        //     }
        // }

        // public bool Register(Customer customer)
        // {
        //     bool saved = false;
        //     using (var db = new DbContextClass())
        //     {
        //         db.Customers.Add(customer);
        //         saved = db.SaveChanges() > 0;
        //     }
        //     return saved;
        // }

        // // public bool AddPizza(APizza pizza)
        // // {
        // //     using (var db = new DbContextClass())
        // //     {
        // //         try
        // //         {

        // //         }
        // //         catch (System.Exception)
        // //         {

        // //             throw;
        // //         }
        // //     }
        // // }


        // public bool RemoveToppingFromDBPizza(Order order, APizza pizza, Topping topping)
        // {
        //     using (var db = new DbContextClass())
        //     {
        //         if (!db.Orders.Contains(order))
        //         {
        //             return true;
        //         }
        //         if (!db.Pizzas.Contains(pizza))
        //         {
        //             return true;
        //         }
        //         // if (component.GetType() == Topping.GetType())
        //         // {
        //         // Console.WriteLine("remove a topping");
        //         if (!db.Toppings.Contains(topping))
        //         {
        //             return true;
        //         }
        //         // }
        //         db.Toppings.Remove(topping);
        //         return db.SaveChanges() > 0;
        //     }
        // }

        // public bool RemoveCrustFromDBPizza(Order order, APizza pizza, Crust crust)
        // {
        //     using (var db = new DbContextClass())
        //     {
        //         if (!db.Orders.Contains(order))
        //         {
        //             return true;
        //         }
        //         if (!db.Pizzas.Contains(pizza))
        //         {
        //             return true;
        //         }
        //         // if (component.GetType() == Topping.GetType())
        //         // {
        //         // Console.WriteLine("remove a topping");
        //         if (!db.Crusts.Contains(crust))
        //         {
        //             return true;
        //         }
        //         // }
        //         db.Crusts.Remove(crust);
        //         return db.SaveChanges() > 0;
        //     }
        // }

        // public bool RemoveSizeFromDBPizza(Order order, APizza pizza, Size size)
        // {
        //     using (var db = new DbContextClass())
        //     {
        //         if (!db.Orders.Contains(order))
        //         {
        //             return true;
        //         }
        //         if (!db.Pizzas.Contains(pizza))
        //         {
        //             return true;
        //         }
        //         // if (component.GetType() == size.GetType())
        //         // {
        //         // Console.WriteLine("remove a size");
        //         if (!db.Sizes.Contains(size))
        //         {
        //             return true;
        //         }
        //         // }
        //         db.Sizes.Remove(size);
        //         return db.SaveChanges() > 0;
        //     }
        // }


        // public bool DeleteCustomer(Customer customer)
        // {
        //     using (var db = new DbContextClass())
        //     {
        //         db.Customers.Remove(customer);
        //         return db.SaveChanges() > 0;
        //     }
        // }

        // /* #endregion */

        // /* #region Store Methods */

        // public Dictionary<Customer, List<Order>> GetAllFinishedOrders()
        // {
        //     // customerOrders = new Dictionary<Customer, List<Order>>();
        //     Dictionary<Customer, List<Order>> os = new Dictionary<Customer, List<Order>>();
        //     using (var db = new DbContextClass())
        //     {
        //         // db.Orders.EntityType.GetProperty("CustomerId").;
        //         db.Customers.Include(c => c.FinishedOrders).ToList().ForEach(c =>
        //           {
        //               //   Console.WriteLine("customer? " + c.Username);
        //               if (c.FinishedOrders.Count > 0)
        //               {
        //                   foreach (var order in c.FinishedOrders)
        //                   {
        //                       db.Entry(order).Reference(o => o.Store).Load();
        //                       db.Entry(order).Collection(o => o.Pizzas).Load();
        //                       order.Pizzas.ForEach(
        //                           pizza =>
        //                           {
        //                               db.Entry(pizza).Reference(p => p.PizzaCrust).Load();
        //                               db.Entry(pizza).Reference(p => p.PizzaSize).Load();
        //                               db.Entry(pizza).Collection(p => p.ToppingList).Load();
        //                           }
        //                       );
        //                       //       Console.WriteLine("order? " + order);
        //                   }
        //               }
        //               //   Console.WriteLine(c.FinishedOrders);
        //               os.Add(c, c.FinishedOrders);
        //           });
        //         return os;

        //         // orders = db.Database.ExecuteSqlRaw("SELECT * FROM dbo.Orders WHERE CustomerId NOT null");
        //         // db.Orders.Where(o=>o.Property());
        //     }
        // }

        // // public Dictionary<Customer, List<Order>> GetPizzaRevenue(Dictionary<Customer, List<Order>> customerOrders)
        // // {
        // //     if (customerOrders.Count <= 0)
        // //     {
        // //         customerOrders = GetAllFinishedOrders();
        // //     }
        // //     List<APizza> pizzas = new List<APizza>();
        // //     foreach (var pair in customerOrders)
        // //     {
        // //         pair.Value.ForEach(o =>
        // //         {
        // //             pizzas.AddRange(o.Pizzas);
        // //         });
        // //     }
        // //     using (var db = new DbContextClass())
        // //     {
        // //         foreach (var pair in customerOrders)
        // //         {

        // //         }
        // //     }
        // // }

        /* #endregion */


    }
}