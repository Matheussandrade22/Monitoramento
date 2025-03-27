using System.Collections.Generic;
using System.Data.Entity;

namespace MonitoramentoAPI.Models
{
    public class MonitoramentoContext : DbContext
    {
        public MonitoramentoContext(DbContextOptions<MonitoramentoContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<LogMonitoramento> LogsMonitoramento { get; set; }
    }
}
