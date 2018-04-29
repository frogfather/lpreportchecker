using System;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LPReportCheck
{
    public class FileReader
    {
        public string FacilityName
        {
            get
            {
                return _facilityName;
            }

            set
            {
                _facilityName = value;
            }
        }

        public bool Success
        {
            get
            {
                return _success;
            }
            set
            {
                _success = value;
            }
        }

        public String ReadFile(string filename)
        {
            string results = "";
            if (File.Exists(filename)) 
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    string prevLine = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!LineIsBlank(line))
                        {
                            if (prevLine.Contains("<div id=facilityBatchHeader>"))
                            {
                                FacilityName = HTMLToCSV(line);
                            }
                            if (prevLine.Contains("<div id=resultsHeader>"))
                            {
                                line = HTMLToCSV(line);
                                Success = (line.Contains("Success"));
                            }
                            if (line.Contains("<div id=facilityBatchContent>"))
                            {
                                results = (HTMLToCSV(line));
                            }
                            prevLine = line;
                        }
                    }
                }
            }
            return results;
        }


        private bool LineIsBlank(string line)
        {
            line.Replace(" ","");
            return line.Length == 0;
        }

      
        private string HTMLToCSV(string input)
        {
            string outputString="";
            int nextOpeningTagPos=0;
            int nextClosingTagPos=0;
            if (input.IndexOf(">", 0,StringComparison.Ordinal) == -1) { return input; }
            input = input.Trim();
            while (nextClosingTagPos > -1 && nextOpeningTagPos > -1)
            {
                nextOpeningTagPos = input.IndexOf("<", nextClosingTagPos,StringComparison.Ordinal);
                if (nextOpeningTagPos - nextClosingTagPos > 1)
                {
                    if (outputString.Length > 0 ){outputString +=", ";}
                    outputString += input.Substring(nextClosingTagPos + 1, nextOpeningTagPos - (nextClosingTagPos+1));
                }
                if (nextOpeningTagPos > -1)
                {
                    nextClosingTagPos = input.IndexOf(">", nextOpeningTagPos, StringComparison.Ordinal);    
                }
            }
            return outputString;               
        }

        String _facilityName;
        bool _success;
    }
}
