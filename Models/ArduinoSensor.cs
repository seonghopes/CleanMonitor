using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMonitor.Models
{
    public enum SensorLevel
    {
        None,
        Normal,
        Warning,
        Critical,

    }

    public class ArduinoSensor
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public SensorLevel Level { get; private set; }

        private int warningThreshold;
        private int criticalThreshold;

        public ArduinoSensor(string name, int warningThreshold, int criticalThreshold)
        {
            Name = name;
            this.warningThreshold = warningThreshold;
            this.criticalThreshold = criticalThreshold;
           
        }

        public void UpdateValue(int newValue)
        {
            Value = newValue;

            switch (Name[0])
            {
                case 'd':
                    if (Value >= criticalThreshold)
                        Level = SensorLevel.Critical;
                    else if (Value >= warningThreshold)
                        Level = SensorLevel.Warning;
                    else
                        Level = SensorLevel.Normal;
                    break;

                case 'l':
                    if (Value >= criticalThreshold)
                        Level = SensorLevel.Normal;
                    else Level = SensorLevel.Critical;
                    break;

                default:
                    Level = SensorLevel.Critical;
                    break;
            }
        }

     
    }

}
