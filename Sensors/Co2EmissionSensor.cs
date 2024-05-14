using System;
namespace EnvironmentMonitoring.Sensors;
public class Co2EmissionSensor
{
    public double GenerateData()
    {
        // Generate CO2 emissions data within the specified range (1 to 100 t CO2e)
        Random random = new Random();
        return random.NextDouble() * 100;
    }
}