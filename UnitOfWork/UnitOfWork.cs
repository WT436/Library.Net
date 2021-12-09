using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using UnitOfWork.Repository;

namespace UnitOfWork
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {
        #region access method
        private readonly TContext _context;
        private bool disposed = false;
        private Dictionary<Type, object> repositories;
        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public TContext DbContext => _context;
        public void ChangeDatabase(string database)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State.HasFlag(ConnectionState.Open))
            {
                connection.ChangeDatabase(database);
            }
            else
            {
                var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"(?<=[Dd]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
                connection.ConnectionString = connectionString;
            }
            var items = _context.Model.GetEntityTypes();
            foreach (var item in items)
            {
                if (item is IConventionEntityType entityType)
                {
                    entityType.SetSchema(database);
                }
            }
        }
        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }
            if (hasCustomRepository)
            {
                var customRepo = _context.GetService<IRepository<TEntity>>();
                if (customRepo != null)
                {
                    return customRepo;
                }
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>)repositories[type];
        }
        public int ExecuteSqlCommand(string sql, params object[] parameters) => _context.Database.ExecuteSqlRaw(sql, parameters);
        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class => _context.Set<TEntity>().FromSqlRaw(sql, parameters);
        public List<TEntity> FromSql<TEntity>(string sql) where TEntity : class, new()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    var lst = new List<TEntity>();
                    var lstColumns = new TEntity().GetType()
                                                  .GetProperties(BindingFlags.DeclaredOnly |
                                                                 BindingFlags.Instance |
                                                                 BindingFlags.Public |
                                                                 BindingFlags.NonPublic)
                                                  .ToList();
                    while (reader.Read())
                    {
                        var newObject = new TEntity();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            PropertyInfo prop = lstColumns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                            if (prop == null)
                            {
                                continue;
                            }
                            var val = reader.IsDBNull(i) ? null : reader[i];
                            prop.SetValue(newObject, val, null);
                        }
                        lst.Add(newObject);
                    }

                    return lst;
                }
            }
        }
        public async Task<List<TEntity>> FromSqlAsync<TEntity>(string sql) where TEntity : class, new()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                await _context.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var lst = new List<TEntity>();
                    var lstColumns = new TEntity().GetType()
                                                  .GetProperties(BindingFlags.DeclaredOnly |
                                                                 BindingFlags.Instance |
                                                                 BindingFlags.Public |
                                                                 BindingFlags.NonPublic)
                                                  .ToList();
                    while (await reader.ReadAsync())
                    {
                        var newObject = new TEntity();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            PropertyInfo prop = lstColumns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                            if (prop == null)
                            {
                                continue;
                            }
                            var val = reader.IsDBNull(i) ? null : reader[i];
                            prop.SetValue(newObject, val, null);
                        }
                        lst.Add(newObject);
                    }

                    return lst;
                }
            }
        }
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            try
            {
                if (ensureAutoHistory)
                {
                    _context.EnsureAutoHistory();
                }

                return _context.SaveChanges();
            }
            catch
            {
                throw new ClientExceptionDatabase(400, "Can't save because data is invalid!. You need to double check the data fields when copying and pasting.");
            }
        }
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            try
            {
                if (ensureAutoHistory)
                {
                    _context.EnsureAutoHistory();
                }

                return await _context.SaveChangesAsync();
            }
            catch
            {
                throw new ClientExceptionDatabase(400, "Can't save because data is invalid!. You need to double check the data fields when copying and pasting.");
            }
        }
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    var count = 0;
                    foreach (var unitOfWork in unitOfWorks)
                    {
                        count += await unitOfWork.SaveChangesAsync(ensureAutoHistory);
                    }

                    count += await SaveChangesAsync(ensureAutoHistory);

                    ts.Complete();

                    return count;
                }
            }
            catch
            {
                throw new ClientExceptionDatabase(400, "Can't save because data is invalid!. You need to double check the data fields when copying and pasting.");
            }
        }
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (repositories != null)
                    {
                        repositories.Clear();
                    }
                    _context.Dispose();
                }
            }

            disposed = true;
        }
        public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
        {
            _context.ChangeTracker.TrackGraph(rootEntity, callback);
        }
        #endregion access method
    }
}