using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FYP_IncentiveMechanismSimulatorMVP.Model;
using FYP_IncentiveMechanismSimulatorMVP.Presenters;
using System.Windows.Forms.DataVisualization.Charting;

namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    public partial class GameMenuForm : Form, View.IGameMenuForm
    {
        private Form _directBackToMain;
        private GameMenuPresenter _presenter;
        private List<string> _prevTurnEvents;
        private List<Label> _stringLabelTest;
        private List<Player> _playerList;
        private List<Federation> _federationList;
        private List<BidModel> _playerBidList;
        private List<Bid> _bidList;
        private List<PlayerRowUC> _playerRowUCList;
        private List<double> _federationMarketShare;
        private List<string> _federationNameList;
        private State _state;
        private List<FederationRowInfoUC> _federationRowUCList;
        private List<ParticipantHistory> _historyOfParticipants;
        private Player _currentPlayer;

        public GameMenuForm(Form parentForm)
        {
            _directBackToMain = parentForm;
            InitializeComponent();
            _presenter = new GameMenuPresenter(this);
            this.UpdateForm();
        }
        private void UpdateForm()
        {
            this._presenter.Update();
            this.UpdateLabels();
            this.UpdateCharts();
            this.ProcessStatLabel();
        }
        private void UpdateCharts()
        {
            //this.comboBox1.SelectedIndex = 0;
            #region Player Graph
            List<Tuple<int, double, double>> profitHistoryPlayer = new List<Tuple<int, double, double>>();
            this.columnChart1.Series.Clear();
            this.columnChart1.ChartAreas.Clear();
            //column chart
            Series dataQuality = new Series();
            Series dataQuantity = new Series();
            dataQuality.ChartType = SeriesChartType.Column;
            dataQuantity.ChartType = SeriesChartType.Column;
            dataQuality.Name = "Data Quality";
            dataQuantity.Name = "Data Quantity";
            double playerDataQuality = this._currentPlayer.DataOwned.DataQuality, playerDataQuantity = this._currentPlayer.DataOwned.DataQuantity;
            int numLowerDQlty = 0, numLowerDQty = 0;
            for (int i = 0; i < this._playerList.Count; i++)
            {
                Player tempPlayer = this._playerList[i];
                dataQuality.Points.AddXY(tempPlayer.Pid, tempPlayer.DataOwned.DataQuality);
                dataQuantity.Points.AddXY(tempPlayer.Pid, tempPlayer.DataOwned.DataQuantity);

                if (tempPlayer.DataOwned.DataQuality < playerDataQuality)
                    numLowerDQlty++;

                if (tempPlayer.DataOwned.DataQuantity < playerDataQuantity)
                    numLowerDQty++;
            }

            this.columnChart1.Series.Add(dataQuality);
            this.columnChart1.Series.Add(dataQuantity);
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Title = "Player Id";
            chartArea.AxisY.Title = "% (in market)";
            chartArea.AxisX.TitleFont = new Font("Arial", 12);
            chartArea.AxisY.TitleFont = new Font("Arial", 12);
            this.columnChart1.ChartAreas.Add(chartArea);
            this.columnChart1.ChartAreas[0].AxisX.Maximum = this._playerList.Count+1;

            this.columnChart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            this.columnChart1.ChartAreas[0].CursorX.AutoScroll = true;
            this.columnChart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

            double percentileDQlty = Math.Round(((double)numLowerDQlty / this._playerList.Count) * 100,2);
            double percentileDQty = Math.Round(((double)numLowerDQty / this._playerList.Count) * 100,2);

            this.dataQltyPctLbl.Text = percentileDQlty.ToString()+"%";
            this.dataQtyPctLbl.Text = percentileDQty.ToString()+"%";

            this.dataQltyPctLbl.ForeColor = Color.Red;
            this.dataQtyPctLbl.ForeColor = Color.Red;

            if (percentileDQlty > 50)
                this.dataQltyPctLbl.ForeColor = Color.Green;
            if (percentileDQty > 50)
                this.dataQtyPctLbl.ForeColor = Color.Green;


            //line chart for comparison of assets between top 3 players excluding player
            //Series playerAssetHistory = new Series();
            //playerAssetHistory.Name = "Own";
            this.lineComparisonChart.Series.Clear();
            this.lineComparisonChart.ChartAreas.Clear();

            int rangeMin = 0;
            int rangeMax = 500;
            int turnMax = 10;
            Series sfiller = this.lineComparisonChart.Series.Add("filler");
            sfiller.Color = Color.Transparent;
            sfiller.IsVisibleInLegend = false;
            sfiller.ChartType = SeriesChartType.Point;
            sfiller.Points.AddXY(1, rangeMin + 100);
            sfiller.Points.AddXY(turnMax, rangeMax - 100);

            ChartArea chartArea2 = new ChartArea();
            chartArea2.AxisX.Title = "Progressed Turns";
            chartArea2.AxisY.Title = "Profit gained/ loss ($)";
            chartArea2.AxisX.TitleFont = new Font("Arial", 12);
            chartArea2.AxisY.TitleFont = new Font("Arial", 12);
            lineComparisonChart.ChartAreas.Add(chartArea2);
            lineComparisonChart.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            lineComparisonChart.ChartAreas[0].AxisY.MajorGrid.Interval = 200;
            lineComparisonChart.ChartAreas[0].AxisX.IsMarginVisible = false;
            lineComparisonChart.ChartAreas[0].AxisX.Minimum = 0;
            lineComparisonChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            lineComparisonChart.ChartAreas[0].CursorX.AutoScroll = true;
            lineComparisonChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            lineComparisonChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            lineComparisonChart.ChartAreas[0].CursorY.AutoScroll = true;
            lineComparisonChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            var sortedList = this._playerList.Where(p => p.Pid != this._currentPlayer.Pid).ToList();
            List<Tuple<int, double>> sumList = new List<Tuple<int, double>>();
            foreach(Player p in sortedList)
            {
                int pid = p.Pid;
                double currentProfit = p.ProfitHistory[p.ProfitHistory.Count - 1].Item3;

                sumList.Add(new Tuple<int, double>(pid, currentProfit));
            }

            sumList = sumList.OrderByDescending(p => p.Item2).ToList();

            int maxPlayer = 3;
            if ((this._playerList.Count-1)<maxPlayer)
                maxPlayer = this._playerList.Count-1;

            for (int i=0; i<maxPlayer; i++)
            {
                Player tempObj = this._playerList.Where(p => p.Pid == sumList[i].Item1).FirstOrDefault();
                if (tempObj == null)
                    continue;

                Series playerAssetHistory = new Series();
                playerAssetHistory.BorderWidth = 3;
                playerAssetHistory.MarkerStyle = MarkerStyle.Circle;
                playerAssetHistory.MarkerSize = 5;
                playerAssetHistory.ChartType = SeriesChartType.Line;

                playerAssetHistory.Name = "Player "+tempObj.Pid.ToString();
                
                foreach(Tuple<double,double,double> tempTup in tempObj.ProfitHistory)
                {
                    playerAssetHistory.Points.AddXY(tempTup.Item1, tempTup.Item3);
                    
                }
                this.lineComparisonChart.Series.Add(playerAssetHistory);
            }

            //add current player's
            Series currentPlayerAssetHistory = new Series();
            currentPlayerAssetHistory.BorderWidth = 3;
            currentPlayerAssetHistory.MarkerStyle = MarkerStyle.Circle;
            currentPlayerAssetHistory.MarkerSize = 5;
            currentPlayerAssetHistory.ChartType = SeriesChartType.Line;

            currentPlayerAssetHistory.Name = "(Own) Player " + _currentPlayer.Pid.ToString();

            foreach (Tuple<double, double, double> tempTup in _currentPlayer.ProfitHistory)
            {
                currentPlayerAssetHistory.Points.AddXY(tempTup.Item1, tempTup.Item3);
            }
            this.lineComparisonChart.Series.Add(currentPlayerAssetHistory);
            double latestProfit = this._currentPlayer.ProfitHistory[this._currentPlayer.ProfitHistory.Count - 1].Item3;
            this.totalProfitLbl.Text = "$" + latestProfit;
            this.totalProfitLbl.ForeColor = Color.Green;

            if (latestProfit <= 0)
                this.totalProfitLbl.ForeColor = Color.Red;

            int rank = -1;
            for(int i = 0; i < sumList.Count; i++)
            {
                Console.WriteLine(String.Format("{2}, {0} vs {1}",sumList[i].Item2,latestProfit,i+1));
                if (latestProfit >= sumList[i].Item2)
                {
                    rank = i + 1;
                    break;
                }
            }
            if (rank == -1)
                rank = sumList.Count+1;

            Console.WriteLine("Ranked " + rank);

            this.ranked_lbl.Text = rank.ToString();
            this.outOf_lbl.Text = String.Format("out of {0} players", this._playerList.Count);

            #endregion
            Console.WriteLine();

            #region Federation Graph
            this.FederationAvgGraph();

            /*
            //Box and whisker
            this.chart2.Series.Clear();
            foreach(Federation f in this._federationList)
            {
                if (f.ParticipantList.Count == 0)
                    continue;
                Series boxAndWhiskerSeries = new Series();
                boxAndWhiskerSeries.ChartType = SeriesChartType.BoxPlot;
                List<double> listPartDQ = new List<double>();
                foreach(Player p in f.ParticipantList)
                {
                    listPartDQ.Add(p.DataOwned.DataQuality);
                }
                double min = listPartDQ.Min();
                double max = listPartDQ.Max();
                double avg = listPartDQ.Average();
                double tfifthpct = this.Percentile(listPartDQ, 25);
                double fifthypct = this.Percentile(listPartDQ, 50);
                double sfithpct = this.Percentile(listPartDQ, 75);
                double[] dataPoints = new double[]
                {
                    min,
                    max,
                    tfifthpct,
                    sfithpct,
                    avg,
                    fifthypct
                };
                boxAndWhiskerSeries.Points.Add(new DataPoint(f.FederationId,(dataPoints)));
                boxAndWhiskerSeries.Name = "Federation " + f.FederationId;
                this.chart2.Series.Add(boxAndWhiskerSeries);
                
            }
            */

            #endregion

        }

        private void FederationAvgGraph()
        {
            //Column Chart
            this.chart2.Series.Clear();
            this.chart2.ChartAreas.Clear();
            List<Tuple<int, double>> participantAvgData = new List<Tuple<int, double>>();
            List<Tuple<int, double>> participantAvgDataQty = new List<Tuple<int, double>>();
            foreach (Federation f in this._federationList)
            {
                int fid = f.FederationId;
                double avg = 0, avgDataQty=0;
                if (f.ParticipantList.Count != 0)
                {
                    avg = f.ParticipantList.Average(p => p.DataOwned.DataQuality);
                    avgDataQty = f.ParticipantList.Average(p => p.DataOwned.DataQuantity);

                    avg = Math.Round(avg, 3);
                    avgDataQty = Math.Round(avgDataQty, 3);
                }
                
                participantAvgData.Add(new Tuple<int, double>(fid,avg));
                participantAvgDataQty.Add(new Tuple<int, double>(fid, avgDataQty));
            }

            Series dataQualityAvg = new Series();
            Series dataQuantityAvg = new Series();
            dataQualityAvg.ChartType = SeriesChartType.Column;
            dataQuantityAvg.ChartType = SeriesChartType.Column;
            dataQualityAvg.Name = "Avg quality of data of Participants";
            dataQuantityAvg.Name = "Avg quantity of data of Participants";

            dataQualityAvg.IsValueShownAsLabel = true;
            dataQuantityAvg.IsValueShownAsLabel = true;

            for(int i = 0; i < participantAvgData.Count; i++)
            {
                Tuple<int, double> tup = participantAvgData[i];
                dataQualityAvg.Points.AddXY(tup.Item1, tup.Item2);
                if (tup.Item2 <= 0)
                {
                    dataQualityAvg.Points[i].IsEmpty = true;
                }
            }

            for (int i = 0; i < participantAvgDataQty.Count; i++)
            {
                Tuple<int, double> tup = participantAvgDataQty[i];
                dataQuantityAvg.Points.AddXY(tup.Item1, tup.Item2);
                if (tup.Item2 <= 0)
                {
                    dataQuantityAvg.Points[i].IsEmpty = true;
                }
            }

            /*
            foreach (Tuple<int, double> tup in participantAvgData)
            {
                dataQualityAvg.Points.AddXY(tup.Item1, tup.Item2);
            }

            foreach (Tuple<int, double> tup in participantAvgDataQty)
            {
                dataQuantityAvg.Points.AddXY(tup.Item1, tup.Item2);
            }*/
            this.chart2.Series.Add(dataQualityAvg);
            this.chart2.Series.Add(dataQuantityAvg);

            ChartArea chartArea3 = new ChartArea();
            chartArea3.AxisX.Title = "Federation Id";
            chartArea3.AxisY.Title = "Avg. values of Participants";
            chartArea3.AxisX.TitleFont = new Font("Arial", 12);
            chartArea3.AxisY.TitleFont = new Font("Arial", 12);
            this.chart2.ChartAreas.Add(chartArea3);
            this.chart2.ChartAreas[0].AxisX.Minimum = 0;
            this.chart2.ChartAreas[0].AxisX.Maximum = this._federationList.Count + 1;
            this.chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            this.chart2.ChartAreas[0].CursorX.AutoScroll = true;
            this.chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

            this.chart2.ChartAreas[0].AxisY.Minimum = 0;
            this.chart2.ChartAreas[0].AxisY.Maximum = 1;
        }


        private void FederationMarketSharePieChart()
        {
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.Titles.Clear();
            this._federationNameList.Clear();
            this._federationMarketShare.Clear();
            Series pieChart = new Series();
            pieChart.ChartType = SeriesChartType.Pie;


            if (this._federationList == null)
                return;
            for (int i = 0; i < this._federationList.Count; i++)
            {
                Federation tempFed = this._federationList[i];
                this._federationNameList.Add(tempFed.FederationId.ToString());
                this._federationMarketShare.Add(tempFed.MarketShare);
            }
            pieChart.Points.DataBindXY(this._federationNameList, this._federationMarketShare);
            foreach (DataPoint p in pieChart.Points)
            {
                p.Label = p.Label + Environment.NewLine + "(Federation " + p.LegendText + ")";
            }

            this.chart2.Series.Add(pieChart);
            this.chart2.Titles.Add("Pie Chart");
        }

        private double calStandardDeviation(List<double> values)
        {
            double mean = values.Average();
            double ssd = values.Select(val => (val - mean) * (val - mean)).Sum();
            double sdev = Math.Sqrt(ssd / values.Count);
            return sdev;
        }
        /*
         * Reference from: https://stackoverflow.com/questions/41413544/calculate-percentile-from-a-long-array
         */
        private double Percentile(List<double> listPartDQ, double Percentile)
        {
            listPartDQ.Sort();
            int Index = (int)Math.Ceiling(((double)Percentile / (double)100) * (double)listPartDQ.Count);
            return listPartDQ[Index - 1];
        }
        private void UpdateLabels()
        {
            //Top Right Panel -> Federation List
            if(this._federationRowUCList == null)
            {
                this._federationRowUCList = new List<FederationRowInfoUC>();
                for(int i=0; i < this._federationList.Count; i++)
                {
                    FederationRowInfoUC tempUC = new FederationRowInfoUC(this._federationList[i]);
                    tempUC.Dock = DockStyle.Top;
                    this.panel15.Controls.Add(tempUC);
                    this._federationRowUCList.Add(tempUC);
                    tempUC.SendToBack();
                    tempUC.BringToFront();                    
                }
            }
            else
            {
                for (int i = 0; i < this._federationRowUCList.Count; i++)
                {
                    this._federationRowUCList[i].EditLabel(this._federationList[i]);
                }
            }

            //Bottom Right Panel -> Summary
            //summary panel
            if (bodyPanelBR.Controls.Count != 0)
                bodyPanelBR.Controls.Clear();

            if(this._prevTurnEvents.Count == 0)
            {
                Label tempLabel = new Label();
                tempLabel.Dock = DockStyle.Top;
                tempLabel.Font = new Font("Arial", 14, FontStyle.Italic);
                tempLabel.Text = "Nil records";
                this.bodyPanelBR.Controls.Add(tempLabel);
            }

            foreach (string s in this._prevTurnEvents)
            {
                if (s == "")
                    continue;

                Label tempLabel = new Label();
                tempLabel.Font = new Font("Arial", 12);
                tempLabel.Dock = DockStyle.Top;
                tempLabel.Text = s;
                tempLabel.ForeColor = Color.Black;
                bodyPanelBR.Controls.Add(tempLabel);
                tempLabel.SendToBack();
                tempLabel.BringToFront();
            }

            //Quick View Panel
            #region Quick View Panel
            if (this.quickViewPanel.Controls.Count == 0)
            {
                var prevIndexPanel = -1;
                for (int i = 0; i < this._federationList.Count; i++)
                {
                    Panel fedQK_panel = new Panel();
                    fedQK_panel.Name = ""+i;
                    //fedQK_panel.Dock = DockStyle.Top;

                    Label federationText = new Label();
                    Label federationStatusText = new Label();
                    Label playerJoined = new Label();
                    Label timeLeft = new Label();
                    Federation temp = this._federationList[i];
                    Player humanPlayer = this._playerList[0];
                    federationText.Text = "Federation " + temp.FederationId;
                    string status = "";
                    switch (temp.Current_state)
                    {
                        case ApplicationLogic.StateEnum.BID_ROUND:
                            {
                                status = "Open for bids";
                                if (temp.TimeLeftInState != _presenter.GetInBidTime())
                                    status = "Processing bids...";
                                break;
                            }
                        case ApplicationLogic.StateEnum.TRAIN_ROUND:
                            {
                                status = "In Training";
                                break;
                            }
                        case ApplicationLogic.StateEnum.PROFIT_ROUND:
                            {
                                status = "Calculating Profits";
                                break;
                            }
                    }
                    federationStatusText.Text = status;
                    string playerJoinedText = "";
                    Bid tempBidObj = this._bidList.Where(p => p.Pid == humanPlayer.Pid && p.Fid == temp.FederationId).FirstOrDefault();
                    Player tempPlayerObj = null;
                    if (tempBidObj == null) {
                        tempPlayerObj = temp.ParticipantList.Where(p => p.Pid == humanPlayer.Pid).FirstOrDefault();
                        playerJoinedText = ((tempPlayerObj== null ? "Not Participating" : "Participating"));
                    }
                    else
                        playerJoinedText = "Bid submitted";

                    int numResources = 0;

                    if(playerJoinedText != "Not Participating")
                    {
                        if (tempBidObj == null)
                        {
                            if (tempPlayerObj != null)
                                numResources = tempPlayerObj.ResourceOwned.AssignedQty;
                        }
                        else
                            numResources = tempBidObj.ResourceBid.AssignedQty;
                    }

                    playerJoined.Text = playerJoinedText;
                    timeLeft.Text = temp.TimeLeftInState.ToString() + " (time left)";

                    federationText.Font = new Font("Microsoft Sans Serif", 12);
                    federationStatusText.Font = new Font("Microsoft Sans Serif", 12);
                    timeLeft.Font = new Font("Microsoft Sans Serif", 12);
                    playerJoined.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Italic);
                    if (playerJoined.Text.Contains("Not"))
                        playerJoined.ForeColor = Color.Red;
                    else
                        playerJoined.ForeColor = Color.Green;

                    //DistributionOfResourcesUC tempUCDistr = new DistributionOfResourcesUC(numResources);


                    federationText.ForeColor = Color.Black;
                    federationStatusText.ForeColor = Color.Black;
                    timeLeft.ForeColor = Color.Black;

                    federationText.Dock = DockStyle.Left;
                    federationStatusText.Dock = DockStyle.Left;
                    playerJoined.Dock = DockStyle.Fill;
                    timeLeft.Dock = DockStyle.Left;
                    //tempUCDistr.Dock = DockStyle.Left;

                    fedQK_panel.Controls.Add(federationText);
                    var prevIndex = fedQK_panel.Controls.IndexOf(federationText);
                    fedQK_panel.Controls.Add(federationStatusText);
                    fedQK_panel.Controls.SetChildIndex(federationStatusText, prevIndex);
                    prevIndex = fedQK_panel.Controls.IndexOf(federationStatusText);
                    fedQK_panel.Controls.Add(timeLeft);
                    fedQK_panel.Controls.SetChildIndex(timeLeft, prevIndex);
                    prevIndex = fedQK_panel.Controls.IndexOf(timeLeft);
                    fedQK_panel.Controls.Add(playerJoined);
                    fedQK_panel.Controls.SetChildIndex(playerJoined, prevIndex);
                    //prevIndex = fedQK_panel.Controls.IndexOf(playerJoined);
                    //fedQK_panel.Controls.Add(tempUCDistr);
                    //fedQK_panel.Controls.SetChildIndex(tempUCDistr, prevIndex);


                    federationText.AutoSize = true;
                    federationStatusText.AutoSize = true;
                    playerJoined.AutoSize = true;
                    timeLeft.AutoSize = true;

                    fedQK_panel.AutoSize = true;
                    this.quickViewPanel.Controls.Add(fedQK_panel);
                    /*
                    if (prevIndexPanel != -1)
                    {
                        this.quickViewPanel.Controls.SetChildIndex(fedQK_panel, prevIndexPanel);
                        prevIndexPanel = this.quickViewPanel.Controls.IndexOf(this.quickViewPanel.Controls[i]);
                    }
                    else
                    {
                        this.quickViewPanel.Controls.SetChildIndex(fedQK_panel, -1);
                        prevIndexPanel = this.quickViewPanel.Controls.IndexOf(fedQK_panel);
                        Console.WriteLine("Index " + prevIndexPanel);
                    }*/

                    //fedQK_panel.SendToBack();
                    //fedQK_panel.BringToFront();
                }
            }
            else
            {
                Player humanPlayer = this._playerList[0];
                int fedIndex = 0;
                //reverse order
                //for (int i = this.quickViewPanel.Controls.Count - 1; i >= 0; i--)
                //normal order
                for(int i=0; i<this.quickViewPanel.Controls.Count;i++)
                {
                    Federation temp = this._federationList[fedIndex];

                    string status = "";
                    string playerJoined = "";
                    switch (temp.Current_state)
                    {
                        case ApplicationLogic.StateEnum.BID_ROUND:
                            {
                                status = "Open for bids";
                                break;
                            }
                        case ApplicationLogic.StateEnum.TRAIN_ROUND:
                            {
                                status = "In Training";
                                break;
                            }
                        case ApplicationLogic.StateEnum.PROFIT_ROUND:
                            {
                                status = "Calculating Profits";
                                break;
                            }
                    }
                    string playerJoinedText = "";
                    Bid tempBidObj = this._bidList.Where(p => p.Pid == humanPlayer.Pid && p.Fid == temp.FederationId).FirstOrDefault();
                    Player tempPlayerObj = null;
                    if (tempBidObj == null)
                    {
                        tempPlayerObj = temp.ParticipantList.Where(p => p.Pid == humanPlayer.Pid).FirstOrDefault();
                        playerJoinedText = ((tempPlayerObj == null ? "Not Participating" : "Participating"));
                    }
                    else
                        playerJoinedText = "Bid submitted";

                    int numResources = 0;

                    if (playerJoinedText != "Not Participating")
                    {
                        if (tempBidObj == null)
                        {
                            if (tempPlayerObj != null)
                                numResources = tempPlayerObj.ResourceOwned.AssignedQty;
                        }
                        else
                            numResources = tempBidObj.ResourceBid.AssignedQty;
                    }

                    //DistributionOfResourcesUC tempControl = (DistributionOfResourcesUC)this.quickViewPanel.Controls[i].Controls[0];
                    //tempControl.EditResourceLbl(numResources);
                    this.quickViewPanel.Controls[i].Controls[3].Text = "Federation " + temp.FederationId;
                    this.quickViewPanel.Controls[i].Controls[2].Text = status;
                    this.quickViewPanel.Controls[i].Controls[1].Text = temp.TimeLeftInState + " (time left)";
                    this.quickViewPanel.Controls[i].Controls[0].Text = playerJoinedText;


                    if (this.quickViewPanel.Controls[i].Controls[0].Text.Contains("Not"))
                        this.quickViewPanel.Controls[i].Controls[0].ForeColor = Color.Red;
                    else
                        this.quickViewPanel.Controls[i].Controls[0].ForeColor = Color.Green;

                    fedIndex++;
                }
            }
            this.quickViewPanel.HorizontalScroll.Visible = true;
            this.quickViewPanel.VerticalScroll.Visible = false;
            #endregion            
        }

        #region getset for interface
        public double MarketShare
        {
            get
            {
                return 1;
            }
            set
            {
                //gameTurn_lbl.Text = value.ToString();
            }
        }


        public List<Player> PlayerList
        {
            get
            {
                return this._playerList;
            }
            set
            {
                this._playerList = value;
                /*
                if (this._playerRowUCList == null)
                {
                    this._playerRowUCList = new List<PlayerRowUC>();

                    int pid = presenter.getHumanPlayerId();
                    for (int i = 0; i < value.Count(); i++)
                    {
                        PlayerRowUC tempUC = new PlayerRowUC(value[i], pid);
                        tempUC.Dock = DockStyle.Top;
                        this._playerRowUCList.Add(tempUC);
                        this.playerList_panel.Controls.Add(tempUC);
                        tempUC.SendToBack();
                        tempUC.BringToFront();
                    }
                }
                else
                {
                    for (int i = 0; i < value.Count(); i++)
                    {
                        this._playerRowUCList[i].Edit(value[i]);
                    }
                }*/

    }
}

        public List<Federation> FederationList
        {
            get
            {
                return this._federationList;
            }
            set
            {
                this._federationList = value;
                this._federationNameList = new List<string>();
                this._federationMarketShare = new List<double>();
            }
        }

        public State CurrentState
        {
            get
            {
                return this._state;
            }
            set
            {
                this.turnLbl.Text = "TURN :" + value.CurrentTurn.ToString();
                this.progressionLbl.Text = "PROGRESSION :"+ value.CurrentTurnProgression.ToString() + "/ 1.0";
            }
        }
        public List<Bid> PlayerBidList
        {
            get
            {
                return null;
            }
            set
            {
                this._bidList = value;
                if (this._playerBidList == null)
                    this._playerBidList = new List<BidModel>();
                if (value.Count == 0)
                    this._playerBidList.Clear();

                foreach (Bid b in value)
                {
                    this._playerBidList.Add(new BidModel(b));
                }
            }
        }

        public List<string> PreviousTurnEvents
        {
            get
            {
                return this._prevTurnEvents;
            }
            set
            {
                this._prevTurnEvents = value;
            }
        }

        public List<ParticipantHistory> ParticipantHistoryList 
        {
            set
            {
                this._historyOfParticipants = value;
            }
        }

        public Player CurrentPlayer 
        {
            set
            {
                this._currentPlayer = value;
            }
        }
        #endregion

        private void GameMenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._directBackToMain.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form _bidForm;
            BidForm bidForm = new BidForm();
            //bidForm.ButtonClick += new EventHandler(DialogBtn_Click);
            _bidForm = bidForm;

            if(!bidForm.IsDisposed)
                bidForm.ShowDialog();

            Console.WriteLine("Dialog closed");
            this.UpdateForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Confirm end turn?", "End Turn", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                int result = _presenter.ProcessEndRound();
                this.UpdateForm();

                //SummaryPageForm tempForm = new SummaryPageForm();
                //tempForm.ShowDialog();

                //check for turn num
                if (result != 1)
                {
                    DialogResult end = MessageBox.Show("Max Turn has reached. Ending game and saving data to database", "End Game", MessageBoxButtons.OK);
                    //DB Access and Save.
                    _presenter.SaveDB();
                    this.returnToMainMenu();
                }

            }
            else if (res == DialogResult.No)
            {

            }
        }
        private void returnToMainMenu()
        {
            this.Close();
            this._directBackToMain.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void ProcessStatLabel()
        {
            Player humanPlayer = this._currentPlayer;
            //Player always has first index
            
            this.stat_label.Text = "ID: " + humanPlayer.Pid + Environment.NewLine
                + "Current Resource Quantity: " + (humanPlayer.ResourceOwned.AssignedQty - humanPlayer.ResourceOwned.InBidQty - humanPlayer.ResourceOwned.InTrainingQty).ToString() +
                "  (In used:" + (humanPlayer.ResourceOwned.InBidQty + humanPlayer.ResourceOwned.InTrainingQty) + ")"
                + Environment.NewLine
                + "Assigned Data: Quality " + humanPlayer.DataOwned.DataQuality + " : Quantity " + humanPlayer.DataOwned.DataQuantity + Environment.NewLine
                + "Current Asset: $" + (humanPlayer.Asset - humanPlayer.AssetInBid)
                + ((humanPlayer.AssetInBid != 0) ? "  (In Bid: $" + humanPlayer.AssetInBid + ")" : "");
                
            Console.WriteLine();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HelpViewerForm form = new HelpViewerForm();
            form.ShowDialog();
        }
    }
}
