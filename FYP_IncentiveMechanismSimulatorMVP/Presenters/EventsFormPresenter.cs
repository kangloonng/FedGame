using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Presenters
{
    public class EventsFormPresenter
    {
        private View.IEventsForm _IEventsForm;

        public EventsFormPresenter(View.IEventsForm eventsForm)
        {

            this._IEventsForm = eventsForm;
            this._IEventsForm.eventsList = ApplicationLogic.Simulation.Instance.eventsManager.eventsObjList;
        }
    }
}
