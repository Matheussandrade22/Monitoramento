using System.Net;
using System.Net.Http;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonitoramentoApi.Data;
using MonitoramentoApi.Models;

namespace MonitoramentoApi.Services
{
    public class MonitoringService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MonitoringService> _logger;

        public MonitoringService(IServiceProvider serviceProvider, ILogger<MonitoringService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<MonitoramentoContext>();
                        var services = await context.MonitoramentoServices.Where(s => s.Ativo).ToListAsync();

                        foreach (var service in services)
                        {
                            var log = new CheckLogService
                            {
                                MonitoramentoServiceId = service.Id,
                                CheckTime = DateTime.UtcNow
                            };

                            try
                            {
                                using (var client = new HttpClient())
                                {
                                    var response = await client.GetAsync(service.Url);
                                    log.IsSuccess = response.IsSuccessStatusCode;
                                    log.Message = response.ReasonPhrase;
                                }
                            }
                            catch (Exception ex)
                            {
                                log.IsSuccess = false;
                                log.Message = ex.Message;
                                SendFailureEmail(service, ex.Message);
                            }

                            context.ServiceCheckLogs.Add(log);
                            await context.SaveChangesAsync();

                            // Aguarda o intervalo específico do serviço antes de verificar o próximo
                            await Task.Delay(TimeSpan.FromMinutes(service.IntervaloMinutos), stoppingToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro no MonitoringService: {ex.Message}");
                }
            }
        }

        private void SendFailureEmail(MonitoramentoService service, string errorMessage)
        {
            var smtpClient = new SmtpClient("smtp.seudominio.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("seuemail@seudominio.com", "sua_senha"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("monitoramento@seudominio.com"),
                Subject = $"Falha no serviço: {service.Name}",
                Body = $"O serviço {service.Name} falhou com a seguinte mensagem: {errorMessage}",
                IsBodyHtml = true,
            };

            mailMessage.To.Add("admin@seudominio.com");

            smtpClient.Send(mailMessage);
        }
    }
}
