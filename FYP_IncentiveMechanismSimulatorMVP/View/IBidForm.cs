using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;

namespace FYP_IncentiveMechanismSimulatorMVP.View
{
    public interface IBidForm
    {
        int fid { get; set; }
        Player player { get; set; }
        Bid playerBid { get; set; }
        InTraining playerTraining { get; set; }
        List<Federation> federationList {set; }
    }
}
