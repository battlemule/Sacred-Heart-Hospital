/********************************************************************
 *  Utility Class                                        v1.2 09/2016
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Defines utility functions for Sacred Heart Hospital system.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WDAssignment2.Utility
{
    public static class Utility
    {
        /************************************************************
         * GetConnectionString()
         * Return ConnectionString
         ***********************************************************/
        public static string GetConnectionString()
        {
            return System.Web.Configuration.WebConfigurationManager.
                   ConnectionStrings["SacredHeartConnectionString"].
                   ToString();
        }
    }
}