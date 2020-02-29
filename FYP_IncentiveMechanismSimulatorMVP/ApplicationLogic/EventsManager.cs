﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public  class EventsManager
    {
        public List<Model.EventsObject> eventsObjList { get; set; }
        public List<string> currentTurnEvent { get; set; }
        public List<string> PreviousTurnEvent { get; set; }
        public EventsManager()
        {
            this.eventsObjList = new List<Model.EventsObject>();
            this.currentTurnEvent = new List<string>();
            this.PreviousTurnEvent = new List<string>();
        }

        public void CreateEventObj<T>(int turn, double progression, T obj)
        {
            Model.EventsObject tempObj = new Model.EventsObject(turn, progression, obj.ToString());
            this.eventsObjList.Add(tempObj);
        }

        public void CreateEventString(string eventText)
        {
            this.currentTurnEvent.Add(eventText);
        }
    }
}