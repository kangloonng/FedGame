using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Presenters
{
    public class SummaryPagePresenter
    {
        private View.ISummaryPageForm _view;
        
        public SummaryPagePresenter(View.ISummaryPageForm view)
        {
            _view = view;
            _view.ownEventsString = ApplicationLogic.Simulation.Instance.eventsManager.currentTurnEvent;
        }
    }
}
