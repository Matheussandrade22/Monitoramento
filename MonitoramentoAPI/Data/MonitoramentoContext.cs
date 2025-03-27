using Microsoft.EntityFrameworkCore;
using MonitoramentoAPI.Models;

namespace MonitoramentoAPI.Data
{
    public class MonitoramentoContext : DbContext
    {
        public MonitoramentoContext(DbContextOptions<MonitoramentoContext> options)
            : base(options)
        {
        }
        public DbSet<ServiceModel> Services { get; set; }
    }
    
}
