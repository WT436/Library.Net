using System.Data.Entity;
using System.Threading.Tasks;

namespace UnitOfWork
{

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        #region Db Context Support Genaric
        TContext DbContext { get; }
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks);
        #endregion
    }
}