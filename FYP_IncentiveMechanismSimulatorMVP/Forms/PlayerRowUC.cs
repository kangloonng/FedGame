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
    public partial class PlayerRowUC : UserControl
    {
        private int humanPlayerPid;
        public PlayerRowUC(Player player, int humanPlayerPid)
        {
            InitializeComponent();
            this.humanPlayerPid = humanPlayerPid;
            this.Edit(player);
        }

        public void Edit(Player player)
        {
            string indexLblString = "";
            if (player.Pid == humanPlayerPid)
                indexLblString = player.Pid + " (You)";
            else
                indexLblString = player.Pid.ToString();

            this.playerIndex_lbl.Text = indexLblString;
            this.income_lbl.Text = "$"+(player.Asset - player.AssetInBid).ToString();
            this.resourceQty_lbl.Text = "Qty :"+player.ResourceOwned.AssignedQty.ToString() + Environment.NewLine + 
                "In Bid :"+player.ResourceOwned.InBidQty.ToString() + Environment.NewLine + 
                "In Training :"+player.ResourceOwned.InTrainingQty.ToString();
            this.dataOwned_lbl.Text = "Quality :" + player.DataOwned.DataQuality + Environment.NewLine + "Quantity :" + player.DataOwned.DataQuantity;
        }
    }
}
