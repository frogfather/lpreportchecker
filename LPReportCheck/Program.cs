﻿using System;
using System.Collections.Specialized;

namespace LPReportCheck
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            //Script myScript = new Script("TestScript",1024);
            //myScript.ValueChanged += OnValueChanged;
            //myScript.MatchChanged += OnMatchChanged;
            //myScript.RecCount = 2;
            //myScript.AddDashFail("1021");
            //myScript.AddDashFail("1121");
            //myScript.AddDashSuccess("2212");
            //myScript.AddEmailFail("1121","Something not very bad happened");
            //myScript.AddEmailFail("1021","ISM: Something awful happened");
            //myScript.AddEmailSuccess("2212");
            //Facility myFacility = new Facility("GrottyHospital", 2059);
            //myFacility.AddScript(myScript);
            //String testscriptName;
            //testscriptName = "TestCabbage";
            //Script foundScript = myFacility.GetScript(testscriptName);
            //if (foundScript != null)
            //{
            //    Console.WriteLine("Looking for "+testscriptName+" found "+foundScript.Name);    
            //}
            //else
            //{
            //    Console.WriteLine("Looking for " + testscriptName + " found nothing");
            //}

            //main method creates dashboard
            Dashboard mainDashboard = new Dashboard();
            mainDashboard.AddFacility("TestFacility", 2012);
            mainDashboard.GetFacility("TestFacility").AddScript("TestScript");
            if (mainDashboard.GetFacility("TestFacility")!=null && mainDashboard.GetFacility("TestFacility").GetScript("TestScript")!=null)
            {
                Console.WriteLine(mainDashboard.GetFacility("TestFacility").GetScript("TestScript").Name);    
            }
            else
            {
                Console.WriteLine("No such object");
            }



            //FileReader fr = new FileReader();
            //String fileContents = fr.ReadFile("/home/john/Downloads/htmlLayout.txt");
            //bool scriptSuccess = fr.Success;
            //string facilityName = fr.FacilityName;
        }



        static void OnValueChanged(object sender,ValueChangedEventArgs args)
        {
            if (sender is Script)
            {
                Console.WriteLine($"Changed {args.CalledBy} from {args.OldValue} to {args.NewValue}");
                //this is where we check the totals - but prob should be in the containing class - eg facility for script delegates
            }
        }

        static void OnMatchChanged(object sender, MatchEventArgs args)
        {
            if (sender is Script)
            {
                Console.WriteLine($"{args.CalledBy} match value is {args.Value} ");
            }
        }        

    }
}
