using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkFwTest.Model
{
    public class OracleDbContext : DbContext
    {
        private readonly OracleConnection _context;
        private readonly OracleCommand cmd;

        public OracleDbContext(string context)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[context].ConnectionString;
            _context = new OracleConnection { ConnectionString = connectionString }
                       ?? throw new ArgumentNullException(nameof(context));
            _context.Open();
            cmd = _context.CreateCommand();
        }

        public void Close()
        {
            _context.Close();
            Dispose();
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DataTable SqlQuery(CommandType commandType, string query, params object[] parameters)
        {
            cmd.CommandType = commandType;
            cmd.CommandText = query;
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            OracleDataReader dataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }

        public async Task<DataTable> SqlQueryAsync(CommandType commandType, string query, params object[] parameters)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OracleDataReader dataReader = (OracleDataReader)(await cmd.ExecuteReaderAsync());
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }

        public List<TEntity> FromSql<TEntity>(string sql) where TEntity : class, new()
        {
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

        //public int SaveChanges(bool ensureAutoHistory = false)
        //{
        //    try
        //    {
        //        if (ensureAutoHistory)
        //        {
        //            _context.EnsureAutoHistory();
        //        }

        //        return _context.SaveChanges();
        //    }
        //    catch
        //    {
        //        throw new ClientExceptionDatabase(400, "Can't save because data is invalid!. You need to double check the data fields when copying and pasting.");
        //    }
        //}
        //public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        //{
        //    try
        //    {
        //        if (ensureAutoHistory)
        //        {
        //            // _context.EnsureAutoHistory();
        //        }

        //        return await _context.SaveChangesAsync();
        //    }
        //    catch
        //    {
        //        throw new ClientExceptionDatabase(400, "Can't save because data is invalid!. You need to double check the data fields when copying and pasting.");
        //    }
        //}
        //public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        //{
        //    try
        //    {
        //        using (var ts = new TransactionScope())
        //        {
        //            var count = 0;
        //            foreach (var unitOfWork in unitOfWorks)
        //            {
        //                count += await unitOfWork.SaveChangesAsync(ensureAutoHistory);
        //            }

        //            count += await SaveChangesAsync(ensureAutoHistory);

        //            ts.Complete();

        //            return count;
        //        }
        //    }
        //    catch
        //    {
        //        throw new ClientExceptionDatabase(400, "Can't save because data is invalid!. You need to double check the data fields when copying and pasting.");
        //    }
        //}

        ~OracleDbContext()
        {
            Close();
        }
    }
}
