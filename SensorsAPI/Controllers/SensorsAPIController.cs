using EnvironmentMonitoring.Sensors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace SensorsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsAPIController : ControllerBase
    {
        private readonly List<TemperatureSensor> temperatureSensors;
        private readonly List<RainfallSensor> rainfallSensors;
        private readonly HumiditySensor humiditySensor;
        private readonly AirpollutionSensor airPollutionSensor;
        private readonly List<Co2EmissionSensor> co2EmissionsSensors;

        private readonly HttpClient httpClient;

        private int sensorIdCounter = 1;

        public SensorsAPIController(IHttpClientFactory httpClientFactory)
        {
            temperatureSensors = new List<TemperatureSensor>();
            rainfallSensors = new List<RainfallSensor>();
            humiditySensor = new HumiditySensor();
            airPollutionSensor = new AirpollutionSensor();
            co2EmissionsSensors = new List<Co2EmissionSensor>();

            InitializeSensors();

            httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7108/");
        }

        private void InitializeSensors()
        {
            // Initialize temperature sensors
            for (int i = 0; i < 3; i++)
            {
                temperatureSensors.Add(new TemperatureSensor());
            }

            // Initialize rainfall sensors
            for (int i = 0; i < 2; i++)
            {
                rainfallSensors.Add(new RainfallSensor());
            }

            // Initialize CO2 emissions sensors
            for (int i = 0; i < 5; i++)
            {
                co2EmissionsSensors.Add(new Co2EmissionSensor());
            }
        }

        [HttpGet("temperature")]
        public ActionResult<List<SensorData>> GetTemperatureData()
        {
            List<SensorData> temperatureData = new List<SensorData>();

            foreach (var sensor in temperatureSensors)
            {
                temperatureData.Add(new SensorData
                {
                    SensorId = sensorIdCounter++,
                    SensorType = "Temperature",
                    Value = sensor.GenerateData(),
                    Timestamp = DateTime.Now
                });
            }

            return temperatureData;
        }

        [HttpGet("rainfall")]
        public ActionResult<List<SensorData>> GetRainfallData()
        {
            List<SensorData> rainfallData = new List<SensorData>();

            foreach (var sensor in rainfallSensors)
            {
                rainfallData.Add(new SensorData
                {
                    SensorId = sensorIdCounter++,
                    SensorType = "Rainfall",
                    Value = sensor.GenerateData(),
                    Timestamp = DateTime.Now
                });
            }

            return rainfallData;
        }

        [HttpGet("humidity")]
        public ActionResult<List<SensorData>> GetHumidityData()
        {
            List<SensorData> humidityData = new List<SensorData>
            {
                new SensorData
                {
                    SensorId = sensorIdCounter++,
                    SensorType = "Humidity",
                    Value = humiditySensor.GenerateData(),
                    Timestamp = DateTime.Now
                }
            };

            return humidityData;
        }

        [HttpGet("airpollution")]
        public ActionResult<List<SensorData>> GetAirPollutionData()
        {
            List<SensorData> airPollutionData = new List<SensorData>
            {
                new SensorData
                {
                    SensorId = sensorIdCounter++,
                    SensorType = "Air Pollution",
                    Value = airPollutionSensor.GenerateData(),
                    Timestamp = DateTime.Now
                }
            };

            return airPollutionData;
        }

        [HttpGet("co2emissions")]
        public ActionResult<List<SensorData>> GetCO2EmissionsData()
        {
            List<SensorData> co2EmissionsData = new List<SensorData>();

            foreach (var sensor in co2EmissionsSensors)
            {
                co2EmissionsData.Add(new SensorData
                {
                    SensorId = sensorIdCounter++,
                    SensorType = "CO2 Emissions",
                    Value = sensor.GenerateData(),
                    Timestamp = DateTime.Now
                });
            }

            return co2EmissionsData;
        }

        [HttpPost("postToMonitoringStation")]
        public async Task<IActionResult> PostDataToMonitoringStation([FromBody] SensorData sensorData)
        {
            try
            {
                // Serialize sensor data
                string jsonString = JsonSerializer.Serialize(sensorData);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                // Send data to Monitoring Station API
                var response = await httpClient.PostAsync("/MonitoringStationAPI/", content);

                response.EnsureSuccessStatusCode();
                return Ok("Data sent to Monitoring Station API successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class SensorData
    {
        public int SensorId { get; set; }
        public string SensorType { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
