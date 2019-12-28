using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class EventosController : ControllerBase
  {
    public readonly DataContext _context;
    public EventosController(DataContext context)
    {
      _context = context;
    }
    
    [HttpGet]
    public IEnumerable<Evento> Get()
    {
      /* return _context.Eventos.ToList();Funciona de qualquer jeito */
      return _context.Eventos;
    }
    [HttpGet("{id}")]
    public ActionResult<Evento> Get(int id)
    {
      return _context.Eventos.FirstOrDefault(x => x.EventoId == id);
    }
  }
}
