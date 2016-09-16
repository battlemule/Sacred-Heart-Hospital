/********************************************************************
 *  InVisit Class                                        v1.0 11/2014
 *  Sacred Heart Hospital                               Robert Willis
 *  
 *  Parent Class Visit. One instance created for each inpatient
 *  visit.
 *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDAssignment2.Utility
{
    // Invisit definition - Parent Visit
    class InVisit : Visit
    {
        // Class Variables
        // Bed ID of inpatient visit
        public int bed { get; private set; }

        // Constructor
        // Bed set by constructor, remaining variables 
        // constructed by super
        public InVisit(int id, int patientId, int type, int doctor,
            string date, string discharge, int bed) :
            base(id, patientId, type, doctor, date, discharge)
        {
            this.bed = bed;
        }

    }

}