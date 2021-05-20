using CleaningSimReport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using CleaningSimReport.Models;

namespace CleaningSimReport.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();

        List<Report> reports = new List<Report>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = CleaningSimReport.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            FetchData();
            return View(reports);
        }

        private void FetchData()
        {
            if (reports.Count > 0)   //prevent displaying duplication
            {
                reports.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) " +
                    "[report_id]," +
                    "[user_id]," +
                    "[time_seconds]," +
                    "[mistakes_count]," +
                    "[level_name] " +
                    "FROM[dbo].[Report]";
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    reports.Add(new Report()
                    {
                        report_id = dr["report_id"].ToString(),
                        user_id = dr["user_id"].ToString(),
                        time_seconds = dr["time_seconds"].ToString(),
                        mistakes_count = dr["mistakes_count"].ToString(),
                        level_name = dr["level_name"].ToString()
                    });
                }

                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult About()
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
