using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Controllers
{
    public class LookupController : Controller
    {

        public IActionResult Index() 
        {
            return View();
        }

        public IActionResult Lookup() {
            ViewData["Message"] = "Your contact page.";
            string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=aspnet-SoftwareEngineeringProject-704B70CD-4566-4C68-8E3F-9951B3FBE94A;Integrated Security=True";
            string query =  "SELECT r.Id, r.Requestor, u.user_type, " +
                            "u.first_name, u.last_name, u.campus, " +
                            "u.department, u.phone, r.Creation_Date, " +
                            "rk.Approval_Date, rk.status " +
                            "FROM Key_Request as r " +
                            "JOIN Key_Request_Lines as rk " +
                            "ON r.id = rk.Key_Request_ID " +
                            "JOIN Users as u " +
                            "ON r.Requestor = u.Banner_Id";
            DataSet dataset =  DataAccess.RetrieveData(connString, query);

            List<string[]> dataRows = new List<string[]>();

            ViewData["test"] = dataset;
            foreach (DataTable table in dataset.Tables) {
                foreach(DataRow row in table.Rows)
                {
                    
                }
            }
            


            return View();
        }
    }
}