using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSequenceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region Variable Initialization
                var input = String.Empty;
                List<string> inputArr = new List<string>();
                #endregion

                #region User guide console messages
                Console.WriteLine("Please provide input and press enter, to stop providing the input just press enter. ");
                Console.WriteLine("Sample job input format : ");
                Console.WriteLine("a=>");
                Console.WriteLine("b=>c");
                Console.WriteLine("c=>");
                Console.WriteLine("<Press Enter to end the job input>");
                #endregion

                #region Reading input from user
                while ((input = Console.ReadLine()) != "")
                {
                    inputArr.Add(input);
                }
                #endregion

                #region Display output
                Console.WriteLine(new JobEngine().GenerateJobSequence(inputArr.ToArray()).Replace("_", " "));
                Console.ReadKey();
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception - Main : " + ex.Message);
            }
        }
    }
}
