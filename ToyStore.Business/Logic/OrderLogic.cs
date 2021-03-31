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
            if (orderModel.addedStacks != null)
                foreach (var stack in orderModel.addedStacks)
                {
                    System.Console.WriteLine("added stack: ");
                    System.Console.WriteLine(stack.Item.SellableName);
                    System.Console.WriteLine(stack.Count);
                    System.Console.WriteLine(stack.Item.SellablePrice);
                    if (_checkIfCanAddToOrder(stack, customer.CurrentOrder))
                        if (_checkIfStackIsInOrder(stack, customer.CurrentOrder))
                        {
                            if (!_toyRepository.AddSellableStackToCustomerOrder(customer, stack))
                                return false;
                        }
                        else
                        {
                            if (!_toyRepository.ChangeSellablStackCountInCustomerOrder(customer, orderModel.newOrder, stack))
                                return false;
                        }
                }

            if (orderModel.removedStacks != null)
                foreach (var stack in orderModel.removedStacks)
                    if (!_toyRepository.RemoveSellableStackFromOrder(customer.CurrentOrder, stack))
                        return false;

            if (orderModel.countChangedStacks != null)
                foreach (var stack in orderModel.countChangedStacks)
                    if (!_toyRepository.ChangeSellablStackCountInCustomerOrder(customer, orderModel.newOrder, stack))
                        return false;
            // get the stored user
            // check every sellablestack in cart
            // if 
            return true;
        }


        /// <summary>
        /// checks if stack is from the same location as the order
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool _checkIfCanAddToOrder(SellableStack stack, Order order)
        {
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
            return order.cart.Any(s => s.Item.SellableId == stack.Item.SellableId);
        }
    }
}