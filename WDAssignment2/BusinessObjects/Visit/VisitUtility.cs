/********************************************************************
 *  Visit Utility Class                                  v1.0 11/2014
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Defines utility functions for Visit and child classes.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WDAssignment2.Utility
{
    class VisitUtility
    {
        /************************************************************
         * GetVisits()
         * Query for and return list of all visits
         ***********************************************************/
        public static List<Visit> GetVisits()
        {
            // Get SqlConnection string and set command to GetVisits 
            // stored procedure
            SqlConnection conn = new SqlConnection(
                                     Utility.GetConnectionString());
            SqlCommand comm = new SqlCommand("GetVisits", conn);
            comm.CommandType = CommandType.StoredProcedure;

            // Open SqlConnection
            conn.Open();

            // Point reader at the stored procedure results
            SqlDataReader reader = comm.ExecuteReader();

            // Instantiate return list for results
            List<Visit> visits = new List<Visit>();

            // For each line in query results
            while (reader.Read())
            {
                // If Patient Type is InPatient
                if ((int)reader["PatientType"] == 0)
                {
                    // Discharge date
                    string discharge;
                    // If still inpatient set discharge to null string
                    if (reader["DateOfDischarge"] == DBNull.Value)
                        discharge = null;
                    // Else set discharge date
                    else
                        discharge = reader["DateOfDischarge"].ToString();

                    // Add new visit to return list from line
                    visits.Add(new InVisit((int)reader["Id"], 
                                           (int)reader["PatientId"],
                                           (int)reader["PatientType"], 
                                           (int)reader["DoctorId"],
                                           reader["DateOfVisit"].ToString(),
                                           discharge, 
                                           (int)reader["BedId"]));
                }
                // If Patient Type is OutPatient
                else
                {
                    // Add new visit to return list from line
                    visits.Add(new OutVisit((int)reader["Id"], 
                                            (int)reader["PatientId"],
                                            (int)reader["PatientType"], 
                                            (int)reader["DoctorId"],
                                            reader["DateOfVisit"].ToString(), 
                                            reader["DateOfDischarge"].ToString()));
                } // End if patient type

            } // End while

            // Close connection
            conn.Close();

            // Return list of beds
            return visits;

        } // End GetVisits()

        /************************************************************
         * GetNewId()
         * Generate a new visit id 1 greater than total amount of
         * visits.
         ***********************************************************/
        public static int GetNewId()
        {
            // Return int
            int count = 0;
            // Get list of all visits
            List<Visit> visits = GetVisits();
            // For each item in visits
            foreach (Visit visit in visits)
                // Increment return int
                count++;

            // Return 1 greater than count of visits
            return ++count;

        } // End GetNewId()

        /************************************************************
         * AddVisit(InVisit visit)
         * Add provided InVisit to database
         ***********************************************************/
        public static bool AddVisit(InVisit visit)
        {
            try
            {
                // Attempt new SqlConnection with insert visit command
                SqlConnection conn = new SqlConnection(Utility.GetConnectionString());
                SqlCommand comm = new SqlCommand("INSERT INTO Visit (PatientId, PatientType, " +
                "DoctorId, BedId, DateOfVisit, DateOfDischarge) VALUES (@PatientId, " +
                "@PatientType, @DoctorId, @BedId, @DateOfVisit, @DateOfDischarge)");

                comm.CommandType = CommandType.Text;
                comm.Connection = conn;

                // Add values to parameters being inserted
                comm.Parameters.AddWithValue("@PatientId", visit.patientId);
                comm.Parameters.AddWithValue("@PatientType", visit.type);
                comm.Parameters.AddWithValue("@DoctorId", visit.doctor);
                comm.Parameters.AddWithValue("@BedId", visit.bed);
                comm.Parameters.AddWithValue("@DateOfVisit", visit.date);
                comm.Parameters.AddWithValue("@DateOfDischarge", DBNull.Value);

                // Open connection
                conn.Open();
                // Execute insert query
                comm.ExecuteNonQuery();
                // Close connection
                conn.Close();

                // Return true if successfull
                return true;
            }
            catch (Exception)
            {
                // If exception caught return false
                return false;
            }

        } // End AddVisit(InVisit visit)

        /************************************************************
         * AddVisit(OutVisit visit)
         * Add provided OutVisit to database
         ***********************************************************/
        public static bool AddVisit(OutVisit visit)
        {
            try
            {
                // Attempt new SqlConnection with insert visit command
                SqlConnection conn = new SqlConnection(Utility.GetConnectionString());
                SqlCommand comm = new SqlCommand("INSERT INTO Visit (PatientId, PatientType, " +
                    "DoctorId, DateOfVisit, DateOfDischarge) VALUES (@PatientId, @PatientType, " +
                    "@DoctorId, @DateOfVisit, @DateOfDischarge)");

                comm.CommandType = CommandType.Text;
                comm.Connection = conn;

                // Add values to parameters being inserted
                comm.Parameters.AddWithValue("@PatientId", visit.patientId);
                comm.Parameters.AddWithValue("@PatientType", visit.type);
                comm.Parameters.AddWithValue("@DoctorId", visit.doctor);
                comm.Parameters.AddWithValue("@DateOfVisit", visit.date);
                comm.Parameters.AddWithValue("@DateOfDischarge", visit.discharge);

                // Open Connection
                conn.Open();
                // Execute insert query
                comm.ExecuteNonQuery();
                // Close connection
                conn.Close();

                // Return true if successfull
                return true;

            }
            catch (Exception)
            {
                // If exception caught return false
                return false;
            }

        } // End AddVisit(OutVisit visit)

        /************************************************************
         * UpdateVisit(InVisit visit)
         * Add provided InVisit to database
         ***********************************************************/
        public static bool UpdateVisit(InVisit visit)
        {
            try
            {
                // Attempt new SqlConnection with update visit command
                SqlConnection conn = new SqlConnection(Utility.GetConnectionString());
                SqlCommand comm = new SqlCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = "UPDATE Visit SET [DateOfDischarge]" + 
                                   " = @DateOfDischarge WHERE [Id] = @Id";

                // Add values to parameters being inserted
                comm.Parameters.AddWithValue("@DateOfDischarge", visit.discharge);
                comm.Parameters.AddWithValue("@id", visit.id);
                comm.Connection = conn;

                // Open connection
                conn.Open();
                // Execute Update Query
                comm.ExecuteNonQuery();
                // Close connection
                conn.Close();

                // Return true if successfull
                return true;

            }
            catch (Exception)
            {
                // If exception caught return false
                return false;
            }

        } // End UpdateVisit(InVisit visit)

        /************************************************************
         * GetPrice(InVisit visit)
         * Get total price of inpatient visit
         ***********************************************************/
        public static string GetPrice(InVisit visit)
        {
            // Start date split holding
            string[] startSplit = visit.date.Split(' ');
            // End date split holding
            string[] endSplit = DateTime.Now.ToString().Split(' ');
            // Start date
            string[] visitStart = startSplit[0].Split('/');
            // End date
            string[] visitEnd = endSplit[0].Split('/');

            // Create DateTime object from start time
            DateTime start = new DateTime(Convert.ToInt32(visitStart[2]), 
                                          Convert.ToInt32(visitStart[1]), 
                                          Convert.ToInt32(visitStart[0]));
            // Create DateTime object from end time
            DateTime end = new DateTime(Convert.ToInt32(visitEnd[2]), 
                                        Convert.ToInt32(visitEnd[1]), 
                                        Convert.ToInt32(visitEnd[0]));

            // Total amount of days of visit
            int days = (end - start).Days;
            // Price of bed per day
            int rate = BedUtility.GetBed(Convert.ToInt32(visit.bed)).rate;
            
            // Double holding for bed rate * days of visit
            double priceDouble = rate * days;

            // If the visit was only 1 day
            if (priceDouble == 0)
                // The rate of the bed is the price
                return rate.ToString();
            else
                // Else return priceDouble as price
                return priceDouble.ToString();

        } // End GetPrice(InVisit visit)

        /************************************************************
         * GetVisit(int id)
         * Return visit (in or out) with specified id
         ***********************************************************/
        public static Visit GetVisit(int id)
        {
            // Get list of visits
            List<Visit> visits = GetVisits();

            // For each item in list
            foreach (Visit visit in visits)
                // If id is equal to provided id
                if (visit.id == id)
                    // Return visit
                    return visit;

            // Return null if not found
            return null;

        } // End GetVisit(int id)

        /************************************************************
         * GetOutVisit(int id)
         * Return outvisit with specified id
         ***********************************************************/
        public static OutVisit GetOutVisit(int id)
        {
            // Get list of visits
            List<Visit> visits = GetVisits();

            // For each item in list
            foreach (Visit visit in visits)
                // If item is an OutVisit
                if (visit is OutVisit)
                    // If id is equal to provided id
                    if (visit.id == id)
                        // Return visit as outvisit
                        return visit as OutVisit;

            // Return null if not found
            return null;

        } // End GetOutVisit(int id)

        /************************************************************
         * GetInVisit(int id)
         * Return invisit with specified id
         ***********************************************************/
        public static InVisit GetInVisit(int id)
        {
            // Get list of visits
            List<Visit> visits = GetVisits();

            // For each item in list
            foreach (Visit visit in visits)
                // If item is an invisit
                if (visit is InVisit)
                    // If id is equal to provided id
                    if (visit.id == id)
                        // Return visit as an invisit
                        return visit as InVisit;

            // If null if not found
            return null;

        } // End GetInVisit(int id)

    } // End Utility

} // End Namespace