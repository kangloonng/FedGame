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
    public partial class BidForm : Form, IBidForm   
    {
        private Presenters.BidFormPresenter _presenter;
        private Player _player;
        private Bid _bid;
        private int _fid;
        private InTraining _inTraining;
        private int _bidFormType;
        public event EventHandler ButtonClick;
        private List<Federation> _federationList;
        public BidForm()
        {
            InitializeComponent();
            this.DisableControls();

            this._presenter = new Presenters.BidFormPresenter(this);

            if (this._federationList.Count == 0)
            {
                DialogResult end = MessageBox.Show("No Federation currently open for bidding! Please try again later!", "Bidding Form", MessageBoxButtons.OK);
                this.Close();
                return;
            }

            for (int i = 0; i < this._federationList.Count; i++)
            {
                this.federationId_comboBox.Items.Add(this._federationList[i].FederationId);
            }

            this.federationId_comboBox.SelectedIndex = 0;
            ProcessLabels();

        }

        #region getset for interface
        public Player player 
        {
            get
            {
                return this._player;
            }
            set
            {
                this._player = value;
                
            }
        }
        public Bid playerBid
        {
            get
            {
                int fid = Convert.ToInt32(this.federationId_comboBox.SelectedItem);
                double dataQualityMultiplier = Convert.ToDouble(this.dataQuality_comboBox.SelectedItem) / 100;
                double dataQuantityMultiplier = Convert.ToDouble(this.dataQuantity_comboBox.SelectedItem) / 100;
                int assignedQuantity = Convert.ToInt32(this.resourceQuantity_comboBox.SelectedItem);
                //this._bid.DataBidMultiplier.DataQuality = Convert.ToDouble(this.dataQualityBid_txtBox.Text) / 100;
                //this._bid.DataBidMultiplier.DataQuantity = Convert.ToDouble(this.dataQuantityBid_txtBox.Text) / 100;
                //this._bid.ResourceBid.AssignedQty = Convert.ToInt32(this.resourceQualityBid_txtBox.Text);
                double bidAmnt = Convert.ToDouble(this.bidAmount_txtbox.Text.Replace("$",""));
                Bid tempBid = new Bid(this.player.Pid,fid,dataQualityMultiplier,dataQuantityMultiplier,assignedQuantity,bidAmnt);
                return tempBid;
            }
            set
            {
                this._bid = value;
            }
        }
        public int fid 
        {
            get
            {
                return 1;// Convert.ToInt32(this.federationIndex_lbl.Text.Replace(toAppendFederation,""));
            }
            set
            {
                this.fid = value;
            }
        }
        public string bidFormTitle
        {
            get
            {
                return this.formTitle_lbl.Text;
            }
            set
            {
                this.formTitle_lbl.Text = value;
            }
        }

        public InTraining playerTraining 
        {
            get
            {
                double dataQuality = (Convert.ToDouble(this.dataQuality_comboBox.SelectedItem)/100) * this._player.DataOwned.DataQuality;
                double dataQuantity =(Convert.ToDouble(this.dataQuantity_comboBox.SelectedItem)/100) * this._player.DataOwned.DataQuantity;
                int resourceQuantity = Convert.ToInt32(this.resourceQuantity_comboBox.SelectedItem);
                int fid = Convert.ToInt32(this.federationId_comboBox.SelectedItem);
                InTraining tempObj = new InTraining(this.player.Pid,fid,new Model.DataObject(dataQuality,dataQuantity),new Resource(resourceQuantity),_inTraining.AdmissionAmt);
                return tempObj;
            }
            set
            {
                this._inTraining = value;
            }
        }
        public int bidFormType
        {
            get
            {
                return this._bidFormType;
            }
            set
            {
                this._bidFormType = value;
            }
        }

        public List<Federation> federationList
        {
            set
            {
                this._federationList = value;
            }
        }
        #endregion
        #region Form-related methods
        private void EnableControls()
        {
            this.dataQuality_comboBox.Enabled = true;
            this.dataQuantity_comboBox.Enabled = true;
            this.resourceQuantity_comboBox.Enabled = true;
            this.bidAmount_txtbox.Enabled = true;
            this.bid_btn.Enabled = true;
        }
        private void DisableControls()
        {
            this.dataQuality_comboBox.Enabled = false;
            this.dataQuantity_comboBox.Enabled = false;
            this.resourceQuantity_comboBox.Enabled = false;
            this.bidAmount_txtbox.Enabled = false;
            this.bid_btn.Enabled = false;
        }
        private void ProcessLabels()
        {
            //data owned
            this.dataQuality_lbl.Text = this._player.DataOwned.DataQuality.ToString();
            this.dataQuantity_lbl.Text = this._player.DataOwned.DataQuantity.ToString();
            this.resourceQty_lbl.Text = (this._player.ResourceOwned.AssignedQty - this._player.ResourceOwned.InBidQty - this._player.ResourceOwned.InTrainingQty).ToString();
            this.asset_lbl.Text = "$" + (this._player.Asset - this._player.AssetInBid).ToString();

        }
        private void ReInit()
        {
            //repopulate
            this.dataQuality_comboBox.Items.Clear();
            this.dataQuantity_comboBox.Items.Clear();
            this.resourceQuantity_comboBox.Items.Clear();
           
            //Combobox populating
            int pctMin = 10, pctMax = 100, qtyMin = 1, qtyMax = this._player.ResourceOwned.AssignedQty - this.player.ResourceOwned.InBidQty - this.player.ResourceOwned.InTrainingQty;
            if (this._inTraining != null)
                qtyMax += this._inTraining.ResourceCommited.AssignedQty;
            else if (this._bid != null)
                qtyMax += this._bid.ResourceBid.AssignedQty;

            this.dataQuality_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.dataQuantity_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.resourceQuantity_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.federationId_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            for (int i = 0; i <= pctMax; i += pctMin)
            {
                this.dataQuality_comboBox.Items.Add(i);
                this.dataQuantity_comboBox.Items.Add(i);
            }

            for (int i = 0; i <= qtyMax; i += qtyMin)
            {
                this.resourceQuantity_comboBox.Items.Add(i);
            }

            if (this._inTraining == null | this._bid == null)
            {
                this.dataQuality_comboBox.SelectedIndex = 0;
                this.dataQuantity_comboBox.SelectedIndex = 0;
                this.bidAmount_txtbox.Text = "$0";
                this.resourceQuantity_comboBox.SelectedIndex = 0;
            }
        }
        private void ChangeInFederationSelected()
        {
            int fid = Convert.ToInt32(this.federationId_comboBox.SelectedItem);
            this._presenter.GetPlayerBid(this.player.Pid, fid);
            this._presenter.GetPlayerTraining(this.player.Pid, fid);
            this.ReInit();
            int biddedDataQuality=0, biddedDataQuantity=0, biddedResourceQuantity=0;
            if (this._inTraining == null)
            {
                if (this._bid == null)
                {
                    this._bidFormType = 0;
                }
                else
                {
                    biddedDataQuality = (int)(this._bid.DataBidMultiplier.DataQuality * 100);
                    biddedDataQuantity = (int)(this._bid.DataBidMultiplier.DataQuantity * 100);
                    biddedResourceQuantity = this._bid.ResourceBid.AssignedQty;
                    this.bidAmount_txtbox.Text = "$" + this._bid.AmountBid.ToString();
                    this.bid_btn.Text = "Edit";
                    this._bidFormType = 1;
                }
            }
            else
            {
                biddedDataQuality = (int)((_inTraining.DataCommitted.DataQuality / _player.DataOwned.DataQuality) * 100); //(int)(this._bid.DataBidMultiplier.DataQuality * 100);
                biddedDataQuantity = (int)((_inTraining.DataCommitted.DataQuantity / _player.DataOwned.DataQuantity) * 100);
                biddedResourceQuantity = this._inTraining.ResourceCommited.AssignedQty;
                this.bidAmount_txtbox.Text = this._inTraining.AdmissionAmt.ToString();// "participating"; //+ this._bid.AmountBid.ToString();
                this.bidAmount_txtbox.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Italic);

                //this.dataQuality_comboBox.DropDownStyle = ComboBoxStyle.Simple;
                this.bidAmount_txtbox.Enabled = false;
                this._bidFormType = 2;
            }

            this.dataQuality_comboBox.SelectedIndex = this.dataQuality_comboBox.FindString(biddedDataQuality.ToString());
            this.dataQuantity_comboBox.SelectedIndex = this.dataQuantity_comboBox.FindString(biddedDataQuantity.ToString());
            this.resourceQuantity_comboBox.SelectedIndex = this.resourceQuantity_comboBox.FindString(biddedResourceQuantity.ToString());
        }

        private void bid_btn_Click(object sender, EventArgs e)
        {
            int result = 0;
            if(this._bidFormType == 0)
            {
                //new bid
                result = this._presenter.AddBid(this.playerBid);
            }
            else if(this._bidFormType == 1)
            {
                //Edit of bid
                result = this._presenter.EditBid(this.playerBid);
            }
            else if (this._bidFormType == 2)
            {
                //Edit of allocated resources
                result = this._presenter.EditInTraining(this.playerTraining);
            }

            string messageBoxText ="Success";
            if (result == -1)
                messageBoxText = "Error in form";

            DialogResult end = MessageBox.Show(messageBoxText, "Bidding Form", MessageBoxButtons.OK);

            if(result!=-1)
                this.Close();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void federationId_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.EnableControls();
            this.ChangeInFederationSelected();
        }
        #endregion
    }
}
