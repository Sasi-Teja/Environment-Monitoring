using Microsoft.AspNetCore.Mvc;
using MonitoringStationAPI.Models;
using System;
using System.Threading.Tasks;

namespace MonitoringStationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitoringStationController : ControllerBase
    {
        private readonly SensorDataContext _context;

        public MonitoringStationController(SensorDataContext context)
        {
            _context = context;
        }

        [HttpPost()]
        public async Task<IActionResult> CollectData([FromBody] SensorData sensorData)
        {
            try
            {
                // Save sensor data to the database
                _context.SensorData.Add(sensorData);
                await _context.SaveChangesAsync();

                // Check for abnormal data and generate warnings

                if (sensorData.SensorType == "Temperature" && (sensorData.Value < -9 || sensorData.Value > 30))
                {
                    Console.WriteLine($"Warning: Temperature is outside normal range! Sensor ID: {sensorData.SensorId}, Value: {sensorData.Value}");
                }

                if (sensorData.SensorType == "Rainfall" && (sensorData.Value < 0 || sensorData.Value > 32))
                {
                    Console.WriteLine($"Warning: Rainfall is outside normal range! Sensor ID: {sensorData.SensorId}, Value: {sensorData.Value}");
                }

                if (sensorData.SensorType == "AirPollution" && (sensorData.Value < 1 || sensorData.Value > 9))
                {
                    Console.WriteLine($"Warning: Air Pollution is outside normal range! Sensor ID: {sensorData.SensorId}, Value: {sensorData.Value}");
                }

                return Ok("Sensor data collected and stored successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("sensor-data")]
        public IActionResult GetSensorData()
        {
            try
            {
                var sensorData = _context.SensorData.ToList(); // Retrieve all sensor data
                return Ok(sensorData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
