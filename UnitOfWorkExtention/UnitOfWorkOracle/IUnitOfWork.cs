using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkOracle
{
    public interface IUnitOfWork : IDisposable
    {
        //*---------------------------------------------------------------------------*//
        //* CREATOR          : TRẦN HỮU HẢI NAM ( WT436 )                             *//
        //* DATE_CREATE      :                                *//
        //* DATE_UPDATE      :                                *//
        //* REFERENCE_SOURCE : GITHUB,GOOGLE                                          *//
        //* DESCRIPTION      : SUPPORT CONNECTING AND QUESTIONING MULTIPLE DATABASES  *//
        //*---------------------------------------------------------------------------*//

        int SaveChanges(bool ensureAutoHistory = false);
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);
        int ExecuteSqlCommand(string sql, params object[] parameters);
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
        List<TEntity> FromSql<TEntity>(string sql) where TEntity : class, new();
        Task<List<TEntity>> FromSqlAsync<TEntity>(string sql) where TEntity : class, new();
        DataTable FromSql(CommandType commandType, string query, params object[] parameters);
        Task<DataTable> FromSqlAsync(CommandType commandType, string query, params object[] parameters);

    }
}