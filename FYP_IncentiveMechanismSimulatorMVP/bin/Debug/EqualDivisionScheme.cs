using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;

namespace FYP_IncentiveMechanismSimulatorMVP.Schemes
{
    public class EqualDivisionScheme : Model.Scheme
    {
        public override int CalculateProfitShare(List<Player> playerList, double marketShare, double fixed_marketShare)
        {
            return -1;
        }
    }
}
