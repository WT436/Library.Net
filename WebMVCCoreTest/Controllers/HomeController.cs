using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using OracleLibaryQuery;
using OracleLibaryQuery.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebMVCCoreTest.Domain.Entity;
using WebMVCCoreTest.Models;

namespace WebMVCCoreTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOracleDbContext _context;

        public HomeController(ILogger<HomeController> logger, IOracleDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //List<OracleFillParameter> parameters = new List<OracleFillParameter>
            //{
            //    new OracleFillParameter{Name="P_CURSOR",Type = OracleDbType.RefCursor,Direction = ParameterDirection.Output }
            //};
            //var data = _context.ExecuteReader<Countries>("pkg_update_countries.UPDATE_COUNTRIES", parameters: parameters);            

            List<Countries> countries = new List<Countries>
            {
                new Countries{CountryId = "120", CountryName = "HaiNam1"},
                new Countries{CountryId = "121", CountryName = "HaiNam2"},
                new Countries{CountryId = "122", CountryName = "HaiNam3"},
                new Countries{CountryId = "123", CountryName = "HaiNam4"},
                new Countries{CountryId = "124", CountryName = "HaiNam5"}
            };

            List<OracleFillParameter> parameters = new List<OracleFillParameter>
            {
                new OracleFillParameter{Name="REGION_ID",Type = OracleDbType.Int64,Direction = ParameterDirection.Input },
                new OracleFillParameter{Name="REGION_NAME",Type = OracleDbType.Varchar2,Direction = ParameterDirection.Input }
            };

            _context.InsertRanger("PKG_TEST_INSERT.TESTPROCEDURE", parameters, countries);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
