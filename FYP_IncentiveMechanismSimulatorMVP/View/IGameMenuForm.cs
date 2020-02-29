using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.View
{
    public interface IGameMenuForm
    {
        double MarketShare { get; set; }
        List<Player> PlayerList { get; set; }
        List<Federation> FederationList { get; set; }
        List<Bid> PlayerBidList { get; set; }
        State CurrentState { get; set; }
        List<string> PreviousTurnEvents { get; set; }
        List<ParticipantHistory> ParticipantHistoryList { set; }
        Player CurrentPlayer { set; }
    }
}
