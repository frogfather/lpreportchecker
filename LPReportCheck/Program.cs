using System;

namespace LPReportCheck
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            Script myScript = new Script("TestScript",1024);
            myScript.ValueChanged += OnValueChanged;
            myScript.RecCount = 2;
            myScript.AddDashFail("1021");
            myScript.AddDashSuccess("2212");
            myScript.AddEmailFail("1232","it didn't work");
            myScript.AddEmailSuccess("2265");
            //FileReader fr = new FileReader();
            //fr.ReadFile("/home/john/Downloads/htmlLayout.txt");
            //Console.WriteLine("Result count "+fr.GetResultCount());
        }

        static void OnValueChanged(object sender,ValueChangedEventArgs args)
        {
            if (sender is Script)
            {
                Console.WriteLine($"Changed in {args.CalledBy} from {args.OldValue} to {args.NewValue}");
                //this is where we check the totals - but prob should be in the containing class - eg facility for script delegates
            }
        }
    }
}
