using System;

namespace LPReportCheck
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            FileReader fr = new FileReader();
            fr.ReadFile("/home/john/Downloads/htmlLayout.txt");
            Console.WriteLine("Result count "+fr.GetResultCount());
        }
    }
}
