/********************************************************************
 *  User Class                                           v1.2 09/2016
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Definition of User class. Created and stored one instance for
 *  each staff member of the hospital.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDAssignment2.Utility
{

    // User Definition
    class User
    {
        // Class variables
        // Unique ID of user
        public int id { get; private set; }
        // Username
        public string username { get; private set; }
        // Password
        public string password { get; private set; }

        // Constructor
        public User(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        } // End Constructor

    } // End User Definition

} // End Namespace