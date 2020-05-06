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
        private string incentive_py_file_path = "";
        public string database_server { get; set; }
        public IOManager() 
        {
            //TODO INIT OF FILE PATHS
            try
            {
                string settingsFilepathName = "FilePathSettings.txt";
                string text = File.ReadAllText(Environment.CurrentDirectory + "\\" + settingsFilepathName);
                string[] textList = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach(string s in textList)
                {
                    Console.WriteLine(s);
                    if(s!="")
                        this.assignVariables(s);
                }
                int i = 0;
            }
            catch(Exception e)
            {
                Console.WriteLine("FilePathSettings.txt not found in \\bin\\Debug " +
                    "\nPlease ensure that text file is in the path");
                Environment.Exit(0);
            }
        }

        private void assignVariables(string text)
        {
            string[] textSplit = text.Split('=');
            string header = textSplit[0], path = textSplit[1].Replace("\"","");
            Console.WriteLine(path);
            switch (header)
            {
                case "incentive_py_file_path":                    
                    if (path.Equals("default"))
                        this.incentive_py_file_path = Environment.CurrentDirectory;
                    else
                        this.incentive_py_file_path = path;

                    break;
                default:
                    Console.WriteLine("Error in FilePathSettings.txt");
                    break;
            }
        }
        #region Python IO tbi
        public List<string> GetStringSchemeTypes()
        {
            List<string> schemeTypeList = new List<string>();

            string source = @"~\Schemes";
            string[] files = Directory.GetFiles(source, "*.cs");
            Console.WriteLine(files[0]);

            return schemeTypeList;
        }

        public PythonInterface LoadPythonModules()
        {
            var obj = this.importDynamicModules();
            if (obj == null)
                return null;

            PythonInterface interface_python = new PythonInterface(obj);
            return interface_python;
        }
        public dynamic importDynamicModules()
        {
            // Setup working directory for python files
            string pathFolder = this.incentive_py_file_path;
            string pathToPython = @"H:\FYP Current\DemoXMLPython\bin\Debug";
            string path = pathToPython + ";" +
                          Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
            Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pathToPython, EnvironmentVariableTarget.Process);
            //string[] fileNamearray = null;
            //s
            //try
            //{
            //    fileNamearray = Directory.GetFiles(pathFolder, "*Schemes.py");
            //}catch(DirectoryNotFoundException e)
            //{
            //    Console.WriteLine(e.ToString());
            //    ApplicationLogic.Simulation.Instance.EXIT_FLAG = -1;
            //    return null;
            //}
            //string[] fileNamearray = Directory.GetFiles(pathFolder, "Test.py");

            var lib = new[]
            {
                pathFolder,
                Path.Combine(pathToPython, "Lib"),
                Path.Combine(pathToPython, "DLLs")
            };
            dynamic loadedModule = null;
            string paths = string.Join(";", lib);
            Environment.SetEnvironmentVariable("PYTHONPATH", null, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", paths, EnvironmentVariableTarget.Process);
            using (Py.GIL())
            {
                //string moduleName = Path.GetFileName(this.incentive_py_file_path + "IncentiveSchemes.py");
                //foreach (string s in fileNamearray)
                //{
                //    string moduleName = Path.GetFileName(s).Replace(".py", "");
                //    //moduleName = moduleName.Replace("_", "");
                //    loadedModule = Py.Import(moduleName);
                //    Console.WriteLine("Loaded "+moduleName);
                //}
                try
                {
                    loadedModule = Py.Import("IncentiveSchemes");
                    Console.WriteLine("Loaded " + loadedModule);
                }catch(Exception e)
                {
                    Console.WriteLine("Exception caught " + e.ToString());
                    Environment.Exit(0);
                }
            }

            return loadedModule;
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

        public void CreateTextFile(string content)
        {
            string fileName = "SqlCommands.txt";
            string path = Environment.CurrentDirectory + "\\" + fileName;

            File.AppendAllLines(path, new[] { content });

        }

        public  string GetFixedSettings()
        {
            //TODO move path to FilePathStrings file
            string filepath = @"H:\FYP Current\FYP_IncentiveMechanismSimulatorMVP\bin\Debug\FixedParams.txt";
            string text = File.ReadAllText(filepath);

            return text;
        }
    }
}