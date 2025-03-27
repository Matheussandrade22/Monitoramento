using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoApi.Data;
using MonitoramentoApi.Models;

namespace MonitoramentoApi.Controllers
{
    [ApiController]
    [Route("api/ServiceController")]
    public class ServiceController : ControllerBase
    {
        private readonly MonitoramentoContext _context;

        public ServiceController(MonitoramentoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await _context.MonitoramentoServices.ToListAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _context.MonitoramentoServices.FindAsync(id);
            if (service == null) return NotFound();
            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> PostService(MonitoramentoService service)
        {
            _context.MonitoramentoServices.Add(service);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, MonitoramentoService service)
        {
            if (id != service.Id) return BadRequest();

            _context.Entry(service).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ToggleServiceStatus(int id)
        {
            var service = await _context.MonitoramentoServices.FindAsync(id);
            if (service == null) return NotFound();

            service.Ativo = !service.Ativo;
            await _context.SaveChangesAsync();
            return Ok(new { service.Id, service.Ativo });
        }

        [HttpGet("{id}/logs")]
        public async Task<IActionResult> GetServiceLogs(int id)
        {
            var logs = await _context.ServiceCheckLogs
                .Where(l => l.MonitoramentoServiceId == id)
                .OrderByDescending(l => l.CheckTime)
                .ToListAsync();

            return Ok(logs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.MonitoramentoServices.FindAsync(id);
            if (service == null) return NotFound();

            _context.MonitoramentoServices.Remove(service);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
