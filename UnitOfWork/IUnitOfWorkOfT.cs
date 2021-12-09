using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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