using System;
namespace EnvironmentMonitoring.Sensors;

public class AirpollutionSensor
{
    public double GenerateData()
    {
        // Generate air pollution data within the specified range (1 to 10 AQI)
        Random random = new Random();
        return random.Next(1, 11);
    }
}