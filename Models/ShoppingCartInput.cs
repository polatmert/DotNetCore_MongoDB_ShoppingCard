using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_MongoDB_ShoppingCard.Models
{
    public class ShoppingCartInput
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public string CustomerId { get; set; }

    }
}
