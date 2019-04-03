using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_MongoDB_ShoppingCard.Models
{
    public class ShoppingCartItem
    {
        [BsonElement("ProductId")]
        public string ProductId;

        [BsonElement("Qty")]
        public int Qty;

        [BsonElement("Amount")]
        public double Amount;
    }
}
