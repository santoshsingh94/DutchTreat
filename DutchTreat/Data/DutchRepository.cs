﻿using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(o=>o.Items)
                .ThenInclude(i=>i.Product)
                .ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAll Product was called");
                return _ctx.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Product : {ex}");
                return null;
            }
            
        }

        public Order GetOrderById(int id)
        {
            //return _ctx.Orders.Find(id);  //This will not return all the OrderItems and Product(Navigational properties)
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o=>o.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                        .Where(p => p.Category == category)
                        .ToList();
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
