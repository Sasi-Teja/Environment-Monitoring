using Microsoft.EntityFrameworkCore;

namespace MonitoringStationAPI.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public string SensorType { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class SensorDataContext : DbContext
    {
        public SensorDataContext(DbContextOptions<SensorDataContext> options) : base(options)
        {

        }

        public DbSet<SensorData> SensorData { get; set; }
    }
    
}
