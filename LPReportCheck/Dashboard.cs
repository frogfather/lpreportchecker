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

        public void ProcessResults(List<string> results, string facilityName, bool success)
        {
            //first we need to add the facility
            if (!FacilityExists(facilityName))
            {
                AddFacility(facilityName);
            }
            //now we need to find the script names
            foreach(string result in results)
            {
                if (result.Contains("Service Code"))
                {
                    string[] separators = { "," };
                    string[] sepItems = result.Split(separators,StringSplitOptions.RemoveEmptyEntries);
                    int scPos = -1; //service code position
                    int fmPos = -1; //fail message position
                    scPos = Array.IndexOf(sepItems, "Service Code");
                    fmPos = Array.IndexOf(sepItems, "Reason for error");
                }
                //now we can find the data
            }

        }

        public bool FacilityExists(string facilityName)
        {
            return facilities.Find(x => x.Name == facilityName) != null;
        }

        public void AddFacility(string facilityName)
        {
            facilities.Add(new Facility(facilityName));
        }

        public Facility GetFacility(string facilityName)
        {
            return facilities.Find(x => x.Name == facilityName);    
        }


        List<Facility> facilities;    
    }
}
