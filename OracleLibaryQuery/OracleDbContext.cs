using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleLibaryQuery.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OracleLibaryQuery
{
    [OracleCustomTypeMapping("countries_table_update")]
    public class countries_table_update
    {
        [OracleObjectMappingAttribute("COUNTRY_ID")]
        public string COUNTRY_ID { get; set; }
        [OracleObjectMappingAttribute("COUNTRY_NAME")]
        public string COUNTRY_NAME { get; set; }
        [OracleObjectMappingAttribute("REGION_ID")]
        public int REGION_ID { get; set; }
    }

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
        public IEnumerable<TEntity> ExecuteReader<TEntity>(string sql, List<OracleFillParameter> parameters) where TEntity : class, new()
        {
            using var command = _context.CreateCommand();
            OracleDataAdapter da = new OracleDataAdapter() { SelectCommand = command };

            command.CommandText = sql;
            command.CommandType = CommandType.StoredProcedure;

            if (parameters.Count >= 1)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter.Name, parameter.Type, parameter.Direction);
                }
            }

            //Create the input object
            //Create the input object
            countries_table_update inputObject = new countries_table_update { COUNTRY_ID = "A", COUNTRY_NAME = "s", REGION_ID = 2 };
            command.Parameters.Add("P_TABLE", "COUNTRIES_TABLE_UPDATE").Value = inputObject;
            ////Create the input parameter
            //OracleParameter parameter_in = command.CreateParameter();
            //parameter_in.OracleDbType = OracleDbType.Object;
            //parameter_in.Direction = ParameterDirection.Input;
            //parameter_in.ParameterName = "P_TABLE";
            //parameter_in.UdtTypeName = "countries_table_update";
            //parameter_in.Value = inputObject;
            //command.Parameters.Add(parameter_in);

            _context.Open();
            //ExecuteNonQuery
            //ExecuteReader
            //ExecuteNonQueryAsync
            //ExecuteReaderAsync
            //ExecuteScalar
            //ExecuteStream
            //ExecuteToStream
            //ExecuteXmlReader

            command.ExecuteNonQuery();

            DataTable dt = new DataTable();
            da.Fill(dt);
            var lst = FillCollection<TEntity>.FillCollectionFromDataTable(dt);
            _context.Close();
            return lst;
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
}
