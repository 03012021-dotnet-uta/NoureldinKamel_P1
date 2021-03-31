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
                    if (!_toyRepository.AddSellableStackToCustomerOrder(customer, stack))
                        return false;
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
    }
}