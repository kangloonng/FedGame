using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class BidModel
    {
        public int Pid { get; set; }
        public int Fid { get; set; }
        public double AmountBid { get; set; }
        public double DataQualityPct { get; set; }
        public double DataQuantityPct { get; set; }
        public int ResourceCommitted { get; set; }

        public BidModel(Model.Bid b)
        {
            this.Pid = b.Pid;
            this.Fid = b.Fid;
            this.AmountBid = b.AmountBid;
            this.DataQualityPct = b.DataBidMultiplier.DataQuality;
            this.DataQuantityPct = b.DataBidMultiplier.DataQuantity;
            this.ResourceCommitted = b.ResourceBid.AssignedQty;
        }
    }
}
