using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Utils
{
    public class Logger
    {
        public Logger() { }
        public void PrintInit(string initTarget)
        {
            Console.WriteLine("------Init {0}------",initTarget);
        }
        public void PrintConsoleMessage(string msg)
        {
            Console.WriteLine(msg);
        }
        public void PrintConsoleMessage(string target, string action)
        {
            Console.WriteLine("------{0} {1}------", target,action);
        }
    }
}
