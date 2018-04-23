using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dapper;

namespace FeedbackService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        private string _azureConn =
            "Server=tcp:fridai.database.windows.net,1433;Initial Catalog=TestDB;Persist Security Info=False;User ID=Fridai;Password=Skole123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<Feedback> GetAllFeedback()
        {
            //Feedback fb = new Feedback();
            //List<Feedback> Flist = new List<Feedback>();

            //using (SqlConnection connection = new SqlConnection(_azureConn))
            //{
            //    connection.Open();
            //    string query = "SELECT * FROM dbo.Feedback";
            //    SqlCommand command = new SqlCommand(query, connection);

            //    using (SqlDataReader reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            fb.Id = Int32.Parse(reader["Id"].ToString());
            //            fb.Title = reader["title"].ToString();
            //            fb.Name = reader["name"].ToString();
            //            fb.Message = reader["message"].ToString();

            //            Flist.Add(fb);

            //        }

            //        return Flist;
            //    }
            //}

            using (IDbConnection dapConnection = new SqlConnection(_azureConn))
            {
                return dapConnection.Query<Feedback>("SELECT * FROM dbo.Feedback").ToList();
               
            }
            
        }

        public Feedback GetOneFeedback(string id)
        {
            Feedback fb = new Feedback();
            using (SqlConnection connection = new SqlConnection(_azureConn))
            {
                connection.Open();
                string query = $"SELECT * FROM dbo.Feedback where id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        fb.Id = Int32.Parse(reader["Id"].ToString());
                        fb.Title = reader["title"].ToString();
                        fb.Name = reader["name"].ToString();
                        fb.Message = reader["message"].ToString();



                    }
                    connection.Close();
                    return fb;
                }
            }
        }
    }
}
