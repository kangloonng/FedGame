using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic;
using FYP_IncentiveMechanismSimulatorMVP.Model;

namespace FYP_IncentiveMechanismSimulatorMVP.Presenters
{
    public class BidFormPresenter
    {
        private View.IBidForm _IBidForm;
        public BidFormPresenter(View.IBidForm iBidForm)
        {
            this._IBidForm = iBidForm;
            this._IBidForm.player = Simulation.Instance._playerManager.PlayerList.Where(p=>p.Pid == Simulation.Instance.HumanPlayer.Pid).FirstOrDefault();
            this._IBidForm.federationList = Simulation.Instance._federationManager.FederationList.Where(f=>f.Current_state==StateEnum.BID_ROUND).ToList();
        }
        public void GetPlayerBid(int pid, int fid)
        {
            this._IBidForm.playerBid = Simulation.Instance._bidManager.RetrievePlayerBid(pid, fid);
        }
        public void GetPlayerTraining(int pid, int fid)
        {
            this._IBidForm.playerTraining = Simulation.Instance._trainingManager.RetrievePlayerTraining(pid, fid);
        }
        public int AddBid(Bid playerBid)
        {
            if (playerBid.AmountBid <= 0 | playerBid.DataBidMultiplier.DataQuality <= 0 | playerBid.DataBidMultiplier.DataQuantity <= 0
                | playerBid.ResourceBid.AssignedQty <= 0 | playerBid.AmountBid <= 0)
                return -1;

            Simulation.Instance.AddBid(playerBid);
            return 1;
            //throw new NotImplementedException();
        }

        public int EditBid(Bid playerBid)
        {
            if (playerBid.AmountBid <= 0 | playerBid.DataBidMultiplier.DataQuality <= 0 | playerBid.DataBidMultiplier.DataQuantity <= 0
                | playerBid.ResourceBid.AssignedQty <= 0 | playerBid.AmountBid <= 0)
                return -1;

            Simulation.Instance.AddBid(playerBid);
            return 1;
        }

        public int EditInTraining(InTraining playerTraining)
        {
            if (playerTraining.DataCommitted.DataQuality <= 0 | playerTraining.DataCommitted.DataQuantity <= 0
    |           playerTraining.ResourceCommited.AssignedQty <= 0)
                return -1;

            Simulation.Instance.EditTraining(playerTraining);

            return 1;

        }

        public int RemoveBid(Bid playerBid)
        {
            Simulation.Instance.RemoveBid(playerBid);

            return 1;
        }

        public int RemoveInTraining(InTraining playerTraining)
        {
            Simulation.Instance.RemoveTraining(playerTraining);

            return 1;
        }
    }
}
