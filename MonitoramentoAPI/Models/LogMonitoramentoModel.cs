namespace MonitoramentoAPI.Models
{
    public class LogMonitoramentoModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime DataVerificacao { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public int? TempoResposta { get; set; }
        public string Erro { get; set; }

        public ServiceModel ServiceModel { get; set; }
    }
}
