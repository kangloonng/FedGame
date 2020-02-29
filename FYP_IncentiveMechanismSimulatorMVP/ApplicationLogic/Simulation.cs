using FYP_IncentiveMechanismSimulatorMVP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using FYP_IncentiveMechanismSimulatorMVP.Utils;

namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public sealed class Simulation
    {
        private static Simulation instance = null;
        private static readonly object padlock = new object();

        public FederationManager _federationManager;
        public PlayerManager _playerManager { get; set; }
        public Player HumanPlayer { get; set; } // keep a reference to index 0 of list.
        private SchemeManager _schemeManager;
        public StateManager _stateManager { get; set; }
        public BidManager _bidManager { get; set; }
        public InTrainingManager _trainingManager { get; set; }
        public StrategyManager strategyManager { get; set; }
        public List<Tuple<string, Tuple<string, string>>> settingsList { get; set; }
        public EventsManager eventsManager { get; set; }
        public SimulationSettings simulationSettings { get; set; }
        public DBManager dbManager { get; set; }

        private AllocationManager _allocationManager;
        private Utils.Logger _log;
        private IOManager _ioManager;
        private int _numUsers;
        private int _numFederations;


        private string env_path = System.IO.Path.GetFullPath(@"..\..\") + "\\Resources";
        public static Simulation Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Simulation();
                    }
                    return instance;
                }
            }
        }

        public void EditTraining(InTraining playerTraining)
        {
            int index = this._trainingManager.InTrainingList.FindIndex(t => (t.Pid == playerTraining.Pid) && (t.Fid == playerTraining.Fid));
            if (index != -1)
            {
                if (playerTraining.ResourceCommited.AssignedQty == 0)
                {
                    this._federationManager.RemoveParticipant(this._trainingManager.InTrainingList[index].Fid, this._trainingManager.InTrainingList[index].Pid);
                    this._trainingManager.InTrainingList.RemoveAt(index);
                }
                else
                    this._trainingManager.InTrainingList[index] = playerTraining;

                this.UpdateHumanPlayer();
            }
        }

        public void InitSettings()
        {
            this._ioManager = new IOManager();
            //this._ioManager.ReInitSettings();
            this._log = new Utils.Logger();
            //Load sim settings
            this.simulationSettings = new SimulationSettings();
            _log.PrintInit(simulationSettings.ToString());
            this.simulationSettings = new SimulationSettings(this._ioManager.GetSettings());

        }
        public void Init()
        {
            //Init all the managers
            //To incorporate Level settings local file.
            this.InitSettings();

            this._numUsers = this.simulationSettings.NUM_OF_PLAYERS+1;
            this._numFederations = this.simulationSettings.NUM_OF_FEDERATIONS;

            this._playerManager = new PlayerManager();
            this._federationManager = new FederationManager();
            this._schemeManager = new SchemeManager();
            this._stateManager = new StateManager();
            this._allocationManager = new AllocationManager();
            this._bidManager = new BidManager();
            this._trainingManager = new InTrainingManager();
            this.strategyManager = new StrategyManager();
            this.eventsManager = new EventsManager();
            this.dbManager = new DBManager();
            //init State

            //Populate Lists
            _log.PrintInit(this._playerManager.ToString());
            _playerManager.PopulatePlayers(_numUsers, strategyManager.StrategyList);
            this.HumanPlayer = this._playerManager.PlayerList[0];

            _log.PrintInit(this._allocationManager.ToString());
            _allocationManager.GenerateDifferentHands(_numUsers,this.simulationSettings);

            //Allocate Hands
            double dataQualityWeight = simulationSettings.DATA_QUALITY_WEIGHT;
            double dataQuantityWeight = simulationSettings.DATA_QUANTITY_WEIGHT;
            for (int i = 0; i < _playerManager.PlayerList.Count; i++)
            {
                Tuple<DataObject, Resource, double> tempTuple = this._allocationManager.HandList[i];
                _playerManager.AllocatePlayer(i, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3,dataQualityWeight,dataQuantityWeight);
            }

            //Load federation with different schemes
            //TODO: Random/ scheme spread of federations
            Admission policy = new Admission(simulationSettings.INIT_DATA_QUALITY, simulationSettings.INIT_DATA_QUANTITY, simulationSettings.INIT_RESOURCE_QUANTITY, simulationSettings.INIT_AMOUNT_BID);
            _federationManager.PopulateFederations(_numFederations, _schemeManager.SchemeList,policy,simulationSettings.MIN_BID_LENGTH);
            double split = 100 / this._federationManager.FederationList.Count;
            for (int i = 0; i < this._federationManager.FederationList.Count; i++)
            {
                this._federationManager.FederationList[i].MarketShare = Math.Round(split/100,2);
                this._federationManager.FederationList[i].CollabTrainingQuality = 0.1;
                this._federationManager.FederationList[i].FederationMarketShareHistory.Add(this._federationManager.FederationList[i].MarketShare);
            }

            //set up premise for initial DB set up
            //Game ID, Settings, Initial Federation/ Players
            this.dbManager.AddGameInstance(simulationSettings, Guid.NewGuid().ToString());
            Boolean b = true;
            foreach (Player p in this._playerManager.PlayerList) {
                if (p.Pid != 1)
                    b = false;
                this.dbManager.AddParticipant(p,b);
                this.dbManager.AddParticipantHistory(p, 0, 0);
            }
            foreach(Federation f in this._federationManager.FederationList)
            {
                this.dbManager.AddFederation(f);
                this.dbManager.AddFederationHistory(f, 0, 0);                
            }
            //_ioManager.LoadPythonModules(_schemeManager,strategyManager);
        }

        public void CreateEventManager<T>(T obj)
        {
            int turn = this._stateManager.CurrentStateModel.CurrentTurn;
            double prog = this._stateManager.CurrentStateModel.CurrentTurnProgression;
            this.eventsManager.CreateEventObj(turn, prog, obj);
        }

        private void UpdateHumanPlayer()
        {
            Player tempPlayer = this._playerManager.PlayerList[0];
            tempPlayer.ResourceOwned.InBidQty = this._bidManager.BidList.Sum(b => (b.Pid == tempPlayer.Pid) ? b.ResourceBid.AssignedQty : 0);
            tempPlayer.AssetInBid = this._bidManager.BidList.Sum(b => (b.Pid == tempPlayer.Pid) ? b.AmountBid : 0);
            tempPlayer.ResourceOwned.InTrainingQty =this._trainingManager.InTrainingList.Sum(t => (t.Pid == tempPlayer.Pid) ? t.ResourceCommited.AssignedQty : 0);
        }

        #region Processing of logic
        public void AddBid(Bid playerBid)
        {
            this._bidManager.AddBid(playerBid);
            //update Human Player
            this.UpdateHumanPlayer();
        }
        public int ProcessEndRound()
        {
            //round progression
            /* Round Progression
             * Progression will be based on attribute (Federation) TimeLeftInState.
             * The following steps are ranked in order to map the timeline.
             * 1. Each action turn is bounded between 0 and 1 mapped to an int number - Day.
             * 2. Progression of rounds for each federation state should come after progression of internal game state.
             * 3. This is controlled by the StateManager object.
             * 4. Thereafter StateManager will return the length to be deducted from each federation at its state.
             */

            /* Processing of bids and training
             * 1. Before round progression, bids that were applied in the current time state will be processed first through this.ProcessBids();
             * 2. Thereafter, the TimeLeftInState for each federation in question will be updated with a length.
             * 3. This length will then be used as a variable in deciding the length to be deducted for Round Progression by the StateManager.
             */
            List<Federation> federationsInBid = this._federationManager.FederationList.Where(f => f.Current_state == StateEnum.BID_ROUND && f.TimeLeftInState == simulationSettings.MIN_BID_LENGTH).ToList();
            List<Federation> federationsInTraining = this._federationManager.FederationList.Where(f => f.Current_state == StateEnum.TRAIN_ROUND).ToList();
            List<Federation> federationsInProfit = this._federationManager.FederationList.Where(f => f.Current_state == StateEnum.PROFIT_ROUND).ToList();
            this.eventsManager.PreviousTurnEvent = this.eventsManager.currentTurnEvent;
            this.eventsManager.currentTurnEvent.Clear();

            //Process CPU Player Actions
            //Goal : Create Bid Objects for Federations that are open in bid.
            this.ProcessCPUActions(federationsInBid);

            //Round Progression
            double currentTurn = this._stateManager.CurrentStateModel.CurrentTurn;
            double length = this._stateManager.ProgressGameState(this._federationManager.FederationList, this._playerManager.PlayerList,
                this._bidManager.BidList, this._trainingManager.InTrainingList,simulationSettings.MIN_BID_LENGTH);

            //for visualization graph stats
            if (currentTurn != this._stateManager.CurrentStateModel.CurrentTurn)
            {
                this._federationManager.RecordFederationMktShareHistory();
                this._federationManager.DisseminateGlobalMarketAsset(this.simulationSettings.FIXED_MARKET_SHARE);
            }
            this.ProcessTime(length);

            this.eventsManager.CreateEventString("Progressed Length: "+length);
            this.eventsManager.CreateEventObj(this._stateManager.CurrentStateModel.CurrentTurn, this._stateManager.CurrentStateModel.CurrentTurnProgression,
                "Processed End Round Done");

            //for DB store purposes
            foreach (Player p in this._playerManager.PlayerList)
            {
                this.dbManager.AddParticipantHistory(p, this._stateManager.CurrentStateModel.CurrentTurnProgression, this._stateManager.CurrentStateModel.CurrentTurn);
            }
            foreach (Federation f in this._federationManager.FederationList)
            {
                this.dbManager.AddFederationHistory(f, this._stateManager.CurrentStateModel.CurrentTurnProgression, this._stateManager.CurrentStateModel.CurrentTurn);
            }

            this._playerManager.RecordPlayerHistory(this._stateManager.CurrentStateModel.CurrentTurnProgression, this._stateManager.CurrentStateModel.CurrentTurn);

            if (this.simulationSettings.MAX_TURNS + 1 == this._stateManager.CurrentStateModel.CurrentTurn)
                return -1;

            return 1;
        }

        private void ProcessCPUActions(List<Federation> federationsInBid)
        {
            List<Bid> cpuPlayersBid = this._playerManager.ProcessCPUActions(federationsInBid);
            this._bidManager.BidList.AddRange(cpuPlayersBid);
        }

        private void ProcessTime(double length)
        {
            foreach (Federation f in this._federationManager.FederationList)
            {
                if (f.Current_state == StateEnum.BID_ROUND && this._bidManager.GetBidsCount(f.FederationId) == 0 && this._trainingManager.RetrieveFederationTrainingCount(f.FederationId)==0)
                    continue;

                StateEnum oldState = f.Current_state;
                f.UpdateLength(length,simulationSettings.DATA_QUALITY_WEIGHT,simulationSettings.DATA_QUANTITY_WEIGHT);
                StateEnum newState = f.Current_state;

                if (oldState != newState)
                {
                    double newLength = -1;
                    if (oldState == StateEnum.BID_ROUND && newState == StateEnum.TRAIN_ROUND)
                    {
                        //Process Bids
                        bool success = this.ProcessBidsV2(f);
                        if (success)
                        {
                            this._log.PrintConsoleMessage("PROGRESS STATE ", "Federation " + f.FederationId);
                            newLength = this._trainingManager.RetrieveFederationMaxTraining(f.FederationId);
                            if (newLength > 0)
                            {
                                f.TimeLeftInState = newLength;
                                this._log.PrintConsoleMessage("PROGRESS STATE ", "Max Time Needed -> " + newLength);
                            }
                        }
                        else
                        {
                            f.Current_state = oldState;
                            f.TimeLeftInState = simulationSettings.MIN_BID_LENGTH;
                        }
                    }
                    else if (oldState == StateEnum.TRAIN_ROUND && newState == StateEnum.PROFIT_ROUND)
                    {
                        /*
                         * Time left in state to of that of profit round
                         */
                        f.TimeLeftInState = this.simulationSettings.MIN_PROFIT_LENGTH;
                        /*
                        //Release of resources allocated to players
                        List<InTraining> resourcesList = this._trainingManager.RemoveTrainResources(f.FederationId);
                        foreach (InTraining t in resourcesList)
                        {
                            Player tempPlayer = this._playerManager.PlayerList.Where(p => p.Pid == t.Pid).FirstOrDefault();
                            if (tempPlayer != null)
                            {
                                tempPlayer.ResourceOwned.InTrainingQty -= t.ResourceCommited.AssignedQty;

                            }
                        }
                        f.TimeLeftInState = Constants.MIN_TIMEINPROFIT;
                        */
                    }
                    else
                    {
                        /*
                         * Profit dissemination to all participants
                         * Reinit Time Left In State to that of bid round
                         * ->TODO: Some strategy to leave or continue staying in federation
                         */
                        List<Tuple<int, double>> tempList = f.DisseminateProfits(simulationSettings.FIXED_MARKET_SHARE_PCT,simulationSettings.DATA_QUALITY_WEIGHT,simulationSettings.DATA_QUANTITY_WEIGHT);
                        foreach (Tuple<int, double> tupleObj in tempList)
                        {
                            this._playerManager.PlayerList[(tupleObj.Item1 - 1)].Asset += tupleObj.Item2;
                            if((tupleObj.Item1) == HumanPlayer.Pid)
                            {
                                this.eventsManager.CreateEventString("Earned $" + tupleObj.Item2 + " from Federation" + f.FederationId);
                            }
                            this.CreateEventManager(String.Format("Player {0} received ${1} from Federation {2}", tupleObj.Item1, tupleObj.Item2, f.FederationId));
                        }
                        f.TimeLeftInState = simulationSettings.MIN_BID_LENGTH;

                        //f.participantlist.clear();
                        //for DB record
                        foreach (Player p in f.ParticipantList)
                        {
                            this.dbManager.AddFederationParticipationHistory(p, f.FederationId,
                                this._stateManager.CurrentStateModel.CurrentTurnProgression, this._stateManager.CurrentStateModel.CurrentTurn);
                        }
                    }

                    this.eventsManager.CreateEventString("Federation " + f.FederationId + " entered " + newState);
                    this.CreateEventManager("Federation " + f.FederationId + " entered " + newState);
                }

            }
        }

        private bool ProcessBidsV2(Federation f)
        {
            this._log.PrintConsoleMessage("BID_ROUND", "Processing Bids");
            #region Processing Bid
            List<Bid> bidsList = this._bidManager.BidList;//this._bidManager.BidList.Where(x => x.Fid == f.FederationId).ToList();
            List<Bid> bidsListFed = bidsList.Where(b => b.Fid == f.FederationId).ToList();
            if (bidsListFed.Count == 0)
            {
                if (this._trainingManager.RetrieveFederationTrainingCount(f.FederationId) != 0)
                    return true;
                else
                    return false;
            }
            //event for Federation processing bid
            this.CreateEventManager(String.Format("Federation {0} processing bids",f.FederationId));

            foreach (Bid b in bidsListFed)
            {
                int playerId = b.Pid;
                bool success = false;
                Player tempPlayer = this._playerManager.PlayerList.Where(p => p.Pid == b.Pid).FirstOrDefault();
                if (tempPlayer == null)
                {
                    continue;
                }
                else
                {
                    //Convert % into values
                    double bidDataQuality = tempPlayer.DataOwned.DataQuality * b.DataBidMultiplier.DataQuality;
                    double bidDataQuantity = tempPlayer.DataOwned.DataQuantity * b.DataBidMultiplier.DataQuantity;
                    int bidResourceQty = b.ResourceBid.AssignedQty;
                    double bidAmount = b.AmountBid;
                    //Update player resource
                    tempPlayer.ResourceOwned.InBidQty -= bidResourceQty; //regardless pass or fail bid, return bided resources, if passed will be added to training thereafeter
                    tempPlayer.Asset -= bidAmount;
                    tempPlayer.AssetInBid -= bidAmount;
                    string msg = "Processing Bid : (Data) Quality ->" + bidDataQuality + " Quantity ->" + bidDataQuantity + " (Resource) Quantity ->" + bidResourceQty +" Bid Amnt ->$"+bidAmount;
                    this._log.PrintConsoleMessage(msg);
                    if (!f.AdmissionPolicy.VerifyQualification(bidDataQuality, bidDataQuantity, bidResourceQty, bidAmount))
                    {
                        this._log.PrintConsoleMessage("Failed");
                        if(playerId == HumanPlayer.Pid)
                        {
                            this.eventsManager.CreateEventString("Bid for Federation " + b.Fid + " failed");
                        }
                        //continue;
                    }
                    else
                    {
                        success = true;
                        if (playerId == HumanPlayer.Pid)
                        {
                            this.eventsManager.CreateEventString("Bid for Federation " + b.Fid + " passed");
                        }
                        this._log.PrintConsoleMessage("Passed");
                        DataObject tempObj = new DataObject(bidDataQuality, bidDataQuantity);
                        Resource tempObj2 = new Resource(bidResourceQty);
                        Player participatedPlayer = new Player(tempPlayer.Pid);
                        participatedPlayer.DataOwned = tempObj;
                        participatedPlayer.ResourceOwned = tempObj2;
                        participatedPlayer.Asset = b.AmountBid;
                        //Update Federation's Participant List
                        f.ParticipantList.Add(participatedPlayer);
                        f.FederationAsset += b.AmountBid;
                        InTraining tempITObj = new InTraining(b.Pid,b.Fid, tempObj, tempObj2,b.AmountBid);
                        this._trainingManager.InTrainingList.Add(tempITObj);
                        tempPlayer.ResourceOwned.InTrainingQty += participatedPlayer.ResourceOwned.AssignedQty;

                        this.dbManager.AddInTrainingHistory(tempITObj, this._stateManager.CurrentStateModel.CurrentTurnProgression, this._stateManager.CurrentStateModel.CurrentTurn);

                    }
                }
                this.CreateEventManager(String.Format("Player {0}'s Bid {1}",b.Pid,success==true ? " passed" : " failed"));
                this.dbManager.AddBid(b, success);
            }
            Console.WriteLine("Federation {0} Processed {1} Bid Finished", f.FederationId, bidsListFed.Count);
            #endregion

            this._bidManager.RemoveFederationBid(f.FederationId);
            if (f.ParticipantList.Count == 0)
                return false;

            return true;
        }

        public void SaveDB()
        {
            Console.WriteLine("Saving Environmental Variables into DB");
            dbManager.saveGameInstance();

        }
        #endregion
    }
}