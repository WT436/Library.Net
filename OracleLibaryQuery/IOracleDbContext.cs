using OracleLibaryQuery.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OracleLibaryQuery
{
    public interface IOracleDbContext
    {
        int InsertRanger<T>(string sql, List<OracleFillParameter> parameters, List<T> data);
        IEnumerable<TEntity> ExecuteReader<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new();
        Task<IEnumerable<TEntity>> ExecuteReaderAsync<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new();
        IEnumerable<TEntity> ExecuteReader<TEntity>(string sql) where TEntity : class, new();
        Task<IEnumerable<TEntity>> ExecuteReaderAsync<TEntity>(string sql) where TEntity : class, new();
        int ExecuteNonQuery(string sql, List<OracleFillParameter> parameters);
        Task<int> ExecuteNonQueryAsync(string sql, List<OracleFillParameter> parameters);
        IEnumerable<TEntity> ExecuteScalar<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new();
        Task<IEnumerable<TEntity>> ExecuteScalarAsync<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new();
        Stream ExecuteStream();
        Task<Stream> ExecuteStreamAsync();
        Stream ExecuteToStream(Stream outputStream);
        Task<Stream> ExecuteToStreamAsync(Stream outputStream);
        IEnumerable<TEntity> FromSql<TEntity>(string sql) where TEntity : class, new();
        IEnumerable<TEntity> FromSqlAsync<TEntity>(string sql) where TEntity : class, new();
    }
}
