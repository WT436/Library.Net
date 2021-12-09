using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UnitOfWork.Repository;

namespace UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //*---------------------------------------------------------------------------*//
        //* CREATOR          : TRẦN HỮU HẢI NAM ( WT436 )                             *//
        //* DATE_CREATE      : Monday, March 18, 2020                                 *//
        //* DATE_UPDATE      : Friday, August 6, 2021                                 *//
        //* REFERENCE_SOURCE : GITHUB,GOOGLE                                          *//
        //* DESCRIPTION      : SUPPORT CONNECTING AND QUESTIONING MULTIPLE DATABASES  *//
        //*---------------------------------------------------------------------------*//

        void ChangeDatabase(string database);
        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
        int SaveChanges(bool ensureAutoHistory = false);
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);
        int ExecuteSqlCommand(string sql, params object[] parameters);
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
        List<TEntity> FromSql<TEntity>(string sql) where TEntity : class, new();
        Task<List<TEntity>> FromSqlAsync<TEntity>(string sql) where TEntity : class, new();
        void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback);
    }
}