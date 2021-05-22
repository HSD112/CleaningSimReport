using CleaningSimReport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleaningSimReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IConfiguration Configuration;

        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();

        private Report report;
        private User user;


        public ValuesController(IConfiguration configuration)
        {
            Configuration = configuration;

            string myConnString = Configuration.GetConnectionString("SqlCon"); //getting the sql connection string from azure web app configuration so it is not exposed

            con.ConnectionString = myConnString;
        }

        [HttpPost]
        public void SelectUser(int id)
        {
            MakeConnection();
            string Command = "SELECT TOP (1) [user_id]" +
                "FROM[dbo].[User_Table] WHERE[user_id] = " + id.ToString();

            MakeCommandText(Command);
            ExecuteCommand();
            CloseConnection();
            if (dr.Read())//if there is a row in the read
            {
                //then do nothing
            }
            else//if there is no value already, add a new entry
            {
                MakeConnection();
                Command = "INSERT INTO [dbo].[User_Table]" +
                    "([user_id])" +
                    "VALUES" +
                    "(<"+id+", int,>)";
                MakeCommandText(Command);
                ExecuteCommand();
                CloseConnection();
            }

        }

        private void MakeConnection()
        {
            try
            {
                con.Open();
                com.Connection = con;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void MakeCommandText(String command)
        {
            try
            {
                com.CommandText = command;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void ExecuteCommand()
        {
            try
            {
                dr = com.ExecuteReader();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void CloseConnection()
        {
            try
            {
                con.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
