using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
using FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic;
namespace FYP_IncentiveMechanismSimulatorMVP.Presenters
{
    public class FederationListPresenter
    {
        private View.IFederationListForm _federationListView;
        
        public FederationListPresenter(View.IFederationListForm federationListView)
        {
            this._federationListView = federationListView;

            this._federationListView.FederationList=ApplicationLogic.Simulation.Instance._federationManager.FederationList;
            this._federationListView.FederationJoined = ApplicationLogic.Simulation.Instance._trainingManager.RetrieveFederationIdList(Simulation.Instance.HumanPlayer.Pid);
            this._federationListView.FederationBidded = Simulation.Instance._bidManager.RetrieveFederationIdList(Simulation.Instance.HumanPlayer.Pid);
        }
        public int AddBid(Bid playerBid)
        {
            if (playerBid.AmountBid == 0 || playerBid.DataBidMultiplier.DataQuality == 0 || playerBid.DataBidMultiplier.DataQuantity == 0 || playerBid.ResourceBid.AssignedQty == 0)
                return -1;

            Simulation.Instance.AddBid(playerBid);
            return 1;
            //throw new NotImplementedException();
        }

        public void EditTrainining(InTraining playerTraining)
        {
            Simulation.Instance.EditTraining(playerTraining);
        }
    }
}
