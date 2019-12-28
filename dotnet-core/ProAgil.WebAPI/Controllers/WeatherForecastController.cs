using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    public readonly DataContext _context;
    public WeatherForecastController(DataContext context)
    {
      _context = context;
    }
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<WeatherForecastController> _logger;

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
