using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_MongoDB_ShoppingCard.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DotNetCore_MongoDB_ShoppingCard.Service
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productService;

        public ProductService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("ecommerceDB"));
            var database = client.GetDatabase("ecommerceDB");
            _productService = database.GetCollection<Product>("Product");
        }

        public Product GetProductById(string id)
        {
            return _productService.Find<Product>(product => product.Id.Equals(id)).FirstOrDefault();
        }
    }
}
