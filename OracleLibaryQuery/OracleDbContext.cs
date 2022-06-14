using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleLibaryQuery.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OracleLibaryQuery
{
    public class OracleDbContext : IOracleDbContext
    {
        private readonly OracleConnection _context;

        /// <summary>
        ///  Cài đặt Packages :  Oracle.ManagedDataAccess.Core
        ///  <para> Add chuỗi kết nối tại application.json</para>
        ///  <para>"ConnectionStrings": {"ConnectStrOracle": "Chuỗi kết nối"}</para>
        ///  <para>ConnectStrOracle : Tên thay thế ConnectStrOracle</para>
        /// </summary>
        public OracleDbContext(string ConnectStrOracle)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                              .SetBasePath(Directory.GetCurrentDirectory())
                                              .AddJsonFile("appsettings.json")
                                              .Build();
            var oradb = configuration.GetConnectionString(ConnectStrOracle);

            _context = new OracleConnection(oradb);  // C#
        }

        /// <summary>
        ///  Cài đặt Packages :  Oracle.ManagedDataAccess.Core
        ///  <para> Add chuỗi kết nối tại application.json</para>
        ///  <para>"ConnectionStrings": {"ConnectStrOracle": "Chuỗi kết nối"}</para>
        /// </summary>
        public OracleDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                              .SetBasePath(Directory.GetCurrentDirectory())
                                              .AddJsonFile("appsettings.json")
                                              .Build();
            var oradb = configuration.GetConnectionString("ConnectStrOracle");

            _context = new OracleConnection(oradb);  // C#
        }

        public int InsertRanger<T>(string sql, List<OracleFillParameter> parameters, List<T> data)
        {
            try
            {
                using var command = _context.CreateCommand();

                OracleDataAdapter adapter = new OracleDataAdapter() { SelectCommand = command };

                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;

                var dt = data.ToDataTable();

                var paramConvert = parameters.ToArray();

                for (int i = 0; i < paramConvert.Count(); i++)
                {
                    var dataParam = dt.Rows.Cast<DataRow>().Select(row => row[i]).ToArray();
                    var arry = command.Parameters.Add(paramConvert[i].Name, paramConvert[i].Type);
                    arry.Direction = paramConvert[i].Direction;
                    arry.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                    arry.Value = dataParam;
                    arry.Size = dataParam.Count();
                    arry.ArrayBindStatus = Enumerable.Repeat(OracleParameterStatus.Success, dataParam.Count()).ToArray();
                }

                _context.Open();

                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TEntity> ExecuteReader<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new()
        {
            try
            {
                using var command = _context.CreateCommand();
                OracleDataAdapter da = new OracleDataAdapter() { SelectCommand = command };

                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters.Count >= 1)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter.Name, parameter.Type, parameter.Direction).Value = parameter.Data;
                    }
                }

                _context.Open();

                command.ExecuteNonQuery();

                DataTable dt = new DataTable();
                da.Fill(dt);
                var lst = FillCollection<TEntity>.FillCollectionFromDataTable(dt);
                _context.Close();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<TEntity>> ExecuteReaderAsync<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> ExecuteReader<TEntity>(string sql) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> ExecuteReaderAsync<TEntity>(string sql) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sql, List<OracleFillParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteNonQueryAsync(string sql, List<OracleFillParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> ExecuteScalar<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> ExecuteScalarAsync<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public Stream ExecuteStream()
        {
            throw new NotImplementedException();
        }

        public Task<Stream> ExecuteStreamAsync()
        {
            throw new NotImplementedException();
        }

        public Stream ExecuteToStream(Stream outputStream)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> ExecuteToStreamAsync(Stream outputStream)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FromSql<TEntity>(string sql) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FromSqlAsync<TEntity>(string sql) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

    }

    public static class PrimitiveTypes
    {
        #region Support
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        #endregion
    }

}
