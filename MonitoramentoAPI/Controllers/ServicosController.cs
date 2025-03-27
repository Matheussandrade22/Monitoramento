using Microsoft.AspNetCore.Mvc;
using MonitoramentoAPI.Data;
using MonitoramentoAPI.Models;

namespace MonitoramentoAPI.Controllers
{
    public class ServicosController : Controller
    {
        private readonly MonitoramentoContext _context;

        public ServicosController(MonitoramentoContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/services")]
        public async Task<IActionResult> GetServices()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

        [HttpPost]
        [Route("api/services")]
        public async Task<IActionResult> PostService(ServiceModel service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = service.Id }, service);
        }
    }
}
