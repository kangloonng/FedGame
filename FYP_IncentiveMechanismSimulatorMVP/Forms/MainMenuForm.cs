using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FYP_IncentiveMechanismSimulatorMVP.View;
namespace FYP_IncentiveMechanismSimulatorMVP.Forms
{
    public partial class MainMenuForm : Form, IMainMenuForm
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Initialize Simulator Controller
            ApplicationLogic.Simulation.Instance.Init();
            ApplicationLogic.Simulation.Instance.InitSettings();
            GameMenuForm nextForm = new GameMenuForm(this);
            this.Hide();
            nextForm.Show();
            /*
            //Initialize Simulator Controller
            ApplicationLogic.Simulation.Instance.Init();
            ApplicationLogic.Simulation.Instance.InitSettings();
            GameMenu nextForm = new GameMenu(this);
            nextForm.StartPosition = FormStartPosition.Manual;
            nextForm.Location = this.Location;
            this.Hide();
            nextForm.Show();*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void viewSettings_btn_Click(object sender, EventArgs e)
        {
            ApplicationLogic.Simulation.Instance.InitSettings();
            ViewSettingsForm form = new ViewSettingsForm();
            form.ShowDialog();
            form.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            ApplicationLogic.DBManager test = new ApplicationLogic.DBManager();
            Utils.IOManager temp = new Utils.IOManager();
            temp.ReInitSettings();
            */
        }
    }
}
