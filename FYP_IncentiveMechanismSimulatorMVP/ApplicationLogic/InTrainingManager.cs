using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    /*
     * Manages InTraining objects for Federations
     */
    public class InTrainingManager
    {
        public List<InTraining> InTrainingList { get; set; }

        public InTrainingManager()
        {
            this.InTrainingList = new List<InTraining>();
        }
        /*
         * Return list of federations that the player is currently in participant of
         */
        public List<int> RetrieveFederationIdList(int pid)
        {
            List<int> listOfFederationIdPlayerJoined = new List<int>();
            foreach(InTraining t in this.InTrainingList)
            {
                if (t.Pid == pid)
                    listOfFederationIdPlayerJoined.Add(t.Fid);
            }
            return listOfFederationIdPlayerJoined;
        }
        /*
         * Returns player's in training record of particular federation
         */
        public InTraining RetrievePlayerTraining(int pid, int fid)
        {
            return this.InTrainingList.Where(t => t.Pid == pid && t.Fid == fid).FirstOrDefault();
        }
        /*
         * Returns maximum training length of participants in a Federation to calculate time required in training round
         */
        public double RetrieveFederationMaxTraining(int fid)
        {
            if(this.InTrainingList.Count !=0)
                return this.InTrainingList.Max(t => (t.Fid == fid) ? t.LocalTrainingLength : 0);
            return 0;
        }
        /*
         * Returns total number of participants with InTraining object in Federation. Should tally with number of participants in Federation's participantList
         */
        public int RetrieveFederationTrainingCount(int federationId)
        {
            int count = 0;
            count = this.InTrainingList.Count(t=>t.Fid == federationId);
            return count;
        }

        public void RemoveTrainingRecord(int pid, int fid)
        {
            int index = this.InTrainingList.FindIndex(t => t.Pid == pid && t.Fid == fid);
            if (index != -1)
                this.InTrainingList.RemoveAt(index);
        }
    }
}
