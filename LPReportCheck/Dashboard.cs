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

        public Facility GetFacility(string facilityName)
        {
            return facilities.Find(x => x.Name == facilityName);
        }

        public void AddFacility(string facilityName, int facilityServer)
        {
            facilities.Add(new Facility(facilityName, facilityServer));
        }


        List<Facility> facilities;    
    }
}
