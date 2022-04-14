using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly string connectionString;

        private string sqlGetUser = "SELECT user_id, username, password_hash, salt, user_role FROM users WHERE username = @username";
        private string sqlAddUser = "INSERT INTO family DEFAULT VALUES; " +
                                    "INSERT INTO users (username, password_hash, salt, user_role, family_id) VALUES " +
                                    "(@username, @password_hash, @salt, @user_role, (SELECT MAX(family_id) FROM family))";
        private string sqlAddNewFamilyMember = "INSERT INTO users (username, password_hash, salt, user_role, family_id) VALUES " +
            "(@username, @password_hash, @salt, @user_role, (SELECT family_id FROM users WHERE user_id = @user_id))";
        private string sqlGetChildFamilyUsers = "SELECT user_id, username, user_role, family_id FROM users WHERE user_role = 'child' AND family_id = " +
                                                " (SELECT family_id FROM users WHERE user_id = @user_id);";

        public UserSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public User GetUser(string username)
        {
            User returnUser = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetUser, conn);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    returnUser = GetUserFromReader(reader);
                }
            }

            return returnUser;
        }


        public User AddUser(string username, string password, string role)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlAddUser, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                cmd.Parameters.AddWithValue("@salt", hash.Salt);
                cmd.Parameters.AddWithValue("@user_role", role);
                cmd.ExecuteNonQuery();
            }

            return GetUser(username);
        }

        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"]),
                Role = Convert.ToString(reader["user_role"]),
            };

            return u;
        }

        public User AddNewFamilyMember(string username, string password, string role, int parentId)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlAddNewFamilyMember, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                cmd.Parameters.AddWithValue("@salt", hash.Salt);
                cmd.Parameters.AddWithValue("@user_role", role);
                cmd.Parameters.AddWithValue("@user_id", parentId);
                cmd.ExecuteNonQuery();
            }

            return GetUser(username);
        }

        public List<ReturnUser> GetFamilyChildList(int userId)
        {
            List<ReturnUser> returnUsers = new List<ReturnUser>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetChildFamilyUsers, conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                SqlDataReader reader = cmd.ExecuteReader();

               while (reader.Read())
                {
                    ReturnUser toAdd = new ReturnUser();
                    toAdd.UserId = Convert.ToInt32(reader["user_id"]);
                    toAdd.Username = Convert.ToString(reader["username"]);
                    toAdd.Role = Convert.ToString(reader["user_role"]);
                    toAdd.FamilyId = Convert.ToInt32(reader["family_id"]);

                    returnUsers.Add(toAdd);
                }
            }

            return returnUsers;
        }
    }
}
