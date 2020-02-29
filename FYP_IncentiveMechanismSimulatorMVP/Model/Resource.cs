using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class Resource
    {
        public int AssignedQty { get; set; }
        public int InBidQty { get; set; }
        public int InTrainingQty { get; set; }

        public Resource(int resourceQuantity)
        {
            this.AssignedQty = resourceQuantity;
            this.InBidQty = 0;
            this.InTrainingQty = 0;
        }

        public Resource()
        {
            this.AssignedQty = 0;
            this.InBidQty = 0;
            this.InTrainingQty = 0;
        }
    }
}
