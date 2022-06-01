using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OracleLibaryQuery.Collections
{
    public class OracleFillParameter
    {
        public string Name { get; set; }
        public OracleDbType Type { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}
