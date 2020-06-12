using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    /*
     * Manages state of game system i.e. mainly the overall system's timeline. 
     */
    public class StateManager
    {
        public StateEnum CurrentState { get; set; }
        public State CurrentStateModel { get; set; }
        public Dictionary<int, double> GlobalProfitHistory { get; set; } //for record purposes
        public StateManager()
        {
            this.CurrentStateModel = new State();
            this.CurrentState = StateEnum.BID_ROUND;
            this.CurrentStateModel.CurrentTurnProgression = 0;
            this.CurrentStateModel.CurrentTurn = 1;
            this.GlobalProfitHistory = new Dictionary<int, double>();
        }

        public double ProgressGameState(List<Federation> federationList, List<Player> playerList, List<Bid> bidsList, List<InTraining> trainingList, double bidLengthConstant)
        {
            double lowestLength = federationList.Min(f => f.TimeLeftInState);
            if (double.IsInfinity(lowestLength))
                lowestLength = bidLengthConstant; //no bids, no trains, and no profit states.

            this.AddLength(lowestLength, federationList);
            return lowestLength;
        }

        private void AddLength(double length,List<Federation> federationList)
        {
            int oldTurn = this.CurrentStateModel.CurrentTurn;
            double newTurnProgression = this.CurrentStateModel.CurrentTurnProgression + length;

            if (newTurnProgression >= 1)
            {
                int turnProgressed = (int)Math.Floor(newTurnProgression);
                for(int i=0; i< turnProgressed; i++)
                {
                    this.ProcessGlobalProfit(federationList);
                    this.CurrentStateModel.CurrentTurn += 1;
                }
                //Precision
                this.CurrentStateModel.CurrentTurnProgression = Math.Round(newTurnProgression - turnProgressed,1);
            }
            else
            {
                this.CurrentStateModel.CurrentTurnProgression += length;
            }
        }
        public void ProcessGlobalProfit(List<Federation> federationsList)
        {
            Utils.Logger tempLog = new Utils.Logger();
            tempLog.PrintConsoleMessage("Recalculate Gloabl MarketShare", "");
            if (!this.GlobalProfitHistory.ContainsKey(this.CurrentStateModel.CurrentTurn))
                this.GlobalProfitHistory.Add(this.CurrentStateModel.CurrentTurn, 1);

            double sumCollabTraining = federationsList.Sum(f => f.CollabTrainingQuality);

            foreach(Federation f in federationsList)
            {
                f.MarketShare = this.GetSharePct(f.CollabTrainingQuality, sumCollabTraining);
                Console.WriteLine("DEBUG :: New Market Share :" + f.MarketShare);
            }
        }

        private double GetSharePct(double individualOwn, double sumOfMarket)
        {
            double result = Math.Round(individualOwn / sumOfMarket);
            if (Double.IsNaN(result))
                return 0;
            
            return Math.Round(individualOwn / sumOfMarket,4);
        }

        /*
        public Tuple<int, double> ProgressGameSate(List<Federation> federationList, List<Player> playerList, List<Bid> bidsList, List<InTraining> trainingList)
        {
            List<Federation> federationsInBid = federationList.Where(f => f.Current_state == StateEnum.BID_ROUND).ToList();
            List<Federation> federationsInTraining = federationList.Where(f => f.Current_state == StateEnum.TRAIN_ROUND).ToList();
            List<Federation> federationsInProfit = federationList.Where(f => f.Current_state == StateEnum.PROFIT_ROUND).ToList();
            double length = -1;
            int decision = -1;

            /*
             * Process logic in order :
             * -To decide if no federations are in training or profit state.
             * -To decide if training time or profit time is lesser than bidding time.
             * -If lesser, then process in main thread the length to be progressed.
             * -If more, then process Bid state and subtract length to be progressed.
             
            if (federationsInTraining.Count == 0 && federationsInProfit.Count == 0)
            {
                //this.CurrentStateModel.CurrentTurnProgression += Constants.MIN_TIMEINBID;
                decision = 1;
                length = Constants.MIN_TIMEINBID;
            }
            else if (federationsInBid.Count == 0)
            {
                decision = 4;
                length = 0;
            }
            else
            {
                /*
                 * Various conditions to determine length to be returned (there is a mix of federation states):
                 * If Bid Length -> Either Profit or Train Time is the same as Bid Length;
                 * If Profit Length -> (FederationsInProfit.Count!=0) Profit must be lesser than Bid & Train time;
                 * If Train Length -> FederationsInTrain.Count!=0 ~ Train Length is smallest.
                 

                if (federationsInTraining.Count == 0)
                {
                    //Consider Profit and Bid only
                    if (federationsInProfit.Count == 0)
                    {
                        decision = 1;
                        length = Constants.MIN_TIMEINBID;
                    }
                    else
                    {
                        if (Constants.MIN_TIMEINPROFIT <= Constants.MIN_TIMEINBID)
                        {
                            decision = 3;
                            length = Constants.MIN_TIMEINPROFIT;
                        }
                    }
                }
                else if (federationsInProfit.Count == 0)
                {
                    //Consider Train and Bid only
                    double trainLength = federationsInTraining.Min(f => f.TimeLeftInState);
                    if (trainLength <= Constants.MIN_TIMEINBID)
                    {
                        decision = 2;
                        length = trainLength;
                    }
                    else
                    {
                        decision = 1;
                        length = Constants.MIN_TIMEINBID;
                    }
                }
                else
                {
                    //Decision based on lowest length
                    double trainLength = federationsInTraining.Min(f => f.TimeLeftInState);
                    if (Constants.MIN_TIMEINPROFIT <= trainLength && Constants.MIN_TIMEINPROFIT <= Constants.MIN_TIMEINBID)
                    {
                        decision = 3;
                        length = Constants.MIN_TIMEINPROFIT;
                    }
                    else if (trainLength <= Constants.MIN_TIMEINBID && trainLength <= Constants.MIN_TIMEINPROFIT)
                    {
                        decision = 2;
                        length = trainLength;
                    }
                    else
                    {
                        decision = 1;
                        length = Constants.MIN_TIMEINBID;
                    }
                }
            }
            //double lengthToTrain = 
            Console.WriteLine("DEBUG :: Decision returned ->" + decision);
            this.AddLength(length, federationList);
            return new Tuple<int, double>(decision, length);
        }*/
    }
}
