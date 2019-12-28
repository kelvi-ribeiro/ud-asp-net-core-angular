using Microsoft.AspNetCore.Http;
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
    public IActionResult Get()
    {
      try
      {
        var results = _context.Eventos.ToList();
        return Ok(results);
      }
      catch (System.Exception)
      {

        return this
        .StatusCode(
          StatusCodes.Status500InternalServerError, 
          "Banco de Dados Falhou"
          );
      }

    }
    [HttpGet("{id}")]
    public ActionResult<Evento> Get(int id)
    {
      return _context.Eventos.FirstOrDefault(x => x.EventoId == id);
    }
  }
}
