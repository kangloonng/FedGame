using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public abstract class Scheme
    {
        public int SchemeId { get; set; }
        public string SchemeName { get; set; }
        public double totalTrainingQuality { get; set; }
        public double totalResouceCommitted { get; set; }
        //abstract methods
       public abstract double CalculateProfitShare(int numParticipants, double bidAmount, double indivTrainingQuality, int assignedQty, double federationAsset);
    }
}
