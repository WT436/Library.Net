using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UnitOfWorkOracle
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly OracleConnection _context;

        public UnitOfWork(string context)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[context].ConnectionString;
            _context = new OracleConnection { ConnectionString = connectionString }
                       ?? throw new ArgumentNullException(nameof(context));
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            _context.Close();
            Dispose();
        }

        public DataTable FromSql(CommandType commandType, string query, params object[] parameters)
        {
            _context.Open();
            using (var cmd = _context.CreateCommand())
            {
                cmd.CommandType = commandType;
                cmd.CommandText = query;
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                OracleDataReader dataReader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                return dataTable;
            }
        }

        public async Task<DataTable> FromSqlAsync(CommandType commandType, string query, params object[] parameters)
        {
            _context.Open();
            using (var cmd = _context.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                await _context.OpenAsync();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                OracleDataReader dataReader = (OracleDataReader)(await cmd.ExecuteReaderAsync());
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                return dataTable;
            }
        }

        public List<TEntity> FromSql<TEntity>(string sql) where TEntity : class, new()
        {
            using (var cmd = _context.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                _context.Open();
                using (var reader = cmd.ExecuteReader())
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
            Close();

            using (var command = _context.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                await _context.OpenAsync();

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
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}