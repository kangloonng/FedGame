using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FYP_IncentiveMechanismSimulatorMVP.Model;
using FYP_IncentiveMechanismSimulatorMVP.View;

namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    public partial class FederationListForm : Form, IFederationListForm
    {
        private List<Federation> _federationList;
        private List<FederationRowUC> _federationRowUCList;
        private List<int> _federationJoined;
        private List<int> _federationBidded;
        private Presenters.FederationListPresenter _presenter;


        public FederationListForm()
        {
            InitializeComponent();
            this._presenter = new Presenters.FederationListPresenter(this);
            this.PopulateUC();
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
            }
        }
        public List<int> FederationJoined
        {
            get
            {
                return this._federationJoined;
            }
            set
            {
                this._federationJoined = value;
            }
        }
        public List<int> FederationBidded
        {
            get
            {
                return this._federationBidded;
            }
            set
            {
                this._federationBidded = value;
            }
        }

        private void PopulateUC()
        {
            //reinit
            this.federationPanel.Controls.Clear();
            this._federationRowUCList = null;
            
            if (this._federationRowUCList==null)
            {
                this._federationRowUCList = new List<FederationRowUC>();

                for(int i=0; i < _federationList.Count; i++)
                {
                    FederationRowUC tempUC = new FederationRowUC(this._federationList[i]);
                    tempUC.Dock = DockStyle.Top;
                    this._federationRowUCList.Add(tempUC);
                    this.federationPanel.Controls.Add(tempUC);
                    tempUC.ButtonClick += new EventHandler(actionBtn_Click);
                    tempUC.SendToBack();
                    tempUC.BringToFront();
                    tempUC.buttonText = "Bid";
                    int indexJoined = this._federationJoined.IndexOf(this._federationList[i].FederationId);
                    if (indexJoined != -1)
                    {
                        tempUC.buttonText = "Edit";
                    }
                    else
                    {
                        int indexBid = this._federationBidded.IndexOf(this._federationList[i].FederationId);
                        if (indexBid != -1)
                        {
                            tempUC.buttonText = "Edit";
                        }
                    }
                }
            }
        }
        private BidForm _bidForm;
        protected void actionBtn_Click(object sender, EventArgs e)
        {
            /*
            Button button = sender as Button;
            Console.WriteLine(button.Name + " Clicked");

            BidForm bidForm = new BidForm(Convert.ToInt32(button.Name));
            bidForm.ButtonClick += new EventHandler(DialogBtn_Click);
            _bidForm = bidForm;

            if (button.Text.Contains("Edit"))
                _bidForm.bidFormTitle = "Edit Resources Commited";
            else
                _bidForm.bidFormTitle = "Bidding Form";

            bidForm.ShowDialog();
            this.PopulateUC();
            */
        }
        protected void DialogBtn_Click(object sender, EventArgs e)
        {
            if (_bidForm == null)
                return;
            Button button = sender as Button;
            if (this._federationRowUCList[_bidForm.playerBid.Fid-1].buttonText == "Bid")
                this._federationRowUCList[_bidForm.playerBid.Fid-1].buttonText = "Edit";
            else
            {
                if (_bidForm.playerBid.ResourceBid.AssignedQty == 0)
                    this._federationRowUCList[_bidForm.playerBid.Fid-1].buttonText = "Bid";
            }

            /*
             * If BidFormType = 1 -> Related to Bid,
             * Else if BidFormType = 2 -> Related To Training
             */

            int type = _bidForm.bidFormType, success=0;
            string messageBoxText = "";
            if (type == 1)
            {
                messageBoxText = "Bid Submitted";
                success = _presenter.AddBid(_bidForm.playerBid);
            }
            else 
            {
                messageBoxText = "Re-allocated Resources";
                _presenter.EditTrainining(_bidForm.playerTraining);
            }



            if (success == -1)
                messageBoxText = "Error in form";

            DialogResult res = MessageBox.Show(messageBoxText, "", MessageBoxButtons.OK);
            _bidForm.Close();
            
        }
    }
}
