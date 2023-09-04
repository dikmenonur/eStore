using eStore.Basket.Core.Entity;
using eStore.Contracts.Domains.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Basket.Data.DataSource
{
    public class BasketDataSource : DataSourceBase<Cart, long, BasketDbContext>, IBasketDataSource
    {
        public BasketDataSource(BasketDbContext context) : base(context)
        {
        }

        public async Task<Cart> AddItemToBasket(long basketId, CartItem item)
        {
            var cart = await FindByIdAsync(basketId);
            if (cart != null)
            {
                cart.AddItem(item);
                await Save();
                return cart;
            }

            return null;

        }

        public async Task<Cart> UpdateBasket(Cart cart)
        {
            var updateCart = await FindByIdAsync(cart.Id);
            if (updateCart != null)
            {

            }
            throw new NotImplementedException();
        }

        public async Task CreateBasket(Cart cart)
        {
            await AddAsync(cart);
            await Save();
        }

        public async Task<List<Cart>> GetAllBasket()
        {
            return GetAll().ToList();
        }

        public async Task<Cart> GetBasketById(int id)
        {
            return await FindByIdAsync(id);
        }



    }
}
