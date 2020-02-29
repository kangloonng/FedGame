using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace FYP_IncentiveMechanismSimulatorMVP.ApplicationLogic
{
    public class SimulationSettings
    {
        public double FIXED_MARKET_SHARE { get; }
        public double FIXED_STARTING_ASSET { get; }
        public double MIN_TRAINING_LENGTH { get; }
        public double MAX_TRAINING_LENGTH { get; }
        public double MIN_BID_LENGTH { get; }
        public double MIN_PROFIT_LENGTH { get; }
        public int NUM_OF_FEDERATIONS { get; }
        public int NUM_OF_PLAYERS { get; }
        public double MIN_DATA_QUALITY { get; }
        public double MAX_DATA_QUALITY { get; }
        public double MIN_DATA_QUANTITY { get; }
        public double MAX_DATA_QUANTITY { get; }
        public double DATA_QUALITY_WEIGHT { get; }
        public double DATA_QUANTITY_WEIGHT { get; }
        public int MIN_RESOURCE_QUANTITY { get; }
        public int MAX_RESOURCE_QUANTITY { get; }
        public double INIT_DATA_QUALITY { get; }
        public double INIT_DATA_QUANTITY { get; }
        public int INIT_RESOURCE_QUANTITY { get; }
        public int INIT_AMOUNT_BID { get; }
        public int MAX_TURNS { get; }
        public double FIXED_MARKET_SHARE_PCT { get; }
        public Dictionary<string, List<Tuple<string, string>>> settingsList { get; }
        //public List<Dictionary<string, Tuple<string, string>>> settingsList { get; }

        private XElement _xdoc=null;
        public SimulationSettings() { }
        public SimulationSettings(XElement xdoc)
        {
            _xdoc = xdoc;
            this.settingsList = new Dictionary<string, List<Tuple<string, string>>>();
            FIXED_MARKET_SHARE = ConvertToDouble((GetValue("Environment_Settings", "FIXED_MARKET_SHARE")));
            FIXED_STARTING_ASSET = ConvertToDouble((GetValue("Environment_Settings", "FIXED_STARTING_ASSET")));
            MIN_TRAINING_LENGTH = ConvertToDouble((GetValue("Environment_Settings", "MIN_TRAINING_LENGTH")));
            MAX_TRAINING_LENGTH = ConvertToDouble((GetValue("Environment_Settings", "MAX_TRAINING_LENGTH")));
            MIN_BID_LENGTH = ConvertToDouble((GetValue("Environment_Settings", "MIN_BID_LENGTH")));
            MIN_PROFIT_LENGTH = ConvertToDouble((GetValue("Environment_Settings", "MIN_PROFIT_LENGTH")));
            NUM_OF_FEDERATIONS = ConvertToInt((GetValue("Environment_Settings", "NUM_OF_FEDERATIONS")));
            NUM_OF_PLAYERS = ConvertToInt((GetValue("Environment_Settings", "NUM_OF_CPU_PLAYERS")));
            MAX_TURNS = ConvertToInt((GetValue("Environment_Settings", "MAX_TURNS")));
            MIN_DATA_QUALITY = ConvertToDouble((GetValue("Environment_Settings", "MIN_DATA_QUALITY")));
            MAX_DATA_QUALITY = ConvertToDouble((GetValue("Environment_Settings", "MAX_DATA_QUALITY")));
            MIN_DATA_QUANTITY = ConvertToDouble((GetValue("Environment_Settings", "MIN_DATA_QUANTITY")));
            MAX_DATA_QUANTITY = ConvertToDouble((GetValue("Environment_Settings", "MAX_DATA_QUANTITY")));
            DATA_QUALITY_WEIGHT = ConvertToDouble((GetValue("Environment_Settings", "DATA_QUALITY_WEIGHT")));
            DATA_QUANTITY_WEIGHT = ConvertToDouble((GetValue("Environment_Settings", "DATA_QUANTITY_WEIGHT")));
            MIN_RESOURCE_QUANTITY = ConvertToInt((GetValue("Environment_Settings", "MIN_RESOURCE_QUANTITY")));
            MAX_RESOURCE_QUANTITY = ConvertToInt((GetValue("Environment_Settings", "MAX_RESOURCE_QUANTITY")));
            INIT_DATA_QUALITY = ConvertToDouble((GetValue("Threshold_Settings","INIT_DATA_QUALITY")));
            INIT_DATA_QUANTITY = ConvertToDouble((GetValue("Threshold_Settings", "INIT_DATA_QUANTITY")));
            INIT_RESOURCE_QUANTITY = ConvertToInt((GetValue("Threshold_Settings", "INIT_RESOURCE_QUANTITY")));
            INIT_AMOUNT_BID = ConvertToInt((GetValue("Threshold_Settings", "INIT_AMOUNT_BID")));
            FIXED_MARKET_SHARE_PCT = ConvertToDouble((GetValue("Environment_Settings", "FIXED_MARKET_SHARE_PCT")));
        }

        public string GetValue(string parent,string settingsString)
        {
            try
            {
                string value = this._xdoc.Element(parent).Element(settingsString).Value;
                Console.WriteLine("{0} -> {1}", settingsString, value);
                if (!this.settingsList.ContainsKey(parent))
                {
                    this.settingsList.Add(parent, new List<Tuple<string, string>>());
                }
                this.settingsList[parent].Add(new Tuple<string, string>(settingsString, value));
                return value;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error reading setting {0}", settingsString);
                Console.WriteLine(e.Message);
                return "";
            }
        }
        private double ConvertToDouble(string value)
        {
            try 
            {
                return Convert.ToDouble(value);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return 0.0;
            }
        }
        private int ConvertToInt(string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
    }
}
