using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_MongoDB_ShoppingCard.Models;
using DotNetCore_MongoDB_ShoppingCard.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_MongoDB_ShoppingCard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        public readonly ShoppingCartService _shoppingCartService;
        public readonly ProductService _productService;
        public ShoppingCartController(ShoppingCartService shoppingCartService, ProductService productService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public ActionResult<List<ShoppingCart>> Get()
        {
            return _shoppingCartService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetShoppingCart")]
        public ActionResult<ShoppingCart> Get(string id)
        {
            var shoppingCart = _shoppingCartService.GetById(id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            return shoppingCart;
        }

        [HttpPost]
        public ActionResult<ShoppingCart> Create(ShoppingCart shoppingCart)
        {
            _shoppingCartService.Create(shoppingCart);

            return CreatedAtRoute("GetBook", new { id = shoppingCart.Id.ToString() }, shoppingCart);
        }


        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ShoppingCart shoppingCartIn)
        {
            var shoppingCart = _shoppingCartService.GetById(id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            _shoppingCartService.Update(id, shoppingCartIn);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var shoppingCart = _shoppingCartService.GetById(id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            _shoppingCartService.Remove(shoppingCart.Id);

            return NoContent();
        }

        [HttpPost("addToShoppingCart")]
        public ActionResult<ShoppingCart> AddToShoppingCart(ShoppingCartInput pro)
        {
            var product = _productService.GetProductById(pro.ProductId);
            var shoppingCart = _shoppingCartService.GetByCustomerId(pro.CustomerId);

            if (product.Qty >= pro.Quantity)
            {
                if (shoppingCart == null)
                    shoppingCart = _shoppingCartService.Create(new ShoppingCart
                    {
                        CustomerId = pro.CustomerId,
                        ShoppingCartItems = new List<ShoppingCartItem>()
                    });


                shoppingCart.ShoppingCartItems.Add(new ShoppingCartItem
                {
                    ProductId = product.Id,
                    Qty = pro.Quantity,
                    Amount = product.Price

                });

                shoppingCart.TotalAmount += product.Price * pro.Quantity;
                _shoppingCartService.Update(shoppingCart.Id, shoppingCart);
            }            

            return shoppingCart;
        }

        [HttpPost("deleteToShoppingCart")]
        public ActionResult<ShoppingCart> DeleteToShoppingCart(ShoppingCartInput pro)
        {
            var product = _productService.GetProductById(pro.ProductId);
            var shoppingCart = _shoppingCartService.GetByCustomerId(pro.CustomerId);
            var shoppingCartItem = shoppingCart.ShoppingCartItems.Where(x => x.ProductId.Equals(pro.ProductId)).FirstOrDefault();

            shoppingCart.ShoppingCartItems.Remove(shoppingCartItem);
            if (product != null)
            {
                double total = product.Price * pro.Quantity;
                shoppingCart.TotalAmount = shoppingCart.TotalAmount - total;
                _shoppingCartService.Update(shoppingCart.Id, shoppingCart);
            }

            return shoppingCart;
        }
    }
}