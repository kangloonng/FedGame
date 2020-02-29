using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Strategies
{
    public class EqualDistribution : Model.Strategy
    {
        private double initialBidAmnt = 20;
        private Random ran = new Random();
        public override double DataQualityMultiplier(int numFederation)
        {
            double minRange = 0.5, maxRange = 1;
            double multiplier = minRange + (maxRange - minRange) * ran.NextDouble();
            multiplier = Math.Round(multiplier, 2);
            Console.WriteLine("Multiplier: " + multiplier);
            return multiplier;
        }

        public override double DataQuantityMultiplier(int numFederation)
        {
            double minRange = 0.5, maxRange = 1;
            double multiplier = minRange + (maxRange - minRange) * ran.NextDouble();
            multiplier = Math.Round(multiplier, 2);
            Console.WriteLine("Multiplier: " + multiplier);
            return multiplier;
        }

        public override int ResourceQuantity(int numFederation, int resourceQty)
        {
            return Convert.ToInt32(Math.Ceiling(resourceQty / Convert.ToDouble(numFederation)));
        }
        public override double AssetToBid(int numFederation, double assetLeft)
        {
            //double assetLeftover = assetLeft * 0.1;
            //return Convert.ToDouble(Math.Ceiling(assetLeftover / Convert.ToDouble(numFederation)));

            return this.initialBidAmnt;
        }
    }
}
