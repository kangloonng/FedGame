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
        private string python_path_folder = "";
        public string database_server { get; set; }
        private static string default_directory = Environment.CurrentDirectory;
        public IOManager() 
        {
            //TODO INIT OF FILE PATHS
            try
            {
                string settingsFilepathName = "FilePathSettings.txt";
                string text = File.ReadAllText(default_directory + "\\" + settingsFilepathName);
                string[] textList = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
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
                        this.incentive_py_file_path = default_directory;
                    else
                        this.incentive_py_file_path = path;

                    break;
                case "python_path_folder":
                    this.python_path_folder = path;
                    break;
                default:
                    Console.WriteLine("Error in FilePathSettings.txt");
                    break;
            }
        }
        #region Python IO 
        public string GetFixedSettings()
        {
            //TODO move path to FilePathStrings file
            string filepath = default_directory + "\\FixedParams.txt";
            string text = File.ReadAllText(filepath);

            return text;
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
            //Console.WriteLine(default_directory);
            string pathToPython = python_path_folder;
            string path = pathToPython + ";" +
                          Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
            Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pathToPython, EnvironmentVariableTarget.Process);
            Console.WriteLine("path :"+path);
            Console.WriteLine(EnvironmentVariableTarget.Process);
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
            string workingDirectory = default_directory;
            XElement xdoc = new XElement("Settings",
                                new XElement("Environment_Settings",
                                    new XElement("FIXED_MARKET_SHARE",2000),
                                    new XElement("FIXED_STARTING_ASSET",500),
                                    new XElement("MIN_TRAINING_LENGTH",0.5),
                                    new XElement("MAX_TRAINING_LENGTH_BOUNDARY",1.5),
                                    new XElement("MIN_BID_LENGTH",0.5),
                                    new XElement("MIN_PROFIT_LENGTH",0.3),
                                    new XElement("NUM_OF_FEDERATIONS",5),
                                    new XElement("NUM_OF_CPU_PLAYERS",10),
                                    new XElement("MAX_TURNS", 10),
                                    new XElement("MIN_DATA_QUALITY",0.1),
                                    new XElement("MAX_DATA_QUALITY",0.99),
                                    new XElement("MIN_DATA_QUANTITY",0.1),
                                    new XElement("MAX_DATA_QUANTITY",1),
                                    new XElement("DATA_QUALITY_WEIGHT",0.7),
                                    new XElement("DATA_QUANTITY_WEIGHT",0.3),
                                    new XElement("MIN_RESOURCE_QUANTITY",1),
                                    new XElement("MAX_RESOURCE_QUANTITY",10),
                                    new XElement("FIXED_MARKET_SHARE_PCT",0.3)),
                                new XElement("Threshold_Settings",
                                    new XElement("INIT_DATA_QUANTITY",0.1),
                                    new XElement("INIT_DATA_QUALITY",0.1),
                                    new XElement("INIT_RESOURCE_QUANTITY",1),
                                    new XElement("INIT_AMOUNT_BID",50))
                                );

            xdoc.Save(workingDirectory + "\\Settings.xml");

        }
        public XElement GetSettings()
        {
            string workingDirectory = default_directory+"\\Settings.xml";
            try
            {
                return XElement.Load(workingDirectory);
            }
            catch(Exception e) 
            {
                Console.WriteLine("Could not find settings file! Re-initializing or re-dl from repository!");
                this.ReInitSettings();
                return XElement.Load(workingDirectory);
            }
        }

        public void CreateTextFile(string content)
        {
            string fileName = "SqlCommands.txt";
            string path = default_directory + "\\" + fileName;

            File.AppendAllLines(path, new[] { content });

        }


    }
}