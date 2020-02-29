using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class State
    {
        public double CurrentTurnProgression { get; set; }
        public int CurrentTurn { get; set; }

        public State()
        {
            this.CurrentTurnProgression = 0;
            this.CurrentTurn = 1;
        }
        public State(double progression, int turn)
        {
            this.CurrentTurn = turn;
            this.CurrentTurnProgression = progression;
        }
    }
}
