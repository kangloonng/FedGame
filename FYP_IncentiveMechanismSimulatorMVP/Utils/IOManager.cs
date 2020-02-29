using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic;
using Python.Runtime;

namespace FYP_IncentiveMechanismSimulatorMVP.Utils
{
    public class IOManager
    {
        public IOManager() { }
        #region Python IO tbi
        public List<string> GetStringSchemeTypes()
        {
            List<string> schemeTypeList = new List<string>();

            string source = @"~\Schemes";
            string[] files = Directory.GetFiles(source, "*.cs");
            Console.WriteLine(files[0]);

            return schemeTypeList;
        }

        public void LoadPythonModules(SchemeManager schemeManager, StrategyManager strategyManager)
        {
            List<dynamic> tempList2 = importDynamicModules("Scheme", @"..\..\PythonFiles");
            foreach (dynamic d in tempList2)
            {
                var p = d.scheme("test");
                Console.WriteLine("Results :" + p.whatIsName());
            }
        }
        public static List<dynamic> importDynamicModules(string keyword, string pathFolder)
        {
            List<dynamic> tempList = new List<dynamic>();
            // Setup all paths before initializing Python engine
            string pathToPython = @"H:\FYP Current\FYP_IncentiveMechanismSimulatorMVP\PythonRuntime";
            string path = pathToPython + ";" +
                          Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
            Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pathToPython, EnvironmentVariableTarget.Process);

            string[] fileNamearray = Directory.GetFiles(pathFolder, "*" + keyword + ".py");

            var lib = new[]
{
                pathFolder,
                Path.Combine(pathToPython, "Lib"),
                Path.Combine(pathToPython, "DLLs")
            };

            string paths = string.Join(";", lib);
            Environment.SetEnvironmentVariable("PYTHONPATH", null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", paths, EnvironmentVariableTarget.Process);
            using (Py.GIL())
            {
                foreach (string s in fileNamearray)
                {
                    string moduleName = Path.GetFileName(s).Replace(".py", "");
                    dynamic sampleModule = Py.Import(moduleName);

                    tempList.Add(sampleModule);
                }
            }


            return tempList;
        }
        #endregion 
        public void ReInitSettings()
        {         
            string workingDirectory = Environment.CurrentDirectory;
            XElement xdoc = new XElement("Settings",
                                new XElement("Environment_Settings",
                                    new XElement("FIXED_MARKET_SHARE",2000),
                                    new XElement("FIXED_STARTING_ASSET",500),
                                    new XElement("MIN_TRAINING_LENGTH",0.5),
                                    new XElement("MAX_TRAINING_LENGTH_BOUNDARY",1.5),
                                    new XElement("MIN_BID_LENGTH",0.5),
                                    new XElement("MIN_PROFIT_LENGTH",0.3),
                                    new XElement("NUM_OF_FEDERATIONS",3),
                                    new XElement("NUM_OF_CPU_PLAYERS",0),
                                    new XElement("MAX_TURNS", 10),
                                    new XElement("MIN_DATA_QUALITY",0.1),
                                    new XElement("MAX_DATA_QUALITY",0.99),
                                    new XElement("MIN_DATA_QUANTITY",0.1),
                                    new XElement("MAX_DATA_QUANTITY",1),
                                    new XElement("DATA_QUALITY_WEIGHT",0.7),
                                    new XElement("DATA_QUANTITY_WEIGHT",0.3),
                                    new XElement("MIN_RESOURCE_QUANTITY",1),
                                    new XElement("MAX_RESOURCE_QUANTITY",10),
                                    new XElement("FIXED_MARKET_SHARE_PCT",0.1)),
                                new XElement("Threshold_Settings",
                                    new XElement("INIT_DATA_QUANTITY",0.1),
                                    new XElement("INIT_DATA_QUALITY",0.1),
                                    new XElement("INIT_RESOURCE_QUANTITY",1),
                                    new XElement("INIT_AMOUNT_BID",10))
                                );

            xdoc.Save(workingDirectory + "\\Settings.xml");

        }
        public XElement GetSettings()
        {
            string workingDirectory = Environment.CurrentDirectory+"\\Settings.xml";
            return XElement.Load(workingDirectory);
        }
    }
}