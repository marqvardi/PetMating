using System;
using System.Threading.Tasks;

namespace PetMating.Api.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //    ICategoryRepository Category { get; }
        // IFoodTypeRepository Foodtype { get; }
        // IMenuItemRepository MenuItem{ get; }
        // IApplicationUserRepository ApplicationUser { get; }
        // IShoppingCartRepository ShoppingCart { get; }
        // IOrderDetailsRepository OrderDetails { get; }
        // IOrderHeaderRepository OrderHeader { get; }

        ICustomerRepository Customer { get; }

        IUserRepository User { get; }

        IAnimalRepository Animal { get; }
        void Save();
        Task<int> Complete();
    }
}