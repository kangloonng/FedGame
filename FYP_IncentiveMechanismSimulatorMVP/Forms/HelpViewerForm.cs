using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    public partial class HelpViewerForm : Form
    {
        public HelpViewerForm()
        {
            InitializeComponent();
            Console.WriteLine(@"..\..\");
            var fileText = File.ReadAllText(@"..\..\bin\Debug\Objective.md");
            string [] text = fileText.Split('\n');
            

            foreach(string s in text)
            {
                Label temp = null;
                if ((s.Split('#').Length -1) == 1)
                {
                    temp = this.buildHeaderLabel(s,1);
                }
                else if((s.Split('#').Length - 1) == 2)
                {
                    temp = this.buildHeaderLabel(s, 2);
                }
                else
                {
                    temp = this.buildNormalLabel(s);
                }
                this.body_panel.Controls.Add(temp);
                temp.SendToBack();
                temp.BringToFront(); 
            }
        }

        private Label buildNormalLabel(string text)
        {
            Label bodyLabel = new Label();
            bodyLabel.Font = new Font("Microsoft Sans Serif", 12);
            bodyLabel.Text = text;
            bodyLabel.MaximumSize = new Size(this.body_panel.Width-10, 0);
            bodyLabel.AutoSize = true;
            bodyLabel.Dock = DockStyle.Top;
            return bodyLabel;
        }

        public Label buildHeaderLabel(string text,int type)
        {
            int fontSize = 16;
            if (type == 2)
                fontSize = 14;
            Label headerLabel = new Label();
            headerLabel.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Bold);
            headerLabel.Text = text;
            headerLabel.MaximumSize = new Size(this.body_panel.Width - 10, 0);
            headerLabel.AutoSize = true;
            headerLabel.Dock = DockStyle.Top;
            return headerLabel;
            /*
                                templabel.AutoSize = true;
            templabel.Text = "Nothing happened";
            templabel.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Italic);
            this.bodyPanel.Controls.Add(templabel);
            templabel.SendToBack();
            templabel.BringToFront();*/
        }
    }
}
