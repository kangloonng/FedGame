using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
using System.IO;

namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class StrategyManager
    {
        public List<Strategy> StrategyList { get; set; }
        public List<dynamic> dynamicObjList { get; set; }
        public StrategyManager()
        {
            this.StrategyList = new List<Strategy>();
            this.dynamicObjList = new List<dynamic>();
            this.LoadStrategies();
        }
        public void LoadStrategies()
        {
            Console.WriteLine("Loading Strategies ......");
            Utils.IOManager tempIO = new Utils.IOManager();
            List<string> schemeTypeList = new List<string>();

            string source = @"..\..\Strategies";
            string[] files = Directory.GetFiles(source, "*.cs");

            for (int i = 0; i < files.Length; i++)
            {
                schemeTypeList.Add(Path.GetFileName(files[i]));
            }

            int count = 0;
            string schemeDirectoryNamespace = "FYP_IncentiveMechanismSimulatorMVP.Strategies";
            foreach (string s in schemeTypeList)
            {
                string fullNamespace = schemeDirectoryNamespace + "." + s.Replace(".cs", "");
                var obj = Activator.CreateInstance(null, fullNamespace);
                Strategy strategyObj = (Strategy)obj.Unwrap();
                strategyObj.StrategyId = count;
                strategyObj.StrategyName = s.Replace(".cs", "");
                Console.WriteLine("Creating instance of {0}", s.Replace(".cs", ""));
                this.StrategyList.Add(strategyObj);
                count++;
            }

        }
    }
}
