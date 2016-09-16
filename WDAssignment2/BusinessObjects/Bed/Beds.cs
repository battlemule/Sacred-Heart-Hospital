/********************************************************************
 *  Bed Class                                            v1.0 11/2014
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Definition of Bed class. Created and stored one instance for
 *  each bed in the hospital.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDAssignment2.Utility
{
    // Bed Definition
    class Bed
    {
        // Class Variables
        // Unique ID of bed
        public int id { get; private set; }
        // Bed Name
        public string name { get; private set; }
        // Price per day
        public int rate { get; private set; }
        // Type of bed
        public string type { get; private set; }

        // Constructor
        public Bed(int id, string name, int rate, string type)
        {
            this.id = id;
            this.name = name;
            this.rate = rate;
            this.type = type;
        } // End Constructor

    } // End Class Definition

} // End Namespace