using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class InTrainingManager
    {
        public List<InTraining> InTrainingList { get; set; }

        public InTrainingManager()
        {
            this.InTrainingList = new List<InTraining>();
        }
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

        public InTraining RetrievePlayerTraining(int pid, int fid)
        {
            return this.InTrainingList.Where(t => t.Pid == pid && t.Fid == fid).FirstOrDefault();
        }

        public double RetrieveFederationMaxTraining(int fid)
        {
            if(this.InTrainingList.Count !=0)
                return this.InTrainingList.Max(t => (t.Fid == fid) ? t.LocalTrainingLength : 0);
            return 0;
        }
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
