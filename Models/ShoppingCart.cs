using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetCore_MongoDB_ShoppingCard.Models
{
    public class ShoppingCart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ShoppingCartItems")]
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        [BsonElement("TotalAmount")]
        public double TotalAmount { get; set; }

        [BsonElement("CustomerId")]
        public string CustomerId { get; set; }
    }
}