using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class SchemeManager
    {
        public List<Scheme> SchemeList { get; set; }
        public List<dynamic> dynamicObjectList { get; set; }

        public SchemeManager()
        {
            this.SchemeList = new List<Scheme>();
            this.dynamicObjectList = new List<dynamic>();
            this.LoadSchemes();
        }

        public void LoadSchemes()
        {
            Console.WriteLine("Loading Schemes ......");
            Utils.IOManager tempIO = new Utils.IOManager();
            List<string> schemeTypeList = new List<string>();

            string source = @"..\..\Schemes";
            Console.WriteLine(source);
            string[] files = Directory.GetFiles(source, "*.cs");
            
            for(int i=0; i < files.Length; i++)
            {
                schemeTypeList.Add(Path.GetFileName(files[i]));
            }

            int count = 0;
            string schemeDirectoryNamespace = "FYP_IncentiveMechanismSimulatorMVP.Schemes";
            foreach (string s in schemeTypeList)
            {
                string fullNamespace = schemeDirectoryNamespace + "." + s.Replace(".cs", "");
                var obj = Activator.CreateInstance(null, fullNamespace);
                Scheme schemeObj = (Scheme) obj.Unwrap();
                schemeObj.SchemeId = count;
                schemeObj.SchemeName = s.Replace(".cs", "");
                Console.WriteLine("Creating instance of {0}", s.Replace(".cs", ""));
                this.SchemeList.Add(schemeObj);
                count++;
            }
        }
    }
}
