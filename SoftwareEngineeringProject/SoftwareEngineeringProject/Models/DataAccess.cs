using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Models
{
    public class DataAccess
    {
        public static DataSet RetrieveData(string connString, string query)
        {
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, conn);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }
    }
}
