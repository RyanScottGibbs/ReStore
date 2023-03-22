using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _context;
        public BasketController(StoreContext context)
        {
            _context = context;            
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await Basket.RetrieveBasket(Response, _context, GetBuyerId());
            if (basket == null) return NotFound();
            return basket.MapBasketToDto();
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            // Get Basket || Create Basket.
            var basket = await Basket.RetrieveBasket(Response, _context, GetBuyerId());
            if(basket == null) basket = CreateBasket();

            // Get Product.
            var product = await _context.Products.FindAsync(productId);
            if(product == null) return BadRequest(new ProblemDetails{Title = "Product Not Found"});

            // Add Item.
            basket.AddItem(product, quantity);

            // Save Changes.
            var result = await _context.SaveChangesAsync() > 0;
            if(result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket."});
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            // Get Basket.
            var basket = await Basket.RetrieveBasket(Response, _context, GetBuyerId());
            if(basket == null) return NotFound();
            
            // Remove item or reduce qty.
            basket.RemoveItem(productId, quantity);

            // Save changes.
            var result = await _context.SaveChangesAsync() > 0;
            if(result) return Ok();

            return BadRequest(new ProblemDetails{Title = "Problem removing item from Basket."});
        }

        private string GetBuyerId() 
        {
            return User.Identity?.Name ?? Request.Cookies["buyerId"];
        }
        
        private Basket CreateBasket()
        {
            var buyerId = User.Identity?.Name;
            if(string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)};
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }
            
            var basket = new Basket{BuyerId = buyerId};
            _context.Baskets.Add(basket);

            return basket;
        }
    }
}