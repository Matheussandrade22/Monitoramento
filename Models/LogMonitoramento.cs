using System;

namespace MonitoramentoAPI.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public bool Ativo { get; set; }
        public int IntervaloMonitoramento { get; set; }
        public string StatusUltimaVerificacao { get; set; } = "Aguardando";
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }

    public class LogMonitoramento
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime DataVerificacao { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public int? TempoResposta { get; set; }
        public string Erro { get; set; }

        public Service Service { get; set; }
    }
}
