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
    public partial class FederationRowInfoUC : UserControl
    {
        private Federation _f;
        public FederationRowInfoUC(Federation f)
        {
            InitializeComponent();
            _f = f;

            this.fedIdLbl.Text = f.FederationId.ToString();
            this.fedMarketshare.Text = (f.MarketShare * 100) + "%";
            this.fedNoParticipantsLbl.Text = f.ParticipantList.Count.ToString();
            this.fedSchemeLbl.Text = f.sc.SchemeName;
            this.fedStateLbl.Text = f.Current_state.ToString();
            this.fedTimeLbl.Text = f.TimeLeftInState.ToString();
        }
        public void EditLabel(Federation f)
        {
            _f = f;
            this.fedIdLbl.Text = f.FederationId.ToString();
            this.fedMarketshare.Text = (f.MarketShare * 100) + "%";
            this.fedNoParticipantsLbl.Text = f.ParticipantList.Count.ToString();
            this.fedSchemeLbl.Text = f.sc.SchemeName;
            this.fedStateLbl.Text = f.Current_state.ToString();
            this.fedTimeLbl.Text = f.TimeLeftInState.ToString();
        }
    }
}
