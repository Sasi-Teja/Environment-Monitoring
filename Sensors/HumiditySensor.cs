using System;
namespace EnvironmentMonitoring.Sensors;
public class HumiditySensor
{
    public double GenerateData()
    {
        // Generate humidity data within the specified range (0% to 100%)
        Random random = new Random();
        return random.NextDouble() * 100;
    }
}