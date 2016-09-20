/********************************************************************
 *  OutVisit Class                                       v1.2 09/2016
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Parent Class Visit. One instance created for each outpatient
 *  visit.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacredHeartHospital.Utility
{

    // Outvisit definition - Parent visit
    class OutVisit : Visit
    {
        // Class Variables
        public string bed { get; private set; }

        // Constructor
        // Bed set by constructor, remaining variables 
        // constructed by super
        public OutVisit(int id, int patientId, int type,
            int doctor, string date, string discharge) :
            base(id, patientId, type, doctor, date, discharge)
        {
            // OutVisit patients not assigned a bed
            this.bed = "Null";
        }

    }

}