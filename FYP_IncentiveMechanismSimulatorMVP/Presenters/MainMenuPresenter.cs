using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.View;
namespace FYP_IncentiveMechanismSimulatorMVP.Presenters
{
    class MainMenuPresenter
    {
        IMainMenuForm mainMenuView;

        public MainMenuPresenter(IMainMenuForm view)
        {
            this.mainMenuView = view;
        }
    }
}
