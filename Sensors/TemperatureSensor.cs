using System;

namespace EnvironmentMonitoring.Sensors
{
    public class TemperatureSensor
    {
        public double GenerateData()
        {
            // Generate temperature data within the specified range (-20°C to 39°C)
            Random random = new Random();
            return random.Next(-20, 40); // The upper bound is exclusive, so 40 is used to include 39
        }
    }
}