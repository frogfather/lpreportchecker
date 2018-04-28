using System;
using System.Collections.Generic;
namespace LPReportCheck
{
    public class Facility
    {
        public Facility(string facName, int facServer)

        {
            name = facName;
            server = facServer;
            successCount = 0;
            failCount = 0;
            scripts = new List<Script>();
        }

        public void AddScript(Script script)
        {
            scripts.Add(script);
        }

        public Script GetScript(string scriptName)
        {
            return scripts.Find(x => x.Name == scriptName);            
        }

        //to add check dash success total == email success total, check dash fail total == email fail total, check dash success codes == email success codes, check dash fail codes == email fail codes, check error messages for ISM errors
        public string name { get; set; }

        public int server { get; set; }

        public int coid { get; set; }

        public string pas { get; set; }

        public int facId { get; set; }

        public string scriptingStart { get; set; }

        public string scriptingEnd { get; set; }

        public int successCount { get; set; }

        public int failCount { get; set; }

        public event ValueChangedDelegate ValueChanged;
        List<Script> scripts;


    }
}
