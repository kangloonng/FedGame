using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;

namespace FYP_IncentiveMechanismSimulatorMVP.Schemes
{
    public class EqualDivisionScheme : Model.Scheme
    {
        public EqualDivisionScheme()
        {
        }

        public override double CalculateProfitShare(int numParticipants, double bidAmount, double indivTrainingQuality, int assignedQty, double federationAsset)
        {
            return federationAsset / (double)numParticipants;
        }
    }
}
