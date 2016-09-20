/********************************************************************
 *  Patient Class                                        v1.2 09/2016
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Definition of Patient class. Created and stored one instance for
 *  each patient in the hospital.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDAssignment2.Utility
{

    // Patient Definition
    class Patient
    {
        // Class Variables
        // Unique ID
        public int id { get; private set; }
        // Name
        public string name { get; private set; }
        // Address
        public string address { get; private set; }
        // Birthdate
        public string birthdate { get; private set; }
        // Phone
        public string phone { get; private set; }
        // Emergency Contact
        public string emergency { get; private set; }
        // Registration Date
        public string registration { get; private set; }

        // Constructor
        public Patient(int id, string name, string address, 
                       string birthdate, string phone, 
                       string emergency, string registration)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.birthdate = birthdate;
            this.phone = phone;
            this.emergency = emergency;
            this.registration = registration;
        } // End Constructor

    } // End Definition

} // End Namespace