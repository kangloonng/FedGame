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
        /*
         * Returns a bid object based on player id and federation id.
         */
        public Bid RetrievePlayerBid(int pid, int fid)
        {
            int index = this.BidList.FindIndex(b => b.Pid == pid && b.Fid == fid);
            if (index == -1)
                return null;
            else
                return this.BidList[index];
        }
        /*
         * Adds a bid object to the list. 
         * Replaces original bid object if there exists a similar bid object with same player id and federation id.
         */
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
        /*
         * Returns a list of federation id in which player has bidded for.
         * Main usage is for UI presentation
         */
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
        /*
         * Returns a list of bids belonging to the input player id
         * List will contain 0 elements if none exists
         */
        public List<Bid> RetrievePlayerBids(int pid)
        {
            return this.BidList.Where(p => p.Pid == pid).ToList();
        }
        /*
         * Removes all bids belonging to a particular Federation
         */
        public void RemoveFederationBid(int federationId)
        {
            this.BidList = this.BidList.Where(b => b.Fid != federationId).ToList();
        }
        /*
         * Returns total bids count belonging to a particular federation
         */
        public int GetBidsCount(int federationId)
        {
            return this.BidList.Where(b => b.Fid == federationId).ToList().Count;
        }
        /*
         * Removes player bid based on player id, if exist.
         */
        public void RemovePlayerBid(Bid playerBid)
        {
            int index = this.BidList.FindIndex(b => b.Pid == playerBid.Pid && b.Fid == playerBid.Fid);
            if (index != -1)
                this.BidList.RemoveAt(index);
        }
    }
}