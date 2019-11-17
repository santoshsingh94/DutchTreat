using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        //Products
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        //Order
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}