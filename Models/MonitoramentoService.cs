namespace MonitoramentoApi.Models
{
    public class MonitoramentoService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int IntervaloMinutos { get; set; }
        public bool Ativo { get; set; } // Removido IsActive para evitar redundância
    }
}