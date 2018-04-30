using System;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LPReportCheck
{
    public class FileReader
    {
        public FileReader()
        {
            _contents = new List<string>();
        }

        private void AddResults(string input)
        {
            _contents.Add(input);
        }

        public List<string> GetResults()
        {
            return _contents;
        }

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


        public void ReadFile(string filename)
        {            
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

                                string[] sepTables;
                                string[] sepRows;

                                sepTables = SplitStringByTable(line.Trim());
                                foreach(string table in sepTables)
                                {
                                    sepRows = SplitStringByTableRow(table);
                                    foreach (string row in sepRows)
                                    {
                                        AddResults(HTMLToCSV(row));
                                    }
                                }
                            }
                            prevLine = line;
                        }
                    }
                }
            }
        }


        private bool LineIsBlank(string line)
        {
            line.Replace(" ","");
            return line.Length == 0;
        }

      
        private string[] SplitStringByTable(string input)
        {
            string[] separators = {"</table>"};
            return input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }
         
        private string[] SplitStringByTableRow(string input)
        {
            string[] separators = { "</tr>" };
            return input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
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
        List<string> _contents;
    }
}
