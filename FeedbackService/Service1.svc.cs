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
using Newtonsoft.Json;

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

        public bool PostFeedback(Feedback f)
        {

            using (SqlConnection connection = new SqlConnection(_azureConn))
            {

                string query =
                    $"INSERT INTO dbo.Feedback (Id, name, title, message) VALUES (@id,@name,@title,@message)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id", f.Id);
                    command.Parameters.AddWithValue("@name", f.Name);
                    command.Parameters.AddWithValue("@title", f.Title);
                    command.Parameters.AddWithValue("@message", f.Message);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }


            }
        }

        public bool DeleteFeedback(Feedback f)
        {
            using (SqlConnection connection = new SqlConnection(_azureConn))
            {

                string query =
                    $"DELETE dbo.Feedback WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id", f.Id);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }


            }
        }

        public bool PUTFeedback(Feedback f)
        {
            using (SqlConnection connection = new SqlConnection(_azureConn))
            {

                string query =
                    $"UPDATE dbo.Feedback SET(Id, name, title, message) VALUES (@id,@name,@title,@message) WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id", f.Id);
                    command.Parameters.AddWithValue("@name", f.Name);
                    command.Parameters.AddWithValue("@title", f.Title);
                    command.Parameters.AddWithValue("@message", f.Message);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
        }
    }
}
