using System;
using System.Collections.Generic;
using System.Linq;
using ToyStore.Business.Authentication;
using ToyStore.Models.ControllerModels;
using ToyStore.Models.DBModels;
using ToyStore.Repository.Models;

namespace ToyStore.Business.Logic
{
    public class OrderLogic
    {
        private readonly ToyRepository _toyRepository;
        private readonly Authenticator _authenticator;
        public OrderLogic(ToyRepository _toyRepository, Authenticator _authenticator)
        {
            this._toyRepository = _toyRepository;
            this._authenticator = _authenticator;
        }

        /// <summary>
        /// compare the saved customer with the received one.
        /// check the carts of both.
        /// update the saved customer to be exactly the same as the 
        /// received one.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateOrder(Customer customer, OrderModel orderModel)
        {
            bool success = false;
            if (orderModel.addedStacks != null)
                foreach (var stackId in orderModel.addedStacks)
                {
                    // System.Console.WriteLine("added stack: ");
                    // System.Console.WriteLine(stack.Item.SellableName);
                    // System.Console.WriteLine(stack.Count);
                    // System.Console.WriteLine(stack.Item.SellablePrice);
                    var stack = _toyRepository.GetSellableStackByIdDb(stackId);
                    if (_checkIfCanAddToOrder(stack, customer.CurrentOrder))
                        if (_checkIfStackIsInOrder(stack, customer.CurrentOrder))
                        {
                            if (!_toyRepository.AddSellableStackToCustomerOrder(customer, stack))
                                return false;
                            else
                                success = true;
                        }
                }

            // get the stored user
            // check every sellablestack in cart
            // if 
            System.Console.WriteLine("success: " + success);
            return success;
        }

        public bool GetInventory(Guid id, out List<SellableStack> stacks)
        {
            stacks = _toyRepository.GetLocationInventory(id);
            if (stacks != null)
                return true;
            return false;
        }

        /// <summary>
        /// delete the stack in the model from the order of the customer
        /// </summary>
        /// <param name="customerOut"></param>
        /// <param name="odModel"></param>
        public bool DeleteStackFromOrder(Customer customer, OrderDeleteModel odModel)
        {
            bool success = false;
            if (odModel.removedStacks != null)
                foreach (var stack in odModel.removedStacks)
                    if (!_toyRepository.RemoveSellableStackFromOrder(customer.CurrentOrder, stack))
                        return false;
                    else success = true;

            return success;
        }


        /// <summary>
        /// delete the stack in the model from the order of the customer
        /// </summary>
        /// <param name="customerOut"></param>
        /// <param name="odModel"></param>
        public bool UpdateStackCountfromOrder(Customer customer, OrderPatchModel odModel)
        {
            bool success = false;
            if (odModel.newUpdatedStackCount != null)
                foreach (var stackInt in odModel.newUpdatedStackCount)
                    if (!_toyRepository.ChangeSellablStackCountInCustomerOrder(customer, stackInt.Key, stackInt.Value))
                        return false;
                    else success = true;

            return success;
        }


        public bool CheckoutCustomer(Guid customerId)
        {
            return _toyRepository.CheckoutCustomer(customerId);
        }

        /// <summary>
        /// checks if stack is from the same location as the order
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool _checkIfCanAddToOrder(SellableStack stack, Order order)
        {
            if (order == null || order.OrderLocation == null)
            {
                return true;
            }
            return stack.location.LocationId == order.OrderLocation.LocationId;
        }


        /// <summary>
        /// checks if order has the same stack already. <br/>
        /// does not check if the stack is from the same location. <br/>
        /// so must check that first
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool _checkIfStackIsInOrder(SellableStack stack, Order order)
        {
            if (order == null || order.cart == null || order.cart.Count <= 0 || order.cart.ToList()[0] == null)
                return true;
            return !order.cart.Any(s => s.Item.SellableId == stack.Item.SellableId && s.location.LocationId == stack.location.LocationId);
        }
    }
}