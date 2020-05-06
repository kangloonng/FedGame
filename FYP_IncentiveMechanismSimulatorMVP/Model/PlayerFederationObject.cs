using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class PlayerFederationObject
    {
        public int Fid { get; set; }
        public bool ToParticipate { get; set; }
        public bool OpenForBidCurrent { get; set; }
        public double BidAmtSoFar { get; set; }
        public double ProfitSoFar { get; set; }
        public int NumTimesParticipate { get; set; }

        public PlayerFederationObject(int fid)
        {
            this.Fid = fid;
            this.ToParticipate = true;
            this.BidAmtSoFar = 0;
            this.ProfitSoFar = 0;
            this.NumTimesParticipate = 0;
        }
    }
}
