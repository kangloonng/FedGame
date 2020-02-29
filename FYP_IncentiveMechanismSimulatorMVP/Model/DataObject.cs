using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class DataObject
    {
        public double DataQuality { get; set; }
        public double DataQuantity { get; set; }

        public DataObject(double dataQuality, double dataQuantity)
        {
            this.DataQuality = dataQuality;
            this.DataQuantity = dataQuantity;
        }

        public DataObject()
        {
            this.DataQuality = 0;
            this.DataQuantity = 0;
        }
    }
}
