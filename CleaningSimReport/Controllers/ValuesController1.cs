using CleaningSimReport.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class ValuesController1 : ControllerBase
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();

        List<Report> reports = new List<Report>();

        // GET: api/<ValuesController1>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            

            MakeConnection();
            string Command = "SELECT TOP (1) [user_id]" +
                "FROM[dbo].[User_Table] WHERE[user_id] = " + 1;

            MakeCommandText(Command);
            ExecuteCommand();
            CloseConnection();
            string response = dr.Read().ToString();
            return new string[] { response };

            //return new string[] { "value1", "value2"};
        }

        // GET api/<ValuesController1>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value"+id;
        }

        // POST api/<ValuesController1>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
