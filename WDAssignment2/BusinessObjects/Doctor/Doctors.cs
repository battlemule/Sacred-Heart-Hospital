/********************************************************************
 *  Doctor Class                                         v1.2 09/2016
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Definition of Doctor class. Created and stored one instance for
 *  each doctor in the hospital.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDAssignment2.Utility
{

    // Doctor Definition
    class Doctor
    {
        // Class Variables
        // Unique ID
        public int id { get; private set; }
        // Name
        public string name { get; private set; }
        // Address
        public string address { get; private set; }
        // Phone
        public string phone { get; private set; }

        // Constructor
        public Doctor(int id, string name, 
                      string address, string phone)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.phone = phone;
        } // End Constructor

    } // End Definition

}// End Namespace