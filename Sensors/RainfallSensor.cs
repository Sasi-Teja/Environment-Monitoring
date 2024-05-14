using System;

namespace EnvironmentMonitoring.Sensors;

public class RainfallSensor
{
    public double GenerateData()
    {
        // Generate rainfall data within the specified range (0 to 40 mm)
        Random random = new Random();
        return random.NextDouble() * 40;
    }
}