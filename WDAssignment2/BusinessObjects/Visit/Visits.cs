/********************************************************************
 *  Visit Class - Abstract                               v1.0 11/2014
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Definition of  abstract class Visit. Child classes InVisit and
 *  OutVisit. One visit instantiated for each visit registered to
 *  the hospital.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDAssignment2.Utility
{

    // Visit Abstract Class Definition
    abstract class Visit
    {
        // Class Variables
        // Unique visit id for each visit in system
        public int id { get; private set; }
        // Unique patient id that attended visit
        public int patientId { get; private set; }
        // Visit type - in or out
        public int type { get; private set; }
        // Unique doctor id that attended visit
        public int doctor { get; private set; }
        // Date visit occurred
        public string date { get; private set; }
        // Date visit ended
        public string discharge { get; private set; }

        // Constructor
        public Visit(int id, int patientId, int type,
            int doctor, string date, string discharge)
        {
            this.id = id;
            this.patientId = patientId;
            this.type = type;
            this.doctor = doctor;
            this.date = date;
            this.discharge = discharge;
        }

    }

}