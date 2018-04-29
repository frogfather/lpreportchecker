using System;
using System.Collections.Generic;

namespace LPReportCheck
{
    public class Dashboard
    {
        public Dashboard()
        {
            facilities = new List<Facility>();

        }

        public void AddFacility(string facilityName)
        {
            facilities.Add(new Facility(facilityName));
        }

        public Facility GetFacility(string facilityName)
        {
            Facility result = facilities.Find(x => x.Name == facilityName);
            if (result == null)
            {
                AddFacility(facilityName);   
            }
            return facilities.Find(x => x.Name == facilityName);    
        }


        List<Facility> facilities;    
    }
}
