using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    /*
     * Creates a self-interested agent that follows an equal distribution mentality. 
     * Agent will attempt to spread its resources as evenly as possible to each Federation
     * Leave Federation when profit is lower than admission cost.
     * Explore all possible Federations until all have been explored, then choose highest payoff from history and stick to it.
     */
    public class EqualDistributionAgent : Player
    {
        //edit this to modify commitment
        private double _initial_bid_amnt = 100; //initial bid amount
        private double _dataQuantiy_commit_multiplier = 1; //commit all data quantity
        private double _dataQuality_commit_multiplier = 1; //commit all data quality
        public EqualDistributionAgent(int pid) : base(pid)
        {
            this.AgentName = "Equal Distribution";
        }

        #region Agent Decision
        /* Returns a list of tuple objects, if any, for bidding to federations
         * Input: random sorted list of federations open for bid
         */
        public override List<Tuple<int,int,double,double,int,double>> GenerateBidList(List<Federation> federationOpenForBids)
        {
            if (federationOpenForBids.Count == 0)
                return null;

            List<Tuple<int, int, double, double, int,double>> tempBidList = new List<Tuple<int, int, double, double,int ,double>>();
            int leftOverQty = ResourceOwned.AssignedQty - ResourceOwned.InBidQty - ResourceOwned.InTrainingQty;
            if (leftOverQty <= 0)
                return null;
            List<int> eligibleFederations = new List<int>();

            // first round check - eliminate joined
            foreach(Federation f in federationOpenForBids)
            {
                if (this.FederationProfitHistory.ContainsKey(f.FederationId)) //check if previously joined
                {
                    continue;
                }

                eligibleFederations.Add(f.FederationId);
            }

            //if valid then open joined federations for consideration
            if(eligibleFederations.Count == 0)
            {
                eligibleFederations.Clear();
                //lax criteria
                foreach (Federation f in federationOpenForBids)
                {
                    if (this.FederationProfitHistory.ContainsKey(f.FederationId)) //check if previously joined
                    {
                        var obj = this.FederationProfitHistory[f.FederationId];
                        if (obj[obj.Count - 1] < _initial_bid_amnt) //latest profit val
                            continue;

                    }

                    eligibleFederations.Add(f.FederationId);
                }
            }

            double amntDataQualityCommit = this._dataQuality_commit_multiplier;
            double amntDataQuantityCommit = this._dataQuantiy_commit_multiplier;
            double assetTobid = this._initial_bid_amnt <= (this.Asset - this.AssetInBid) ? this._initial_bid_amnt : (this.Asset - this.AssetInBid);
            if (eligibleFederations.Count == 0)
            {
                //find most profitable TODO
                //int maxFed = this.FederationProfitHistory.OrderByDescending(f => f.Value).FirstOrDefault().Key;
                int maxFed = FederationProfitHistory.Aggregate((l, r) => l.Value.Max() > r.Value.Max() ? l : r).Key;
                tempBidList.Add(new Tuple<int, int, double, double, int, double>(this.Pid, maxFed, amntDataQualityCommit, amntDataQuantityCommit, leftOverQty, assetTobid));
                this.ResourceOwned.InBidQty += leftOverQty;
                this.AssetInBid += assetTobid;
            }
            else if(leftOverQty == 1 && eligibleFederations.Count>1) 
            {
                tempBidList.Add(new Tuple<int, int, double, double, int, double>(this.Pid, eligibleFederations[0], amntDataQualityCommit, amntDataQuantityCommit, leftOverQty, assetTobid));
                this.ResourceOwned.InBidQty += leftOverQty;
                this.AssetInBid += assetTobid;
                //if (!this.FederationProfitHistory.ContainsKey(eligibleFederations[0]))
                    //this.FederationProfitHistory.Add(eligibleFederations[0], new List<double>());
            }
            else
            {
                int toSplit = Convert.ToInt32(Math.Ceiling(leftOverQty / Convert.ToDouble(eligibleFederations.Count)));
                for(int i=0; i<eligibleFederations.Count;i++)
                {

                    int fedId = eligibleFederations[i];
                    if (i + 1 == eligibleFederations.Count)
                        toSplit = leftOverQty;

                    //if(!this.FederationProfitHistory.ContainsKey(fedId))
                        //this.FederationProfitHistory.Add(fedId, new List<double>());
                    tempBidList.Add(new Tuple<int, int, double, double, int, double>(this.Pid, fedId, amntDataQualityCommit, amntDataQuantityCommit, toSplit, assetTobid));
                    leftOverQty = leftOverQty - toSplit;

                    this.ResourceOwned.InBidQty += toSplit;
                    this.AssetInBid += assetTobid;
                    if (leftOverQty <= 0 || (this.Asset-this.AssetInBid)<=0)
                        break;

                    assetTobid = (this.Asset - this.AssetInBid) >= this._initial_bid_amnt ? this._initial_bid_amnt : (this.Asset-this.AssetInBid); // (this.Asset - this.AssetInBid);
                }
            }
            return tempBidList;
        }

        /* Select Action to leave or stay with federation
         * 0 - leave | 1 - stay
         */
        public override int SelectActionLeaveStay(int fid, double profitLastRound)
        {
            int tolerance_rnd = 1;
            int countProfitDis = this.FederationProfitHistory[fid].Count;

            if ((tolerance_rnd - countProfitDis) <= 0)
                if (profitLastRound < _initial_bid_amnt) //TODO different threshold
                    return 0;

            return 1; //TODO change back to 1
        }


        #endregion

    }
}
