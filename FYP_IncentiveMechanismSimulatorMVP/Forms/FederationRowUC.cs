using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FYP_IncentiveMechanismSimulatorMVP.Model;
namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    public partial class FederationRowUC : UserControl
    {
        public event EventHandler ButtonClick;
        public FederationRowUC(Federation federation)
        {
            InitializeComponent();

            this.federationIndex_lbl.Text = federation.FederationId.ToString();
            this.trainingQuality_lbl.Text = (federation.CollabTrainingQuality*100).ToString()+"%";
            this.marketshare_lbl.Text = (federation.MarketShare*100).ToString()+"%";
            this.scheme_lbl.Text = federation.SchemeName;
            this.participants_lbl.Text = federation.ParticipantString();//.Count.ToString(); //.ParticipantString();
            this.action_btn.Name = this.federationIndex_lbl.Text;
            this.state_lbl.Text = federation.Current_state.ToString();
            double length = federation.TimeLeftInState;
            this.timeLength_lbl.Text = length == double.PositiveInfinity || length == 0 ? "-" : length.ToString();

            if (this.state_lbl.Text.Equals("BID_ROUND"))
                this.action_btn.Enabled = true;
            else
            {
                this.action_btn.Enabled = false;
            }

            this.panel1.HorizontalScroll.Maximum = 0;
            this.panel1.AutoScroll = false;
            this.panel1.VerticalScroll.Visible = false;
            this.panel1.AutoScroll = true;
        }
        public string buttonText
        {
            get
            {
                return this.action_btn.Text;
            }
            set
            {
                this.action_btn.Text = value;
            }
        }
        public Boolean buttonEnable
        {
            get
            {
                return this.action_btn.Enabled;
            }
            set
            {
                this.action_btn.Enabled = value;
            }
        }
        private void action_btn_Click(object sender, EventArgs e)
        {
            if (this.ButtonClick != null)
            {
                this.ButtonClick(this.action_btn, e);
            }
        }
    }
}
