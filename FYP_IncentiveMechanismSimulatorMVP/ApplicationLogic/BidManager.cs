using System;
using System.Collections.Generic;
using System.Linq;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class BidManager
    {
        public List<Bid> BidList { get; set; }
        public BidManager()
        {
            BidList = new List<Bid>(); //Dictionary<Tuple<int, int>, Bid>();
        }
        public Bid RetrievePlayerBid(int pid, int fid)
        {
            int index = this.BidList.FindIndex(b => b.Pid == pid && b.Fid == fid);
            if (index == -1)
                return null;
            else
                return this.BidList[index];
        }

        public void AddBid(Bid playerBid)
        {
            int index = this.BidList.FindIndex(b => b.Pid == playerBid.Pid && b.Fid == playerBid.Fid);
            if (index == -1)
            {
                this.BidList.Add(playerBid);
            }
            else
            {
                Bid tempBid = this.BidList[index];
                this.BidList[index] = playerBid;
            }
        }

        public List<int> RetrieveFederationIdList(int pid)
        {
            List<int> listOfFederationIdPlayerBid = new List<int>();
            foreach(Bid b in this.BidList)
            {
                if (b.Pid == pid)
                {
                    listOfFederationIdPlayerBid.Add(b.Fid);
                }
            }
            return listOfFederationIdPlayerBid;
        }

        internal List<Bid> RetrievePlayerBids(int pid)
        {
            return this.BidList.Where(p => p.Pid == pid).ToList();
        }

        internal void RemoveFederationBid(int federationId)
        {
            this.BidList = this.BidList.Where(b => b.Fid != federationId).ToList();
        }

        internal int GetBidsCount(int federationId)
        {
            return this.BidList.Where(b => b.Fid == federationId).ToList().Count;
        }
    }
}