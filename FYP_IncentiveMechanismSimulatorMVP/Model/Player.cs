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
        public Strategy LocalStrategy { get; set; }
        public List<Tuple<double,double,double>> ProfitHistory { get; set; }
        public Player(int pid, DataObject dataOwned, Resource resourceOwned)
        {
            this.Pid = pid;
            this.DataOwned = dataOwned;
            this.ResourceOwned = resourceOwned;
            this.Asset = 0;
            this.AssetInBid = 0;
        }

        public Player(int pid)
        {
            this.Pid = pid;
            this.Asset = 0;
            this.IndividualWorth = 0;
            this.AssetInBid = 0;
            this.ProfitHistory = new List<Tuple<double,double,double>>();
        }
        public void AddProfitHistory(int turn, double prog)
        {
            double profitGained = 0;
            if (this.ProfitHistory.Count != 0)
            {
                profitGained = this.Asset - this.ProfitHistory[0].Item2;
            }
            this.ProfitHistory.Add(new Tuple<double,double,double>(turn + prog, this.Asset, profitGained));
        }
        //TODO Randomness
        public List<Bid> CPU_Decision(List<Federation> federationList)
        {
            List<Bid> tempBidList = new List<Bid>();
            int numFederation = federationList.Count;
            int resourceQuantity = this.ResourceOwned.AssignedQty - this.ResourceOwned.InBidQty - this.ResourceOwned.InTrainingQty;
            double assetLeft = this.Asset - this.AssetInBid;

            Console.WriteLine("------------CPU PLAYER {0}-------------",Pid);
            foreach(Federation f in federationList)
            {
                if (resourceQuantity <= 0 || assetLeft <=0)
                    return tempBidList;

                Bid tempBidObj = new Bid();
                int resourceQuantityDecision = this.LocalStrategy.ResourceQuantity(numFederation,resourceQuantity);
                double dataQuantityMultiplier = this.LocalStrategy.DataQuantityMultiplier(numFederation);
                double dataQualityMultiplier = this.LocalStrategy.DataQualityMultiplier(numFederation);
                double amountToBidDecision = this.LocalStrategy.AssetToBid(numFederation, assetLeft);
                numFederation -= 1;
                resourceQuantity -= resourceQuantityDecision;
                assetLeft -= amountToBidDecision;
                Console.WriteLine("Player {0} creating Bid for Federation {1}", Pid, f.FederationId);
                Console.WriteLine("Bid Details: Resource Qty {0}", resourceQuantityDecision);

                tempBidObj.ResourceBid.AssignedQty = resourceQuantityDecision;
                tempBidObj.DataBidMultiplier.DataQuality = dataQualityMultiplier;
                tempBidObj.DataBidMultiplier.DataQuantity = dataQuantityMultiplier;
                tempBidObj.AmountBid = amountToBidDecision;

                tempBidObj.Pid = this.Pid;
                tempBidObj.Fid = f.FederationId;

                tempBidList.Add(tempBidObj);

                //update InBidQty
                this.ResourceOwned.InBidQty += resourceQuantityDecision;
                this.AssetInBid += amountToBidDecision;
;            }
            Console.WriteLine("Resource Quantity Left {0}", resourceQuantity);
            return tempBidList;
        }
    }
}
