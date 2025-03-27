using MonitoramentoAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace MonitoramentoAPI.Controllers
{
    public class ServicesController : ApiController
    {
        private readonly MonitoramentoContext _context = new MonitoramentoContext();

        [HttpGet]
        [Route("api/services")]
        public IHttpActionResult GetServices()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

        [HttpPost]
        [Route("api/services")]
        public IHttpActionResult PostService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = service.Id }, service);
        }

        // Outros métodos para ativar/desativar e atualizar serviços
    }
}

