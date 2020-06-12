using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
using Python.Runtime;

namespace FYP_IncentiveMechanismSimulatorMVP.Utils
{
    /*
     * Mainly used to interface with Python classes and files
     * Current implementation framework:
     * -Usage of single incentivescheme.py file
     * -Consists of a util class and FL class
     * -util class is to generate a python list based on parameter inputs so that the FL class object is able to receive this python list to calculate payoff
     */
   public class PythonInterface
    {
        public dynamic MainModule { get; }
        public dynamic IncentiveSchemesList { get; }
        public dynamic UtilModule { get; }
        public List<string> IncentiveSchemenameList { get; }
        public PythonInterface(dynamic ImportedModule)
        {
            this.MainModule = ImportedModule;
            this.IncentiveSchemenameList = new List<string>();

            using (Py.GIL()) {
                //update incentive schemes list and num of incentive schemes
                this.IncentiveSchemesList = this.MainModule.list_fl;
                this.UtilModule = this.MainModule.util();
                foreach (dynamic incentive in this.IncentiveSchemesList)
                {
                    //IncentiveSchemesListTest.Add(incentive);
                    Console.WriteLine("Loaded scheme " + incentive.Name);
                    this.IncentiveSchemenameList.Add((string)incentive.Name);
                }
            }
        }
        //Creates Python class (new object) based on reference to FL class stored.
        public dynamic CreatePyScheme(int index, int num_players)
        {
            using (Py.GIL())
            {
                dynamic objClass = this.IncentiveSchemesList[index].__class__;
                Console.WriteLine("Creating Python Class " + objClass);

                //default
                dynamic envObj = this.MainModule.env(0, num_players);
                dynamic obj = objClass(0, envObj);
                return obj;
            }
        }
        /*
         * Returns a list of payoff based on input participants list and their contributions
         * Firstly creates a 'readable' python list containing participants' contribution, then obtaining payoff list from python class object
         */
        public List<double> Calculate_Payoff(double budgetToAllocate, dynamic schemeObj, List<Player> participantList, double data_qual_weight, double data_quant_weight)
        {
            List<double> tempList = new List<double>();
            using (Py.GIL())
            {
                foreach(Player p in participantList)
                {
                    //parameters to create list
                    int pid = p.Pid;
                    double dataQuality = p.DataOwned.DataQuality;
                    double dataQuantity = p.DataOwned.DataQuantity;
                    double modelQuality = dataQuality * data_qual_weight + data_quant_weight * dataQuantity;
                    double filterTOCHANGE = 0;
                    double costContribution = p.Asset;

                    this.UtilModule.inputList(pid, dataQuality, dataQuantity, modelQuality, filterTOCHANGE, costContribution);
                }
                //assigns budget for this round
                schemeObj.Env.budget = budgetToAllocate;
                Console.WriteLine("Federation allocated budget = $" + budgetToAllocate);
                dynamic obj = schemeObj.payout_calculation(this.UtilModule.listBuild);
                Console.WriteLine(this.UtilModule.listBuild);
                Console.WriteLine(obj);

                //reset listbuild in python
                 this.UtilModule.reInit();

                for(int i=0; i < participantList.Count; i++)
                {
                    tempList.Add(Math.Round((double)obj[i],2));
                    Console.WriteLine("Player " + (i + 1) + " received $" + tempList[i]);
                }
                schemeObj.Env.t += 1;
            }
            
            return tempList;
        }
    }
}
