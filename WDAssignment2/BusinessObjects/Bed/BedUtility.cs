/********************************************************************
 *  Bed Utility Class                                    v1.0 11/2014
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Defines utility functions for Bed class.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WDAssignment2.Utility
{
    class BedUtility
    {
        /************************************************************
         * GetBeds()
         * Query for and return list of all beds
         ***********************************************************/
        public static List<Bed> GetBeds()
        {
            // Get SqlConnection string and set command to GetBeds 
            // stored procedure
            SqlConnection conn = new SqlConnection(
                                     Utility.GetConnectionString());
            SqlCommand comm = new SqlCommand("GetBeds", conn);
            comm.CommandType = CommandType.StoredProcedure;

            // Open SqlConnection
            conn.Open();

            // Point reader at the stored procedure results
            SqlDataReader reader = comm.ExecuteReader();

            // Instantiate return list for results
            List<Bed> beds = new List<Bed>();

            // For each line in query results
            while (reader.Read())
                // Add new bed to return list from line
                beds.Add(new Bed((int)reader["Id"], 
                                 reader["BedName"].ToString(),
                                 (int)reader["RatePerDay"],
                                 reader["BedType"].ToString()));

            // Close connection
            conn.Close();

            // Return list of beds
            return beds;

        } // End GetBeds()

        /************************************************************
         * GetBed(int id)
         * Query for and return bed from input id
         ***********************************************************/
        public static Bed GetBed(int id)
        {
            // Get list of beds
            List<Bed> beds = GetBeds();
            // For each bed in list
            foreach (Bed bed in beds)
                // If bed id matches input id
                if ((int)bed.id == id)
                    // Return bed
                    return bed;

            // Return null if no match in list
            return null;
        } // End GetBed(int id)

    } // End BedUtility

} // End Namespace