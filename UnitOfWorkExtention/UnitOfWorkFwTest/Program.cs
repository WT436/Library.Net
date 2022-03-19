using Newtonsoft.Json;
using System;
using System.Data;
using System.Threading.Tasks;
using UnitOfWorkFwTest.EntityDto;
using UnitOfWorkOracle;

namespace UnitOfWorkFwTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IUnitOfWork oracleDbContext = new UnitOfWork("Entities");
            var dataSet = oracleDbContext.FromSql<ACCOUNTS>("SELECT * FROM ACCOUNTS");
           
            Console.ReadKey();
        }
    }
}
