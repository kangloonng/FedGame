using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic;
using FYP_IncentiveMechanismSimulatorMVP.Model;

namespace FYP_IncentiveMechanismSimulatorMVP.Presenters
{
    public class GameMenuPresenter
    {
        private View.IGameMenuForm _gameMenu;
        
        public GameMenuPresenter(View.IGameMenuForm gameMenu)
        {
            this._gameMenu = gameMenu;
        }

        public void Update()
        {
            this._gameMenu.PlayerList = Simulation.Instance._playerManager.PlayerList;
            this._gameMenu.FederationList = Simulation.Instance._federationManager.FederationList;
            this._gameMenu.CurrentState = Simulation.Instance._stateManager.CurrentStateModel;
            this._gameMenu.PlayerBidList = Simulation.Instance._bidManager.RetrievePlayerBids(Simulation.Instance.HumanPlayer.Pid);
            this._gameMenu.PreviousTurnEvents = Simulation.Instance.eventsManager.PreviousTurnEvent;
            this._gameMenu.ParticipantHistoryList = Simulation.Instance.dbManager.ParticipantsHistoryList;
            this._gameMenu.CurrentPlayer = Simulation.Instance._playerManager.PlayerList.Where(p => p.Pid == Simulation.Instance.HumanPlayer.Pid).FirstOrDefault();
        }
        public int ProcessEndRound()
        {
            int end = Simulation.Instance.ProcessEndRound();
            this.Update();

            return end;
        }

        public List<InTraining> DebugInTrainingList()
        {
            return Simulation.Instance._trainingManager.InTrainingList;
        }

        public double GetInBidTime()
        {
            return Simulation.Instance.simulationSettings.MIN_BID_LENGTH;
        }

        public void SaveDB()
        {
            Simulation.Instance.SaveDB();
        }
    }
}
