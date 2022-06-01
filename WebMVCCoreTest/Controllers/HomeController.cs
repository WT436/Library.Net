using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using OracleLibaryQuery;
using OracleLibaryQuery.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebMVCCoreTest.Domain.Entity;
using WebMVCCoreTest.Models;

namespace WebMVCCoreTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOracleDbContext _context;

        public HomeController(ILogger<HomeController> logger , IOracleDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<OracleFillParameter> parameters = new List<OracleFillParameter>
            {
                new OracleFillParameter{Name="P_CURSOR",Type = OracleDbType.RefCursor,Direction = ParameterDirection.Output }
            };
            var data = _context.ExecuteReader<Countries>("pkg_update_countries.UPDATE_COUNTRIES", parameters: parameters);
            return View(data.ToList());
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
