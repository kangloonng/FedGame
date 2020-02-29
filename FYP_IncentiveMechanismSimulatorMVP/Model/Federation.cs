using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic;
namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class Federation
    {
        public int FederationId { get; set; }
        public List<Player> ParticipantList { get; set; }
        public double MarketShare { get; set; }
        public Admission AdmissionPolicy { get; set; }
        public Scheme sc { get; set; }
        public double CollabTrainingQuality { get; set; }
        public StateEnum Current_state { get; set; }
        public double TimeLeftInState { get; set; }
        public double FederationAsset { get; set; }
        public List<double> FederationMarketShareHistory { get; set; }
        public Federation(int fid, Admission admission, Scheme sc, double timeLeftInState)
        {
            this.FederationId = fid;
            this.AdmissionPolicy = admission;
            this.sc = sc;

            this.MarketShare = 0;
            this.CollabTrainingQuality = 0;
            this.ParticipantList = new List<Player>();
            //this.ParticipantList = new Dictionary<int, Tuple<DataObject, Resource>>();
            this.Current_state = StateEnum.BID_ROUND;
            this.TimeLeftInState = timeLeftInState;
            this.FederationAsset = 1000;
            this.FederationMarketShareHistory = new List<double>();
        }

        public string ParticipantString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Assets: $" + this.FederationAsset+Environment.NewLine);
            if (ParticipantList.Count == 0)
            {
                sb.Append("Nil Participants");
                return sb.ToString();
            }

            sb.Append("No of Participants: " + this.ParticipantList.Count +Environment.NewLine);
            sb.Append("Participants: ");
            
            for (int i = 0; i < this.ParticipantList.Count; i++)
            {
                sb.Append(ParticipantList[i].Pid + "; ");//Environment.NewLine);
            }
            
            return sb.ToString();
        }
        public string ParticipantStringwData()
        {
            if (ParticipantList.Count == 0)
                return "Nil";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.ParticipantList.Count; i++)
            {
                sb.Append("Player " + ParticipantList[i].Pid + "; Quality :" + ParticipantList[i].DataOwned.DataQuality
                    + "; Quantity :" + ParticipantList[i].DataOwned.DataQuantity + Environment.NewLine);
            }

            return sb.ToString();
        }
        public void EditFederationState(StateEnum next_state, double maxTimeInTraining)
        {
            this.Current_state = next_state;
            this.TimeLeftInState = maxTimeInTraining;
        }

        public void UpdateLength(double length, double dataQualityWeight, double dataQuantityWeight)
        {
            this.TimeLeftInState = Math.Round(this.TimeLeftInState - length,1);
            //this.TimeLeftInState -= length;
            if (this.TimeLeftInState == 0)
            {
                Console.WriteLine("Federation " + this.FederationId + " has entered next state");
                int nextState = ((int)this.Current_state + 1) % Enum.GetNames(typeof(StateEnum)).Length;
                int oldState = (int)this.Current_state;
                this.Current_state = (StateEnum)nextState;
                if((StateEnum) oldState == StateEnum.TRAIN_ROUND)
                {
                    this.CalculateCollabTQ(dataQualityWeight, dataQuantityWeight);
                }
                else if((StateEnum) oldState == StateEnum.PROFIT_ROUND)
                {
                    this.TimeLeftInState = double.PositiveInfinity;
                    //this.DisseminateProfits();
                }
                this.TimeLeftInState = double.PositiveInfinity;

            }
        }

        public void CalculateCollabTQ(double dataQualityWeight, double dataQuantityWeight)
        {
            sc.totalTrainingQuality = 0;
            Utils.Logger tempLog = new Utils.Logger();
            List<double> trainingQualityList = new List<double>();
            foreach(Player p in this.ParticipantList)
            {
                tempLog.PrintConsoleMessage("PROCESS_TRAINING", "Player " + p.Pid + " submitted");
                tempLog.PrintConsoleMessage("Data ", "Quality ->" + p.DataOwned.DataQuality + " Quantity ->" + p.DataOwned.DataQuantity);
                tempLog.PrintConsoleMessage("Resource", "Quantity ->" + p.ResourceOwned.AssignedQty);
                double valueOfData = (p.DataOwned.DataQuality * dataQualityWeight) + (p.DataOwned.DataQuantity * dataQuantityWeight);
                trainingQualityList.Add(valueOfData);
                sc.totalTrainingQuality += valueOfData;
            }
            //Rounding
            double maxValue = trainingQualityList.Max();
            double collabTQ = maxValue - this.calStandardDeviation(trainingQualityList);
            this.CollabTrainingQuality = Math.Round(collabTQ,3);
            tempLog.PrintConsoleMessage("Collab Training Quality ", this.CollabTrainingQuality.ToString());

        }


        private double calStandardDeviation(List<double> values)
        {
            double mean = values.Average();
            double ssd = values.Select(val => (val - mean) * (val - mean)).Sum();
            double sdev = Math.Sqrt(ssd / values.Count);
            return sdev;
        }
        public List<Tuple<int,double>> DisseminateProfits(double marketPct, double dataQualityWeight, double dataQuantityWeight)
        {
            Utils.Logger templog = new Utils.Logger();
            templog.PrintConsoleMessage("PROFIT_CALCULATION", " Disseminating profits");
            Console.WriteLine("Federation {1} , Scheme :{0}", sc.SchemeName,this.FederationId);
            List<Tuple<int, double>> tupleList = new List<Tuple<int, double>>();
            Tuple<int, double> tempTuple;
            foreach (Player p in this.ParticipantList)
            {
                double profit = this.sc.CalculateProfitShare(this.ParticipantList.Count, p.Asset, this.calculateTrainingQuality(p,dataQualityWeight,dataQuantityWeight),
                    p.ResourceOwned.AssignedQty, marketPct*this.FederationAsset);

                //round up to nearest dollar
                profit = Math.Ceiling(profit);
                tempTuple = new Tuple<int, double>(p.Pid, profit);
                //p.Asset += profit;
                tupleList.Add(tempTuple);
                Console.WriteLine("Profit to Player {0} -> ${1}", p.Pid, profit);
            }

            return tupleList;

            //double toSplit = this.sc.CalculateProfitShare(playerList,this.MarketShare,Constants.FIXED_MARKETSHARE);
        }
        private double calculateTrainingQuality(Player p, double dataQualityWeight, double dataQuantityWeight)
        {
            return (p.DataOwned.DataQuality * dataQualityWeight) + (p.DataOwned.DataQuantity * dataQuantityWeight);
        }
    }
}
