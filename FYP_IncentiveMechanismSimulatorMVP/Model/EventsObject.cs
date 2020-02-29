using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class EventsObject
    {
        public int Turn { get; set; }
        public double Round_progression { get; set; }
        public string Text { get; set; }
        public EventsObject(int turn, double round_progression, string text)
        {
            this.Turn = turn;
            this.Round_progression = round_progression;
            this.Text = text;
        }

        public override string ToString()
        {
            return String.Format("{0,-10} {1,-15} {2}", "Turn :"+this.Turn, "Round :" + this.Round_progression, this.Text);
        }
    }
}
