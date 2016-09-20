/********************************************************************
 *  Doctor Utility Class                                 v1.2 09/2016
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Defines utility functions for Doctor class.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SacredHeartHospital.Utility
{
    class DoctorUtility
    {
        /************************************************************
         * GetDoctors()
         * Query for and return list of all Doctors
         ***********************************************************/
        public static List<Doctor> GetDoctors()
        {
            // Get SqlConnection string and set command to GetDoctors
            // stored procedure
            SqlConnection conn = new SqlConnection(Utility.GetConnectionString());
            SqlCommand comm = new SqlCommand("GetDoctors", conn);
            comm.CommandType = CommandType.StoredProcedure;

            // Open SqlConnection
            conn.Open();

            // Point reader at the stored procedure results
            SqlDataReader reader = comm.ExecuteReader();

            // Instantiate return list for results
            List<Doctor> doctors = new List<Doctor>();

            // For each line in query results
            while (reader.Read())
                // Add new doctor to return list from line
                doctors.Add(new Doctor((int)reader["Id"], 
                                       reader["Name"].ToString(),
                                       reader["Address"].ToString(), 
                                       reader["Phone"].ToString()));

            // Close connection
            conn.Close();

            // Return list of doctors
            return doctors;

        } // End GetDoctors()

        /************************************************************
         * GetDoctor(int id)
         * Query for and return doctor from input id
         ***********************************************************/
        public static Doctor GetDoctor(int id)
        {
            // Get list of doctors
            List<Doctor> doctors = GetDoctors();
            // For each doctor in list
            foreach (Doctor doctor in doctors)
                // If doctor id matches input id
                if ((int)doctor.id == id)
                    // Return doctor if so
                    return doctor;

            // Return null if no match in list
            return null;

        } // End GetDoctor(int id)

    } // End DoctorUtility

} // End Namespace