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
    public partial class SummaryPageForm : Form, View.ISummaryPageForm
    {
        private List<string> _ownEventString;
        private Presenters.SummaryPagePresenter _presenter;
        public SummaryPageForm()
        {
            InitializeComponent();
            _presenter = new Presenters.SummaryPagePresenter(this);
            this.GenerateLabels();
        }

        public List<string> ownEventsString
        {
            set
            {
                _ownEventString = value;
            }
        }

        public void GenerateLabels()
        {
            Label templabel = new Label();
            templabel.Dock = DockStyle.Top;
            templabel.Font = new Font("Microsoft Sans Serif", 10);
            if (this._ownEventString == null)
            {
                //templabel.AutoSize = true;
                templabel.Text = "Nothing happened";
                templabel.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Italic);
                this.bodyPanel.Controls.Add(templabel);
                templabel.SendToBack();
                templabel.BringToFront();
            }
            else
            {
                foreach(string s in this._ownEventString)
                {
                    templabel = new Label();
                    //templabel.AutoSize = true;
                    templabel.Dock = DockStyle.Top;
                    templabel.Font = new Font("Microsoft Sans Serif", 10);
                    templabel.Text = s;

                    this.bodyPanel.Controls.Add(templabel);
                    templabel.SendToBack();
                    templabel.BringToFront();
                }
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
