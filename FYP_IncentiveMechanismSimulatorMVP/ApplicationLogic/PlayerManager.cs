using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class PlayerManager
    {
        public List<Player> PlayerList { get; set; }

        public PlayerManager()
        {
            this.PlayerList = new List<Player>();
        }
        public void PopulatePlayers(int numUsers, List<Strategy> strategyList)
        {
            for(int i=0; i < numUsers; i++)
            {
                Player tempPlayer = new Player(i+1);
                tempPlayer.LocalStrategy = strategyList[0];
                this.PlayerList.Add(tempPlayer);
            }
        }
        public void AllocatePlayer(int index, DataObject dataObject, Resource resourceObject, double startingAsset,
            double dataQualityWeight, double dataQuantityWeight)
        {
            Player temp = this.PlayerList[index];
            temp.DataOwned = dataObject;
            temp.ResourceOwned = resourceObject;
            temp.IndividualWorth = dataObject.DataQuality * dataQualityWeight + dataObject.DataQuantity * dataQuantityWeight;
            temp.Asset = startingAsset;
            temp.AddProfitHistory(0, 0);
            //temp.ProfitHistory.Add(new Tuple<double,double>(0,startingAsset));
        }

        public List<Bid> ProcessCPUActions(List<Federation> federationList)
        {
            List<Bid> tempBidList = new List<Bid>();
            for(int i = 1; i < this.PlayerList.Count; i++)
            {
                Player tempPlayer = this.PlayerList[i];
                List<Bid> CPUPlayerDecision = tempPlayer.CPU_Decision(federationList);

                tempBidList.AddRange(CPUPlayerDecision);
            }
            return tempBidList;
        }

        public void RecordPlayerHistory(double progression, int turn)
        {
            foreach(Player p in this.PlayerList)
            {
                p.AddProfitHistory(turn, progression);
                //p.ProfitHistory.Add(new Tuple<double,double>(turn+progression,p.Asset));
            }
        }
    }
}
