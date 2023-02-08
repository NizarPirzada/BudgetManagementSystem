using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineFinanicalApplicaton.Core
{
    public class UserProcess
    {

        public int LoginUser(string connString, string Email,string Password,out string userId)
        {
            userId = "0";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            // Exception handling if code crashes 
            try
            {
                string Select = "SELECT * FROM dbo.Users WHERE Email=@Email AND Password=@Password";
                SqlCommand cmd = new SqlCommand(Select, conn);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                //If the details are found...
                if (reader.HasRows)
                {                 
                    while (reader.Read())
                    {
                        userId = reader["User_ID"].ToString();
                        return 1;
                    }                  
                }
                else
                {
                    //Otherwise tell the user that the login is invalid. - MS
                    reader.Close();                  
                }
                
                reader.Close();
                cmd.Dispose();                
            }
            catch (Exception ex)
            {
                // Show Alert here   
                return 3;
            }

            finally
            {
               
                // Finally block always run
                conn.Close();                
            }
            return 3;
        }


        public int CreateUser(string connString,string fName, string lName, string Email, string Password, out string message,out string userId)
        {
            message = "";
            userId = "0";
            //
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            try
            {
                if (CheckUser(conn, Email))
                {
                    string Insert = @"INSERT INTO dbo.Users (Forename, Surname, Email, Password) 
                              VALUES (@fname, @lname, @email, @password)";
                    SqlCommand cmd = new SqlCommand(Insert, conn);
                    cmd.Parameters.AddWithValue("@fname", fName);
                    cmd.Parameters.AddWithValue("@lname", lName);
                    cmd.Parameters.AddWithValue("@email", Email);
                    cmd.Parameters.AddWithValue("@password", Password);
                    cmd.ExecuteNonQuery();

                    userId = CheckSignupUser(conn, Email);    //Get Record to test if user created.
                    if (userId == "0")
                    {
                        message = "Could Not Create User. ";
                        return 3;   //Error
                    }
                    return 1;   // Created
                }
                else
                {
                    message = "User Already Created";
                    return 2;   //Already Exists
                }
            }
            catch (Exception ex)
            {
                message = "Could Not Create User. " + ex.Message;
                return 3;   //Error
            }
            finally
            {
                conn.Close();
            }

        }

        private bool CheckUser(SqlConnection conn, string Email)
        {
            //Check If User Already Exists via email            
            SqlCommand cmd_user_exists = new SqlCommand(@"SELECT * FROM dbo.Users WHERE Email = @email", conn);
            cmd_user_exists.Parameters.AddWithValue("@email", Email);
            SqlDataReader reader_user_exists = cmd_user_exists.ExecuteReader();
            if (reader_user_exists.HasRows)
            {
                return false;
            }
            reader_user_exists.Close();
            return true;
        }

        private string CheckSignupUser(SqlConnection conn, string Email)
        {
            //Check If User Already Exists via email           
            string userId = "0";
            SqlCommand cmd_user_exists = new SqlCommand(@"SELECT * FROM dbo.Users WHERE Email = @email", conn);
            cmd_user_exists.Parameters.AddWithValue("@email", Email);
            SqlDataReader reader_user_exists = cmd_user_exists.ExecuteReader();
            if (reader_user_exists.HasRows)
            {             
                while (reader_user_exists.Read())
                {
                    userId = reader_user_exists["User_ID"].ToString();
                }                
            }
            else
            {
                userId = "0";
            }
            reader_user_exists.Close();
            return userId;
        }
    }
}