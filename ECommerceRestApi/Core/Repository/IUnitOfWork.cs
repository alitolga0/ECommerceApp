namespace ECommerceRestApi.Core.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
