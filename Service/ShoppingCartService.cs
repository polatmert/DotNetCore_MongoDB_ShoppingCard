using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_MongoDB_ShoppingCard.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DotNetCore_MongoDB_ShoppingCard.Service
{
    public class ShoppingCartService
    {
        private readonly IMongoCollection<ShoppingCart> _shoppingCart;

        public ShoppingCartService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("ecommerceDB"));
            var database = client.GetDatabase("ecommerceDB");
            _shoppingCart = database.GetCollection<ShoppingCart>("ShoppingCart");
        }

        public List<ShoppingCart> Get()
        {
            return _shoppingCart.Find(shoppingCart => true).ToList();
        }

        public ShoppingCart GetById(string id)
        {
            return _shoppingCart.Find<ShoppingCart>(shoppingCart => shoppingCart.Id.Equals(id)).FirstOrDefault();
        }

        public ShoppingCart GetByCustomerId(string customerId)
        {
            return _shoppingCart.Find<ShoppingCart>(shoppingCart => shoppingCart.CustomerId.Equals(customerId)).FirstOrDefault();
        }

        public ShoppingCart Create(ShoppingCart shoppingCart)
        {
            _shoppingCart.InsertOne(shoppingCart);
            return shoppingCart;
        }

        public void Update(string id, ShoppingCart shoppingCartIn)
        {
            _shoppingCart.ReplaceOne(shoppingCart => shoppingCart.Id == id, shoppingCartIn);
        }

        public void Remove(ShoppingCart shoppingCartIn)
        {
            _shoppingCart.DeleteOne(shoppingCart => shoppingCart.Id == shoppingCartIn.Id);
        }

        public void Remove(string id)
        {
            _shoppingCart.DeleteOne(ShoppingCart => ShoppingCart.Id == id);
        }

    }
}
