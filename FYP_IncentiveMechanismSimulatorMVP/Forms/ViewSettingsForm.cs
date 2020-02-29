using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    public partial class ViewSettingsForm : Form, View.IViewSettingsForm
    {
        private Dictionary<string, List<Tuple<string, string>>> _settingsList;
        private Presenters.ViewSettingsFormPresenter _presenter;
        public ViewSettingsForm()
        {
            InitializeComponent();
            this._presenter = new Presenters.ViewSettingsFormPresenter(this);
        }
        public Dictionary<string, List<Tuple<string, string>>> SettingsList 
        {
            get
            {
                //process fields
                return this._settingsList;

            }
            set
            {
                this._settingsList = value;
                //process fields
                if(_settingsList!=null)
                this.PopulateLabels();
            }
        }

        private void PopulateLabels()
        {
            //this.bodyPanel.Margin = new Padding(150);
            foreach(KeyValuePair<string,List<Tuple<string,string>>> entryKP in this._settingsList)
            {
                Label tempLabel = new Label();
                tempLabel.AutoSize = true;
                tempLabel.ForeColor = Color.Black;
                tempLabel.Dock = DockStyle.Top;
                tempLabel.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Bold|FontStyle.Underline | FontStyle.Italic);
                tempLabel.Margin = new Padding(50);
                tempLabel.Text = entryKP.Key.Replace("_"," ");
                this.bodyPanel.Controls.Add(tempLabel);
                tempLabel.SendToBack();
                tempLabel.BringToFront();
                List <Tuple<string, string>> tempList = entryKP.Value;
                foreach(Tuple<string,string> tupEntry in tempList)
                {
                    tempLabel = new Label();
                    tempLabel.ForeColor = Color.Black;
                    tempLabel.Dock = DockStyle.Top;
                    tempLabel.Text = tupEntry.Item1.Replace("_"," ") + ":   " + tupEntry.Item2;
                    this.bodyPanel.Controls.Add(tempLabel);
                    tempLabel.SendToBack();
                    tempLabel.BringToFront();
                }
            }
        }
    }
}
