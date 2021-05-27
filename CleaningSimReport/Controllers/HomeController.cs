using CleaningSimReport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CleaningSimReport.Controllers
{
    public class HomeController : Controller
    {

        private IConfiguration Configuration;

        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();

        List<Report> reports = new List<Report>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration)  //ok this part from the docs I don't get but it works ?
        {
            Configuration = configuration;
            _logger = logger;

            // retrieve App Service connection string
            string myConnString = Configuration.GetConnectionString("SqlCon"); //getting the sql connection string from azure web app configuration so it is not exposed
                

            con.ConnectionString = myConnString;
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
                    "[level_name]," +
                    "[creation_datetime] " +
                    "FROM[dbo].[Report]" +
                    "ORDER BY [report_id] DESC";
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    reports.Add(new Report()
                    {
                        Report_id = dr["report_id"].ToString(),
                        User_id = dr["user_id"].ToString(),
                        Time_seconds = dr["time_seconds"].ToString(),
                        Mistakes_count = dr["mistakes_count"].ToString(),
                        Level_name = dr["level_name"].ToString(),
                        Creation_datetime = dr["creation_datetime"].ToString()
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
