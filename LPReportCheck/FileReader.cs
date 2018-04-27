using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace LPReportCheck
{
    public class FileReader
    {
        public FileReader()
        {
            _results = new List<string>();           
        }

        private void AddToResults(string item)
        {
            _results.Add(item);
        }

        public void ReadFile(string filename)
        {
            //this should read the facility batch header line, facility results header and facility batch content 
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
                            _facilityName = HTMLToCSV(line);
                            Console.WriteLine(_facilityName);
                        }
                        if (prevLine.Contains("<div id=resultsHeader>"))
                        {
                            _resultsHeader = HTMLToCSV(line);
                            Console.WriteLine(_resultsHeader);
                        }

                        if (line.Contains("<div id=facilityBatchContent>"))
                        {
                            ProcessContent(line);
                        }
                        prevLine = line;
                    }
                }

            }
        }

        private void IncrementCounter()
        {
            _itemCount += 1;
        }

        private void ResetCounter()
        {
            _itemCount = 0;
        }

        public int GetResultCount()
        {
            return _itemCount;
        }

        public void CountResults()
        {
            ResetCounter();
            bool onResults = false;
            foreach(string item in _results)
            {                
                if (item.Contains("Service Code") || item.Contains("Script Name:")) { onResults = false; }//don't want to count this record but ones following. 
                if (onResults){IncrementCounter();}
                if (item.Contains("Service Code")) { onResults = true; }//so the next record will be counted
            }
        }

        private void ProcessContent(string batchContent)
        {
            String[] allResults;
            String[] singleResult;
            if (batchContent.Length == 0) { return ; }
            batchContent = batchContent.Trim();
            allResults = Regex.Split(batchContent.Trim(),"</table>"); //gives individual script results
            foreach(string line in allResults)
            {
                singleResult = Regex.Split(line, "</tr>");
                foreach (string rline in singleResult)
                {                    
                    string trimmed = HTMLToCSV(rline);
                    if (trimmed.Length > 0) 
                    {
                        AddToResults(trimmed);
                        Console.WriteLine(trimmed);
                    }
                }

            }
            CountResults();
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

        bool _success;
        List<string> _results;
        int _itemCount;
        String _facilityName;
        String _resultsHeader;
    }
}
