using System;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string connectionString =
                "Server=[server_name];Database=[db_name];User Id=[user_name];Password=[password];"; // if server is localhost use Server=.
            var user = new User
            {
                FirstName = "Sally",
                LastName = "Ali",
                Email = "s@test.com",
                PhoneNumber = "777-12-12",
                MembershipDate = DateTime.Now,
                IsActive = true
            };
            PrintAllUsers(connectionString);
            // PrintUsersByFirstName(connectionString,"John");
            // SearchForUsers(connectionString, "J");
            // PrintUserNameById(connectionString, 1);
            // PrintUserById(connectionString,12);
            // AddNewUser(connectionString, ref user);
            // UpdateUserById(connectionString,1,"Samy");
            // DeleteUserById(connectionString,17);
        }

        private static void PrintAllUsers(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            const string query = "SELECT * FROM Users";
            var sqlCommand = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var userId = (int)reader["UserID"];
                    var firstName = (string)reader["FirstName"];
                    var lastName = (string)reader["LastName"];
                    var email = (string)reader["Email"];
                    var phoneNumber = (string)reader["PhoneNumber"];
                    var membershipDate = (DateTime)reader["MembershipDate"];
                    var isActive = (bool)reader["IsActive"];

                    Console.WriteLine(
                        $"{userId},{firstName},{lastName},{email},{phoneNumber},{membershipDate.ToShortDateString()},{isActive}");
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static void PrintUsersByFirstName(string connectionString, string firstName)
        {
            var connection = new SqlConnection(connectionString);
            const string query = "SELECT * FROM Users WHERE FirstName = @FirstName";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var userId = (int)reader["UserId"];
                    var lastName = (string)reader["LastName"];
                    var email = (string)reader["Email"];
                    var phoneNumber = (string)reader["PhoneNumber"];
                    var membershipDate = (DateTime)reader["MembershipDate"];
                    var isActive = (bool)reader["IsActive"];
                    Console.WriteLine(
                        $"{userId},{firstName},{lastName},{email},{phoneNumber},{membershipDate.ToShortDateString()},{isActive}");
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static void SearchForUsers(string connectionString, string searchQuery)
        {
            var connection = new SqlConnection(connectionString);
            const string query = "SELECT * FROM Users WHERE FirstName LIKE '' + @searchQuery + '%'";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@searchQuery", searchQuery);
            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var userId = (int)reader["UserId"];
                    var firstName = (string)reader["FirstName"];
                    var lastName = (string)reader["LastName"];
                    var email = (string)reader["Email"];
                    var phoneNumber = (string)reader["PhoneNumber"];
                    var membershipDate = (DateTime)reader["MembershipDate"];
                    var isActive = (bool)reader["IsActive"];
                    Console.WriteLine(
                        $"{userId},{firstName},{lastName},{email},{phoneNumber},{membershipDate.ToShortDateString()},{isActive}");
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static void PrintUserNameById(string connectionString, int id)
        {
            var connection = new SqlConnection(connectionString);
            const string query = "SELECT FirstName FROM Users WHERE UserID = @id";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    var userName = result.ToString();
                    Console.WriteLine(userName);
                }
                else
                {
                    Console.WriteLine("User Was Not Found!");
                }

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static void PrintUserById(string connectionString, int id)
        {
            var isFound = false;
            var connection = new SqlConnection(connectionString);
            const string query = "SELECT * FROM Users WHERE USERID=@id";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    isFound = true;
                    var userId = (int)reader["UserId"];
                    var firstName = (string)reader["FirstName"];
                    var lastName = (string)reader["LastName"];
                    var email = (string)reader["Email"];
                    var phoneNumber = (string)reader["PhoneNumber"];
                    var membershipDate = (DateTime)reader["MembershipDate"];
                    var isActive = (bool)reader["IsActive"];
                    Console.WriteLine(
                        $"{userId},{firstName},{lastName},{email},{phoneNumber},{membershipDate.ToShortDateString()},{isActive}");
                }

                if (!isFound) Console.WriteLine("User Was Not Found");
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static void AddNewUser(string connectionString, ref User user)
        {
            var connection = new SqlConnection(connectionString);
            const string query =
                "INSERT INTO Users VALUES(@FirstName,@LastName,@Email,@PhoneNumber,@MembershipDate,@IsActive);SELECT SCOPE_IDENTITY();";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
            command.Parameters.AddWithValue("@MembershipDate", user.MembershipDate);
            command.Parameters.AddWithValue("@IsActive", user.IsActive);
            try
            {
                connection.Open();
                var result =
                    command.ExecuteScalar(); // Because I am using 2 SQL statements; For only Insert user command.ExecuteNonQuery();
                if (result != null &&
                    int.TryParse(result.ToString(),
                        out var insertedId)) // Then result should number of rows affected (int)
                {
                    Console.WriteLine($"User with id {insertedId} was added successfully");
                    user.UserId = insertedId;
                }
                else
                {
                    Console.WriteLine("There was error");
                }

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static void UpdateUserById(string connectionString, int id, string firstName)
        {
            var connection = new SqlConnection(connectionString);
            const string query = "UPDATE Users SET FirstName=@firstName WHERE UserID = @id";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                var result = command.ExecuteNonQuery();
                Console.WriteLine(result > 0 ? "User was updated successfully" : "there was error");
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static void DeleteUserById(string connectionString, int id)
        {
            var connection = new SqlConnection(connectionString);
            const string query = "DELETE FROM USERS WHERE USERID=@id";
            /*
             * To Delete using 'in' keyword:
             * 1.provide string comma separated list as an argument ==> DeleteUsers(connectionString,"1,2,3")
             * 2.write your query as this ==> "DELETE FROM Users WHERE UserID in (" + ContactsIDs + ")"; where ContactsIDs is "1,2,3"
             * Ps: You can't use Parameters while using 'in', so you can't do command.Parameters.AddWithValue()
             */
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                var result = command.ExecuteNonQuery();
                Console.WriteLine(result > 0 ? "User Was Deleted Successfully" : "There was Error");
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private struct User
        {
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public DateTime MembershipDate { get; set; }
            public bool IsActive { get; set; }
        }
    }
}