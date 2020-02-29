using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class FederationManager
    {
        public List<Federation> FederationList { get; set; }
        public double FixedMarketShare { get; set; }
        public FederationManager()
        {
            this.FederationList = new List<Federation>();
        }

        public void PopulateFederations(int numFederations, List<Scheme> schemeList, Admission admsn,double startingTime)
        {
            for (int i = 0; i < numFederations; i++)
            {
                Federation tempFed = new Federation(i + 1, new Admission(0.5, 5, 1,100), schemeList[i%schemeList.Count], startingTime);
                tempFed.sc.totalTrainingQuality = 0;
                this.FederationList.Add(tempFed);
                tempFed.AdmissionPolicy = admsn;
            }
        }

        public void RemoveParticipant(int fid, int pid)
        {
            Federation tempFed = this.FederationList.Where(f => f.FederationId == fid).FirstOrDefault();
            int playerIndex = tempFed.ParticipantList.FindIndex(p => p.Pid == pid);
            tempFed.ParticipantList.RemoveAt(playerIndex);
 
        }


        public void DisseminateGlobalMarketAsset(double FIXED_MARKET_SHARE)
        {
            foreach(Federation f in this.FederationList)
            {
                f.FederationAsset += Math.Round((f.MarketShare * FIXED_MARKET_SHARE),2);
            }
        }

        public void RecordFederationMktShareHistory()
        {
            foreach(Federation f in this.FederationList)
            {
                f.FederationMarketShareHistory.Add(f.MarketShare);
            }
        }
    }
}
