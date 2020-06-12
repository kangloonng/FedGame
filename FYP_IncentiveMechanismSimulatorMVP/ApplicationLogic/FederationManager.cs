using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    /*
     * Manages Federations created during game initialization
     */
    public class FederationManager
    {
        public List<Federation> FederationList { get; set; }
        public double FixedMarketShare { get; set; }
        public FederationManager()
        {
            this.FederationList = new List<Federation>();
        }
        /*
         * Creates Federation object based on input. 
         * schemeList is a list of run-time created object of Python class; 1 to 1 assignment of Federation and Incentive Scheme
         */
        public void PopulateFederations(int numFederations, List<dynamic> schemeList, Admission admsn,double startingTime)
        {
            using (Python.Runtime.Py.GIL())
            {
                for (int i = 0; i < numFederations; i++)
                {
                    Federation tempFed = new Federation(i + 1, new Admission(0.5, 5, 1, 100), (string)schemeList[i].Name, startingTime);
                    this.FederationList.Add(tempFed);
                    tempFed.AdmissionPolicy = admsn;
                }
            }
        }

        public void RemoveParticipant(int fid, int pid)
        {
            Federation tempFed = this.FederationList.Where(f => f.FederationId == fid).FirstOrDefault();
            int playerIndex = tempFed.ParticipantList.FindIndex(p => p.Pid == pid);
            tempFed.ParticipantList.RemoveAt(playerIndex);
 
        }
        /*
         * Disseminate fixed market asset to each Federation based on market share at the point of time.
         */
        public void DisseminateGlobalMarketAsset(double FIXED_MARKET_SHARE)
        {
            foreach(Federation f in this.FederationList)
            {
                f.FederationAsset += Math.Round((f.MarketShare * FIXED_MARKET_SHARE),2); //rounding
            }
        }

        /*
         * For visualization purposes
         */
        public void RecordFederationMktShareHistory()
        {
            foreach(Federation f in this.FederationList)
            {
                f.FederationMarketShareHistory.Add(f.MarketShare);
            }
        }

        public void FederationDebug()
        {
            foreach(Federation f in this.FederationList)
            {
                Console.WriteLine(String.Format("Federation {0} Num Participants {1}", f.FederationId, f.ParticipantList.Count));
                foreach(Player p in f.ParticipantList)
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }
    }
}
