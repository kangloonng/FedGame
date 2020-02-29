using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class Admission
    {
        public double DataQualityThreshold { get; set; }
        public double DataQuantityThreshold { get; set; }
        public int ResourceThreshold { get; set; }
        public double AmountBidThreshold { get; set; }
        public Admission(double DataQualityT, double DataQuantityT, int ResourceT, double AmountBidThreshold)
        {
            //use of constants to assign first
            this.DataQualityThreshold = DataQualityT;
            this.DataQuantityThreshold = DataQuantityT;
            this.ResourceThreshold = ResourceT;
            this.AmountBidThreshold = AmountBidThreshold;
        }
        public Boolean VerifyQualification(double bidDataQuality, double bidDataQuantity, int resourceQty, double amountBidThreshold)
        {
            if (bidDataQuality >= this.DataQualityThreshold && bidDataQuantity >= this.DataQuantityThreshold && 
                resourceQty >= this.ResourceThreshold && amountBidThreshold >= this.AmountBidThreshold)
                return true;
            else
                return false;
        }
    }
}
