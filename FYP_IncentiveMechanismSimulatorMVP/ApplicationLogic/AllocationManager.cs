using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FYP_IncentiveMechanismSimulatorMVP.Model;

namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class AllocationManager
    {
        public List<Tuple<DataObject, Resource,double>> HandList;

        public AllocationManager()
        {
            this.HandList = new List<Tuple<DataObject, Resource,double>>();
        }

        public void GenerateDifferentHands(int numOfPossibilities, SimulationSettings simulationSettings)
        {
            //double dataQuality, min = (Constants.DATAQUALITY_MIN+0.2), max = Constants.DATAQUALITY_MAX - 0.1;
            double dataQuality = 0, dataqlty_min = simulationSettings.MIN_DATA_QUALITY+0.1, dataqlty_max = simulationSettings.MAX_DATA_QUALITY-0.1;
            double startingAsset = simulationSettings.FIXED_STARTING_ASSET;
            double dataQuantity = 0, dataqty_min = simulationSettings.MIN_DATA_QUANTITY + 0.1, dataqty_max = simulationSettings.MAX_DATA_QUANTITY - 0.1;
            int resourceQuantity = 0, resourceqty_min = simulationSettings.MIN_RESOURCE_QUANTITY, resourceqty_max = simulationSettings.MAX_RESOURCE_QUANTITY;
            Random ran = new Random();
            for(int i=0; i < numOfPossibilities; i++)
            {
                //dataQuantity = ran.Next((Constants.DATAQUANTITY_MAX / 2) - 3, (Constants.DATAQUANTITY_MAX / 2) + 3);
                dataQuality = dataqlty_min + (dataqlty_max - dataqlty_min) * ran.NextDouble();
                dataQuality = Math.Round(dataQuality, 2);
                dataQuantity = dataqlty_min + (dataqty_max - dataqty_min) * ran.NextDouble();
                dataQuantity = Math.Round(dataQuantity, 2);
                resourceQuantity = ran.Next(resourceqty_min+1, resourceqty_max-1);

                Tuple<DataObject, Resource,double> tempTuple = new Tuple<DataObject,Resource,double>(new DataObject(dataQuality,dataQuantity), new Resource(resourceQuantity), startingAsset);
                this.HandList.Add(tempTuple);
                Console.WriteLine("{0}. (Data) Quality -> {1}, Quantity -> {2} : Resource Quantity -> {3} : Starting Asset -> {4}"
                    ,i, dataQuality, dataQuantity, resourceQuantity,simulationSettings.FIXED_STARTING_ASSET);
            }
        }
    }
}
