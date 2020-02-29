using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public abstract class Strategy
    {
        public int StrategyId { get; set; }
        public string StrategyName { get; set; }

        public abstract int ResourceQuantity(int numFederation, int resourceQty);
        public abstract double DataQualityMultiplier(int numFederation);
        public abstract double DataQuantityMultiplier(int numFederation);
        public abstract double AssetToBid(int numFederation, double asset);
    }
}
