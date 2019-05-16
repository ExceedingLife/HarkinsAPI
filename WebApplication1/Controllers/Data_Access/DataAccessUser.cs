using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Configuration;

namespace WebApplication1.Controllers.Data_Access
{
    public class DataAccessUser
    {
        readonly SqlConnection connString = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        // Retrieve ALL Users in Database
        public IEnumerable<User> GetAllUsers()
        {
            List<User> lstUsers = new List<User>();

            using (SqlConnection conn = connString)
            {
                SqlCommand cmd = new SqlCommand("spGetAllUsers", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User()
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        DateCreated = Convert.ToDateTime(reader["DateCreated"])
                    };
                    lstUsers.Add(user);
                }
                conn.Close();
            }
            return lstUsers;
        }

        // ADD User to Database
        public void CreateUser(User user)
        {
            using(SqlConnection conn = connString)
            {
                SqlCommand cmd = new SqlCommand("spCreateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fn", user.FirstName);
                cmd.Parameters.AddWithValue("@ln", user.LastName);
                cmd.Parameters.AddWithValue("@em", user.Email);
                cmd.Parameters.AddWithValue("@un", user.UserName);
                cmd.Parameters.AddWithValue("@pw", user.Password);
                cmd.Parameters.AddWithValue("@dc", user.DateCreated);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // UPDATE User in Database
        public void UpdateUser(User user)
        {
            using (SqlConnection conn = connString)
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", user.UserId);
                cmd.Parameters.AddWithValue("@fn", user.FirstName);
                cmd.Parameters.AddWithValue("@ln", user.LastName);
                cmd.Parameters.AddWithValue("@em", user.Email);
                cmd.Parameters.AddWithValue("@un", user.UserName);
                cmd.Parameters.AddWithValue("@pw", user.Password);
                cmd.Parameters.AddWithValue("@dc", user.DateCreated);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Retrieve User by ID
        public User GetUserById(int? id)
        {
            User user = new User();

            using(SqlConnection conn = connString)
            {
                string query = "SELECT * FROM Users WHERE UserId = " + id;

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.FirstName = reader["FirstName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.UserName = reader["UserName"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
                }
            }
            return user;
        }

        // Delete User in Database by ID
        public void DeleteUser(int? id)
        {
            using(SqlConnection conn = connString)
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}