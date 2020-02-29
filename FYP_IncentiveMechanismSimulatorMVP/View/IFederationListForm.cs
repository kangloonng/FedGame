using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;

namespace FYP_IncentiveMechanismSimulatorMVP.View
{
    public interface IFederationListForm
    {
        List<Federation> FederationList { get; set; }
        List<int> FederationJoined { get; set; }
        List<int> FederationBidded { get; set; }
    }
}
