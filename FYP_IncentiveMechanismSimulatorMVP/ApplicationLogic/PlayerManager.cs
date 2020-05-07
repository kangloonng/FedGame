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
        private Random random = new Random();
        public PlayerManager()
        {
            this.PlayerList = new List<Player>();
        }
        public void PopulatePlayers(int numUsers,List<Federation> federationList)
        {
            for(int i=0; i < numUsers; i++)
            {
                Player tempPlayer;
                if (i == 0)
                    tempPlayer = new Player(i + 1);
                else
                    tempPlayer = new EqualDistributionAgent(i + 1);

                //tempPlayer.LocalStrategy = temp; // strategyList[0];
                tempPlayer.InitList(federationList);
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
                EqualDistributionAgent tempPlayer = (EqualDistributionAgent) this.PlayerList[i];
                //List<Bid> CPUPlayerDecision = tempPlayer.CPU_Decision2(federationList);

                //tempBidList.AddRange(CPUPlayerDecision);
                List<Federation> shuffleList = RandomizeList(federationList);
                List<Tuple<int,int,double,double,int,double>> tempList = tempPlayer.GenerateBidList(shuffleList);
                if(tempList!=null)
                    tempBidList.AddRange(this.GenerateBidList(tempList));
            }
            return tempBidList;
        }

        private List<Bid> GenerateBidList(List<Tuple<int, int, double, double, int, double>> tempList)
        {
            List<Bid> tempbidList = new List<Bid>();
            foreach(Tuple<int,int,double,double,int,double> tup in tempList)
            {
                Bid tempObj = new Bid(tup.Item1, tup.Item2, tup.Item3, tup.Item4, tup.Item5, tup.Item6);
                tempbidList.Add(tempObj);
            }
            return tempbidList;
        }

        public void RecordPlayerHistory(double progression, int turn)
        {
            foreach(Player p in this.PlayerList)
            {
                p.AddProfitHistory(turn, progression);
                //p.ProfitHistory.Add(new Tuple<double,double>(turn+progression,p.Asset));
            }
        }

        /*
        public bool UpdateEnvironment(int federationId, int playerIndex, double profitEarned, double admissionAmt)
        {
            Player tempObj = this.PlayerList[playerIndex];//this.PlayerList.Where(p => p.Pid == playerId).FirstOrDefault();
            bool decisionStay = true;
            
            if (tempObj != null)
            {
                decisionStay=tempObj.UpdateList(federationId, profitEarned, admissionAmt);
                //tempObj.LocalStrategy.ToString();
                //var listObj = tempObj.LocalStrategy.FedParticipationTimesList.Where(f => f.Fid == federationId).FirstOrDefault();
                
                
            }
            return decisionStay;
        }*/

        public void ReturnResources(int pid, int assignedQty)
        {
            Player tempObj =this.PlayerList.Where(p => p.Pid == pid).FirstOrDefault();
            if (tempObj == null)
                return;

            //tempObj.ResourceOwned.AssignedQty += assignedQty;
            tempObj.ResourceOwned.InTrainingQty -= assignedQty;
        }
        public void ReturnBidResources(int pid, double assetInBid, int resourceQty)
        {
            Player tempObj = this.PlayerList.Where(p => p.Pid == pid).FirstOrDefault();
            if (tempObj == null)
                return;

            //tempObj.ResourceOwned.AssignedQty += assignedQty;
            tempObj.ResourceOwned.InTrainingQty -= resourceQty;
            tempObj.AssetInBid -= assetInBid;
        }

        public List<T> RandomizeList<T>(List<T> list)
        {
            var result = list.Select(x => new { value = x, order = random.Next() })
            .OrderBy(x => x.order).Select(x => x.value).ToList();

            return result;
        }

        public int SetPlayerAsset(int pid, int fid, double val, bool agent)
        {
            this.PlayerList[pid].Asset += val;
            if (agent)
            {
                if (!this.PlayerList[pid].FederationProfitHistory.ContainsKey(fid))
                    this.PlayerList[pid].FederationProfitHistory.Add(fid, new List<double>());

                this.PlayerList[pid].FederationProfitHistory[fid].Add(val);
                return this.PlayerList[pid].SelectActionLeaveStay(fid, val);
            }

            return -1; //human
        }
    }
}
