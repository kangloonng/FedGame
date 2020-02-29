using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic;
namespace FYP_IncentiveMechanismSimulatorMVP.Presenters
{
    public class ViewSettingsFormPresenter
    {
        private View.IViewSettingsForm _view = null;
        public ViewSettingsFormPresenter(View.IViewSettingsForm view)
        {
            _view = view;
            _view.SettingsList = Simulation.Instance.simulationSettings.settingsList;
        }


    }
}
