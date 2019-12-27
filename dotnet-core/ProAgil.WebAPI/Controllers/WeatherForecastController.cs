using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
           return new Evento[] {
               new Evento() {
                   EventoId = 1,
                   Tema = "Angular e .NET Core",
                   Local = "Belo Horizonte",
                   Lote = "Primeiro Lote",
                   QtdPessoas = 250,
                   DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
               },
               new Evento() {
                   EventoId = 2,
                   Tema = "React e Java",
                   Local = "Rio de Janeiro",
                   Lote = "Segundo Lote Lote",
                   QtdPessoas = 200,
                   DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
               }
           };
        }
        [HttpGet("{id}")]
        public ActionResult<Evento> Get(int id)
        {
           return new Evento[] {
               new Evento() {
                   EventoId = 1,
                   Tema = "Angular e .NET Core",
                   Local = "Belo Horizonte",
                   Lote = "Primeiro Lote",
                   QtdPessoas = 250,
                   DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
               },
               new Evento() {
                   EventoId = 2,
                   Tema = "React e Java",
                   Local = "Rio de Janeiro",
                   Lote = "Segundo Lote Lote",
                   QtdPessoas = 200,
                   DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
               }
           }.FirstOrDefault(x => x.EventoId == id);
        }
    }
}
