using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class Player
    {
        public int Pid { get; set; }
        public double Asset { get; set; }
        public double AssetInBid { get; set; }
        public Resource ResourceOwned { get; set; }
        public DataObject DataOwned { get; set; }
        public double IndividualWorth { get; set; }
        //public Strategy LocalStrategy { get; set; }
        public String AgentName { get; set; }
        public List<Tuple<double,double,double>> ProfitHistory { get; set; }
        //public List<PlayerFederationObject> FedParticipationTimesList { get; set; }
        //public List<PlayerFederationObject> FedParticipationTimesList { get; set; }
        public Dictionary<int, List<double>> FederationProfitHistory { get; set; }
        //public Dictionary<int, List<InTraining>> FederationCommitmentHistory { get; set; }

        public Player(int pid)
        {
            this.AgentName = "Human Player";
            this.Pid = pid;
            this.Asset = 0;
            this.IndividualWorth = 0;
            this.AssetInBid = 0;
            this.ProfitHistory = new List<Tuple<double,double,double>>();
            this.ResourceOwned = new Resource();
            this.DataOwned = new DataObject();
            //this.FederationCommitmentHistory = new Dictionary<int, List<InTraining>>();
            this.FederationProfitHistory = new Dictionary<int, List<double>>();
            //this.FedParticipationTimesList = new List<PlayerFederationObject>();
        }

/*        public void InitList(List<Federation> federationList)
        {
            this.FedParticipationTimesList = new List<PlayerFederationObject>();
            foreach (Federation f in federationList)
            {
                PlayerFederationObject tempObj = new PlayerFederationObject(f.FederationId);
                this.FedParticipationTimesList.Add(tempObj);
            }

            //this.LocalStrategy.InitList(federationList);
        }*/

        public void AddProfitHistory(int turn, double prog)
        {
            double profitGained = 0;
            if (this.ProfitHistory.Count != 0)
            {
                profitGained = this.Asset - this.ProfitHistory[0].Item2;
            }
            this.ProfitHistory.Add(new Tuple<double, double, double>(turn + prog, this.Asset, profitGained));
        }

        public override string ToString()
        {
            return String.Format("Player {0} Data Quality/ Quantity {1}/{2} Resource Commited {3} Amount Bid {4}",
               Pid, DataOwned.DataQuality, DataOwned.DataQuantity, ResourceOwned.AssignedQty, Asset);
        }

        //for future creation of child or other design patterns of agents; mainly for method convention
        #region Agent Decision
        public virtual List<Tuple<int, int, double, double, int, double>> GenerateBidList(List<Federation> federationOpenForBids)
        {
            return null;
        }

        public virtual int SelectActionLeaveStay(int fid, double profitLastRound)
        {
            return 0;
        }

        #endregion
    }
}
