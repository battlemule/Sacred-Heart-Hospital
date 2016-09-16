/********************************************************************
 *  User Utility Class                                   v1.0 11/2014
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Defines utility functions for User class.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WDAssignment2.Utility
{
    class UserUtility
    {
        /************************************************************
         * GetUsers()
         * Query for and return list of all users
         ***********************************************************/
        public static List<User> GetUsers()
        {
            // Get SqlConnection string and set command to GetBeds 
            // stored procedure
            SqlConnection conn = new SqlConnection(
                                      Utility.GetConnectionString());
            SqlCommand comm = new SqlCommand("GetStaff", conn);
            comm.CommandType = CommandType.StoredProcedure;

            // Open SqlConnection
            conn.Open();

            // Point reader at the stored procedure results
            SqlDataReader reader = comm.ExecuteReader();

            // Instantiate return list for results
            List<User> users = new List<User>();

            // For each line in query results
            while (reader.Read())
                // Add new bed to return list from line
                users.Add(new User((int)reader["Id"], 
                                    reader["Username"].ToString(),
                                    reader["Password"].ToString()));

            // Close connection
            conn.Close();

            // Return list of beds
            return users;

        } // End GetBeds()

    } // End UserUtility

} // End Namespace