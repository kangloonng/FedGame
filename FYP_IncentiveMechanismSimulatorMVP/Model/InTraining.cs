using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_IncentiveMechanismSimulatorMVP.Model
{
    public class InTraining
    {
        public int Pid { get; set; }
        public int Fid { get; set; }
        public double LocalTrainingLength { get; set; }
        public DataObject DataCommitted { get; set; }
        public Resource ResourceCommited { get; set; }
        public double AdmissionAmt { get; set; }
        public InTraining(int pid, int fid, DataObject dataCommitted, Resource resourceCommitted, double admissionAmt)
        {
            this.Pid = pid;
            this.Fid = fid;
            this.DataCommitted = dataCommitted;
            this.ResourceCommited = resourceCommitted;
            this.AdmissionAmt = admissionAmt;
            this.CalculateLocalTrainingLength();
        }

        private void CalculateLocalTrainingLength()
        {
            ApplicationLogic.SimulationSettings _ss = ApplicationLogic.Simulation.Instance.simulationSettings;
            Console.WriteLine("Calculating Length -> Data Quantity {0} , Resource Quantity {1}", this.DataCommitted.DataQuantity, this.ResourceCommited.AssignedQty);
            double constant = 10;
            double inverseProportion = constant * (DataCommitted.DataQuantity / this.ResourceCommited.AssignedQty); //(this.ResourceCommited.AssignedQty/10.0) / DataCommitted.DataQuantity;
            double min_range = _ss.MIN_TRAINING_LENGTH;// ApplicationLogic.Constants.MIN_LENGTH;
            double max_range = _ss.MAX_TRAINING_LENGTH; // ApplicationLogic.Constants.MAX_LENGTH;
            double min_length = constant * (_ss.MIN_DATA_QUANTITY / _ss.MAX_RESOURCE_QUANTITY); //constant * (ApplicationLogic.Constants.DATAQUANTITY_MIN / ApplicationLogic.Constants.RESOURCEQUANTITY_MAX);
            double max_length = constant * (_ss.MAX_DATA_QUANTITY / _ss.MIN_RESOURCE_QUANTITY); //(ApplicationLogic.Constants.DATAQUANTITY_MAX / ApplicationLogic.Constants.RESOURCEQUANTITY_MIN);

            //precision round up to 1 dp
            double length = Math.Round(inverseProportion, 1);
            this.LocalTrainingLength = NormalizationEqn(min_range, max_range, length, min_length,max_length);
            
        }
        private double NormalizationEqn(double min, double max, double value, double min_length, double max_length)
        {
            return Math.Round(((value - min_length) / (max_length - min_length)) * (max - min) + min,1);
        }
    }
}
