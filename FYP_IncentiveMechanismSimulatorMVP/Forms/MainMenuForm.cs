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

            //Initialize Simulator Controller
            ApplicationLogic.Simulation.Instance.Init();
            //ApplicationLogic.Simulation.Instance.InitSettings();
            if (ApplicationLogic.Simulation.Instance.FIXED_SETTINGS_FLAG == 1)
                this.loadFixedSettings_chkBox.Checked = true;
            else
                this.loadFixedSettings_chkBox.Checked = false;

            //if any problems during initialization. exit
            if (ApplicationLogic.Simulation.Instance.EXIT_FLAG == -1)
            {
                //TODO add dialog message
                Environment.Exit(0);
            }
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            //reinit 
            //check if load fixed settings chkbox is set
            if (this.loadFixedSettings_chkBox.Checked)
                ApplicationLogic.Simulation.Instance.FIXED_SETTINGS_FLAG = 1;
            else
                ApplicationLogic.Simulation.Instance.FIXED_SETTINGS_FLAG = 0;
            //Initialize Simulator Controller
            ApplicationLogic.Simulation.Instance.Init();
            //ApplicationLogic.Simulation.Instance.InitSettings();

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
            //ApplicationLogic.Simulation.Instance.InitSettings();
            ViewSettingsForm form = new ViewSettingsForm();
            form.ShowDialog();
            form.Close();

            Console.WriteLine("Exitted Settings");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //ApplicationLogic.DBManager test = new ApplicationLogic.DBManager();
            Utils.IOManager temp = new Utils.IOManager();
            temp.ReInitSettings();
            
        }
    }
}
