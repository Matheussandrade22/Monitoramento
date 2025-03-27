using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MonitoramentoApi.Models;

namespace MonitoramentoApi.Data
{
    public class MonitoramentoContext : DbContext
    {
        public MonitoramentoContext(DbContextOptions<MonitoramentoContext> options) : base(options) { }

        public DbSet<MonitoramentoService> MonitoramentoServices { get; set; }
        public DbSet<CheckLogService> ServiceCheckLogs { get; set; }
    }
}
