using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class Bid
    {

        public DataObject DataBidMultiplier { get; set; }
        public Resource ResourceBid { get; set; }
        public int Pid { get; set; }
        public int Fid { get; set; }
        public double AmountBid { get; set; }
        public Bid()
        {
            this.DataBidMultiplier = new DataObject();
            this.ResourceBid = new Resource();
        }

        public Bid(int pid, int fid)
        {
            Pid = pid;
            Fid = fid;
            this.DataBidMultiplier = new DataObject();
            this.ResourceBid = new Resource();
            this.AmountBid = 0;
        }

        public Bid(int pid,int fid, double dataQualityMultiplier, double dataQuantityMultiplier, int assignedQuantity, double bidAmnt)
        {
            Fid = fid;
            Pid = pid;
            this.DataBidMultiplier = new DataObject();
            this.ResourceBid = new Resource();
            this.DataBidMultiplier.DataQuality = dataQualityMultiplier;
            this.DataBidMultiplier.DataQuantity = dataQuantityMultiplier;
            this.ResourceBid.AssignedQty = assignedQuantity;
            this.AmountBid = bidAmnt;
        }

        public override string ToString()
        {
            return String.Format("Player {0} bidded for Federation {1}", this.Pid, this.Fid);
        }
    }
}
