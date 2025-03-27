namespace MonitoramentoApi.Models
{
    public class CheckLogService
    {
        public int Id { get; set; }
        public int MonitoramentoServiceId { get; set; }
        public DateTime CheckTime { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public MonitoramentoService MonitoramentoService { get; set; }
    }
}