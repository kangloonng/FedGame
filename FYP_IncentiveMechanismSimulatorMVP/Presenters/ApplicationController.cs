using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Presenters;

namespace FYP_IncentiveMechanismSimulatorMVP.Presenters
{
   public sealed class ApplicationController
    {
        private static ApplicationController instance = null;
        private static readonly object padlock = new object();

        public static ApplicationController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ApplicationController();
                    }
                    return instance;
                }
            }
        }
    }
}
