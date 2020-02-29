using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;

namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class DBManager
    {
        private List<Bids> _bidsHistory;
        private List<FederationHistory> _federationHistory;
        private List<FederationParticipantsHistory> _federationParticipantsHistory;
        private List<Federations> _federationList;
        private List<InTrainings> _inTrainingList;
        public List<ParticipantHistory> ParticipantsHistoryList { get; set; }
        private List<Participants> _participantsList;
        private GameInstance _gameInstance;
        public DBManager()
        {
            _bidsHistory = new List<Bids>();
            _federationHistory = new List<FederationHistory>();
            _federationParticipantsHistory = new List<FederationParticipantsHistory>();
            _federationList = new List<Federations>();
            _inTrainingList = new List<InTrainings>();
            ParticipantsHistoryList = new List<ParticipantHistory>();
            _participantsList = new List<Participants>();
        }
        #region ADO object modelling
        public void AddBid(Bid b, bool success)
        {
            var tempObject = new Bids()
            {
                Pid = b.Pid,
                Fid = b.Fid,
                AmountBid = b.AmountBid,
                ResourceQty = b.ResourceBid.AssignedQty,
                DataQuality = b.DataBidMultiplier.DataQuality,
                DataQty = b.DataBidMultiplier.DataQuantity,
                Success = success
            };

            _bidsHistory.Add(tempObject);
        }
        public void AddFederationHistory(Federation f, double prog, int turn) 
        {
            var tempObj = new FederationHistory()
            {
                Fid = f.FederationId,
                Progression = prog,
                Turn = turn,
                Asset = f.FederationAsset,
                TimeLeftInState = f.TimeLeftInState,
                State = f.Current_state.ToString(),
                MarketShare = f.MarketShare,
                ModelQuality = f.CollabTrainingQuality
            };

            _federationHistory.Add(tempObj);
        }
        public void AddFederation(Federation f)
        {
            var tempObj = new Federations()
            {
                Fid = f.FederationId,
                FederationAsset = f.FederationAsset,
                SchemeAdopted = f.sc.SchemeName,
                AdmissionPolicyId = 1 //f.AdmissionPolicy
            };

            _federationList.Add(tempObj);
        }
        public void AddParticipant(Player p, Boolean b)
        {
            var tempObj = new Participants()
            {
                Pid = p.Pid,
                Strategy = p.LocalStrategy.StrategyName,
                HumanPlayer = b
            };

            _participantsList.Add(tempObj);
        }
        public void AddParticipantHistory(Player p, double prog, int turn)
        {
            var tempObj = new ParticipantHistory()
            {
                Pid = p.Pid,
                Progression = prog,
                Turn = turn,
                Asset = p.Asset,
                DataQuality = p.DataOwned.DataQuality,
                DataQuantity = p.DataOwned.DataQuantity,
                ResourceQuantity = p.ResourceOwned.AssignedQty
            };

            ParticipantsHistoryList.Add(tempObj);
        }
        public void AddInTrainingHistory(InTraining t, double prog, int turn)
        {
            var tempObj = new InTrainings()
            {
                Pid = t.Pid,
                Fid = t.Fid,
                Progression = prog,
                Turn = turn,
                DataQuality = t.DataCommitted.DataQuality,
                DataQuantity = t.DataCommitted.DataQuantity,
                ResourceQuantity = t.ResourceCommited.AssignedQty,
                BidAmount = t.AdmissionAmt
            };

            _inTrainingList.Add(tempObj);
        }
        public void AddGameInstance(SimulationSettings ss, string gid)
        {
            var gameInstance = new GameInstance()
            {
                Gid = gid,
                MarketShare = ss.FIXED_MARKET_SHARE,
                StartingAsset = ss.FIXED_STARTING_ASSET,
                MinTrainingLength = ss.MIN_TRAINING_LENGTH,
                MaxTrainingLength = ss.MAX_TRAINING_LENGTH,
                MinBidLength = ss.MIN_BID_LENGTH,
                MinProfitLength = ss.MIN_PROFIT_LENGTH,
                NumFederations = ss.NUM_OF_FEDERATIONS,
                NumPlayers = ss.NUM_OF_PLAYERS,
                MinDataQuality = ss.MIN_DATA_QUALITY,
                MaxDataQUality = ss.MAX_DATA_QUALITY,
                MinDataQuantity = ss.MIN_DATA_QUANTITY,
                MaxDataQuantity = ss.MAX_DATA_QUANTITY,
                DataQualityWeight = ss.DATA_QUALITY_WEIGHT,
                DataQuantityWeight = ss.DATA_QUANTITY_WEIGHT,
                MinResourceQuantity = ss.MIN_RESOURCE_QUANTITY,
                MaxResourceQuantity = ss.MAX_RESOURCE_QUANTITY,
                InitDataQualityTh = ss.INIT_DATA_QUALITY,
                InitDataQuantityTh = ss.INIT_DATA_QUANTITY,
                InitResourceQuantityTh = ss.INIT_RESOURCE_QUANTITY,
                InitAmountTh = ss.INIT_AMOUNT_BID,
                MaxTurns = ss.MAX_TURNS
            };

            this._gameInstance = gameInstance;
        }
        public void AddFederationParticipationHistory(Player p, int fid, double prog, int turn)
        {
            var tempObj = new FederationParticipantsHistory()
            {
                Progression = prog,
                Turn = turn,
                Pid = p.Pid,
                ResourceCommitted = p.ResourceOwned.AssignedQty,
                DataQualityCommitted = p.DataOwned.DataQuality,
                DataQuantityCommitted = p.DataOwned.DataQuantity,
                BidAmount = p.Asset,
                Fid = fid
            }; 
            _federationParticipantsHistory.Add(tempObj);
        }
        #endregion

        public void saveGameInstance()
        {
            /*
            var id = Guid.NewGuid().ToString();
            //string servername = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""H:\FYP Current\FYP_IncentiveMechanismSimulatorMVP\SimDatabase.mdf"";Integrated Security=True";
            string servername = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\FYP Current\FYP_IncentiveMechanismSimulatorMVP\SimDatabase.mdf"";Integrated Security=True";
            //string servername2 = "Server=(LocalDB)\\MSSQLLocalDB;Database=Database1.mdf;Integrated Security=True;";
            //MySqlConnection test = new MySqlConnection(servername2);
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = servername;
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = "INSERT INTO GameInstance(Gid,MarketShare,StartingAsset,MinTrainingLength,MaxTrainingLength,MinBidLength,MinProfitLength,NumFederations," +
                "NumPlayers,MinDataQuality,MaxDataQuality,MinDataQuantity,MaxDataQuantity,DataQualityWeight,DataQuantityWeight,MinResourceQuantity,MaxResourceQuantity,InitDataQualityTH,InitDataQuantityTH," +
                "InitResourceQuantityTh,InitAmountTh,MaxTurns,MarketShareFederationPct) " +
                "VALUES('" + _gameInstance.Gid + "'," + _gameInstance.MarketShare + "," + _gameInstance.StartingAsset + "," + _gameInstance.MinTrainingLength + "," + _gameInstance.MaxTrainingLength
                + "," + _gameInstance.MinBidLength + "," + _gameInstance.MinProfitLength + "," + _gameInstance.NumFederations + "," + _gameInstance.NumPlayers
                + "," + _gameInstance.MinDataQuality + "," + _gameInstance.MaxDataQUality + "," + _gameInstance.MinDataQuantity + "," + _gameInstance.MaxDataQuantity
                + "," + _gameInstance.DataQualityWeight + "," + _gameInstance.DataQuantityWeight + "," + _gameInstance.MinResourceQuantity + "," + _gameInstance.MaxResourceQuantity
                + "," + _gameInstance.InitDataQualityTh + "," + _gameInstance.InitDataQuantityTh + "," + _gameInstance.InitResourceQuantityTh + "," + _gameInstance.InitAmountTh + ","+_gameInstance.MaxTurns+","+_gameInstance.MarketShareFederationPct+ ")";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            int gid = 0;

            cmd.CommandText = "SELECT Id FROM GameInstance WHERE Gid='" + _gameInstance.Gid + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                gid = (int)reader["Id"];// Console.WriteLine("Game Instance Index "+reader["Id"]);
            reader.Close();

            foreach(Federations f in this._federationList)
            {
                cmd.CommandText = "INSERT INTO Federations(Gid,Fid,FederationAsset,SchemeAdopted,AdmissionPolicyId) VALUES ("+ 
                    gid+","+f.Fid+","+f.FederationAsset+",'"+f.SchemeAdopted+"',"+f.AdmissionPolicyId+ ")";
                cmd.ExecuteNonQuery();
            }
            foreach(Participants p in this._participantsList)
            {
                int bit = 0;
                if (p.HumanPlayer)
                    bit = 1;
                cmd.CommandText = "INSERT INTO Participants(Gid,Pid,Strategy,HumanPlayer) VALUES (" +
                    gid+","+p.Pid + ",'" + p.Strategy + "'," + bit + ")";
                cmd.ExecuteNonQuery();
            }

            foreach(Bids b in this._bidsHistory)
            {
                int bit = 0;
                if (b.Success)
                    bit = 1;
                cmd.CommandText = "INSERT INTO Bids(Gid,Pid,Fid,AmountBid,ResourceQty,DataQty,DataQuality,Success) VALUES (" +
                    gid+","+b.Pid + "," +b.Fid + "," +b.AmountBid + "," + b.AmountBid +"," +b.DataQty + "," +b.DataQuality + "," +bit +")";
                cmd.ExecuteNonQuery();
            }

            foreach(InTrainings t in this._inTrainingList)
            {
                cmd.CommandText = "INSERT INTO InTrainings(Pid,Fid,Progression,Turn,DataQuality,DataQuantity,ResourceQuantity,BidAmount,Gid) VALUES(" +
                    t.Pid+","+t.Fid + ","+t.Progression + ","+t.Turn + ","+t.DataQuality + ","+t.DataQuantity + ","+t.ResourceQuantity + ","+t.BidAmount + ","+gid+")";
                cmd.ExecuteNonQuery();
            }
            foreach(FederationHistory fh in this._federationHistory)
            {
                cmd.CommandText = "INSERT INTO FederationHistory(Fid,Progression,Turn,Asset,TimeLeftInState,State,MarketShare,ModelQuality,Gid) VALUES(" +
                    fh.Fid + "," + fh.Progression + "," + fh.Turn + "," + fh.Asset + "," + fh.TimeLeftInState + ",'" + fh.State + "',"+ 
                    fh.MarketShare +","+fh.ModelQuality + ","+ gid +")";
                cmd.ExecuteNonQuery();
            }
            foreach(FederationParticipantsHistory fph in this._federationParticipantsHistory)
            {
                cmd.CommandText = "INSERT INTO FederationParticipantsHistory(Progression,Turn,Pid,ResourceCommitted,DataQuantityCommitted,DataQualityCommitted,BidAmount,Fid,Gid) VALUES(" +
                    fph.Progression + ","+ fph.Turn + ","+ fph.Pid + ","+ fph.ResourceCommitted + ","+ fph.DataQuantityCommitted + ","+ fph.DataQualityCommitted
                    + ","+ fph.BidAmount + ","+ fph.Fid + ","+ gid+")";
                cmd.ExecuteNonQuery();
            }
            foreach(ParticipantHistory ph in this.ParticipantsHistoryList)
            {
                cmd.CommandText = "INSERT INTO ParticipantHistory(Pid,Progression,Turn,Asset,DataQuantity,DataQuality,ResourceQuantity,Gid) VALUES(" +
                    ph.Pid + ","+ ph.Progression + ","+ ph.Turn + ","+ ph.Asset + ","+ph.DataQuantity + ","+ ph.DataQuality + ","+ ph.ResourceQuantity + ","+ gid +")";
                cmd.ExecuteNonQuery();
            }
            con.Close();
            */
        }
    }
}
