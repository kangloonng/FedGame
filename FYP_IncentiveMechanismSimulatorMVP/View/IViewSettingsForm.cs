using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.View
{
    public interface IViewSettingsForm
    {
        Dictionary<string, List<Tuple<string, string>>> SettingsList { get; set; }
    }
}
