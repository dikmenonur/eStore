using eStore.Basket.Core.Entity;
using eStore.Contracts.Domains.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.Basket.Data.DataSource
{
    public interface IBasketDataSource : IDataSourceBase<Cart, long>
    {
        Task<Cart> AddItemToBasket(long basketId, CartItem item);

        Task<Cart> UpdateBasket(Cart cart);

        Task CreateBasket(Cart cart);

        Task<List<Cart>> GetAllBasket();

        Task<Cart> GetBasketById(int id);
    }
}
