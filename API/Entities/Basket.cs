using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = new();

        public void AddItem(Product product, int quantity)
        {
            // If Item isn't in basket add it.
            if(Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem{Product = product, Quantity = quantity});
            }

            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);

            if(existingItem != null) existingItem.Quantity += quantity;
        }

        public void RemoveItem(int productId, int quanity) 
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);

            if(item == null) return;

            item.Quantity -= quanity;
            if(item.Quantity == 0) Items.Remove(item);
        }

        public static async Task<Basket> RetrieveBasket(HttpResponse response, StoreContext contex, string buyerId)
        {
            if(string.IsNullOrEmpty(buyerId))
            {
                response.Cookies.Delete("buyerId");
                return null;
            }

            return await contex.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
        }
    }
}