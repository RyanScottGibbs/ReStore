using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreContext _contex;
        public BasketController(StoreContext context)
        {
            _contex = context;            
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();
            return MapBasketToDto(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            // Get Basket || Create Basket.
            var basket = await RetrieveBasket();
            if(basket == null) basket = CreateBasket();

            // Get Product.
            var product = await _contex.Products.FindAsync(productId);
            if(product == null) return NotFound();

            // Add Item.
            basket.AddItem(product, quantity);

            // Save Changes.
            var result = await _contex.SaveChangesAsync() > 0;
            if(result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket."});
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            // Get Basket.
            var basket = await RetrieveBasket();
            if(basket == null) return NotFound();
            
            // Remove item or reduce qty.
            basket.RemoveItem(productId, quantity);

            // Save changes.
            var result = await _contex.SaveChangesAsync() > 0;
            if(result) return Ok();

            return BadRequest(new ProblemDetails{Title = "Problem removing item from Basket."});
        }

        private async Task<Basket> RetrieveBasket()
        {
            return await _contex.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
        }
        
        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)};

            Response.Cookies.Append("buyerId", buyerId, cookieOptions);

            var basket = new Basket{BuyerId = buyerId};

            _contex.Baskets.Add(basket);

            return basket;
        }

        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}