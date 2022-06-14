using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace OracleLibaryQuery.Collections
{
    public class OracleFillParameter
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public OracleDbType Type { get; set; }
        [Required]
        public ParameterDirection Direction { get; set; }
        public Object Data { get; set; }
    }
}
