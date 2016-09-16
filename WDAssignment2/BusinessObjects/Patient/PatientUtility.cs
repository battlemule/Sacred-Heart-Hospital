/********************************************************************
 *  Patient Utility Class                                v1.0 11/2014
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Defines utility functions for Patient class.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WDAssignment2.Utility
{
    class PatientUtility
    {
        /************************************************************
         * GetPatients()
         * Query for and return list of all patients
         ***********************************************************/
        public static List<Patient> GetPatients()
        {
            // Get SqlConnection string and set command to
            // GetPatients stored procedure
            SqlConnection conn = new SqlConnection(
                                     Utility.GetConnectionString());
            SqlCommand comm = new SqlCommand("GetPatients", conn);
            comm.CommandType = CommandType.StoredProcedure;

            // Open SqlConnection
            conn.Open();

            // Point reader at the stored procedure results
            SqlDataReader reader = comm.ExecuteReader();

            // Instantiate return list for results
            List<Patient> patients = new List<Patient>();

            // For each line in query results
            while (reader.Read())
                // Add new patient to return list from line
                patients.Add(new Patient((int)reader["Id"], 
                                          reader["Name"].ToString(),
                                          reader["Address"].ToString(),
                                          reader["DateOfBirth"].ToString(),
                                          reader["Phone"].ToString(),
                                          reader["EmergencyContact"].ToString(),
                                          reader["DateOfRegistration"].ToString()));

            // Close connection
            conn.Close();

            // Return list of patients
            return patients;

        } // End GetPatients()

        /************************************************************
         * GetPatient(int id)
         * Query for and return bed from input id
         ***********************************************************/
        public static Patient GetPatient(int id)
        {
            // Get list of patients
            List<Patient> patients = GetPatients();
            // For each patient in list
            foreach (Patient patient in patients)
                // If patient id matches input id
                if ((int)patient.id == id)
                    // Return patient
                    return patient;
            // Return null if no match in list
            return null;
        }// End GetPatient(int id)

        /************************************************************
         * GetNewId()
         * Return an int 1 higher than the amount of patients in
         * database to use as unique id for new patient registrations
         ***********************************************************/
        public static int GetNewId()
        {
            // Get list of all patients
            List<Patient> patients = GetPatients();
            // Get size of list of all patients
            int size = patients.Count;
            // Return that size +1
            return ++size;
        } // End GetNewId()

        /************************************************************
         * AddPatient(Patient patient)
         * Add the provided patient details to database
         ***********************************************************/
        public static bool AddPatient(Patient patient)
        {
            try
            {
                // Attempt new SqlConnection with insert patient command
                SqlConnection conn = new SqlConnection(Utility.GetConnectionString());
                SqlCommand comm = new SqlCommand("INSERT INTO Patient (Name, Address, " +
                "DateOfBirth, Phone, EmergencyContact, DateOfRegistration) VALUES " +
                "(@name, @Address, @DateOfBirth, @Phone, @EmergencyContact, @DateOfRegistration)");

                comm.CommandType = CommandType.Text;
                comm.Connection = conn;

                // Add values to parameters being inserted
                comm.Parameters.AddWithValue("@Name", patient.name);
                comm.Parameters.AddWithValue("@Address", patient.address);
                comm.Parameters.AddWithValue("@DateOfBirth", patient.birthdate);
                comm.Parameters.AddWithValue("@Phone", patient.phone);
                comm.Parameters.AddWithValue("@EmergencyContact", patient.emergency);
                comm.Parameters.AddWithValue("@DateOfRegistration", patient.registration);

                // Open connection
                conn.Open();
                // Execute insert query
                comm.ExecuteNonQuery();
                // Close connection
                conn.Close();

                // Return true if no exceptions caught
                return true;
            }
            catch (Exception)
            {
                // Return false if anything went wrong
                return false;
            }
        } // End addPatient(Patient patient)

    } // End PatientUtility

} // End Namespace