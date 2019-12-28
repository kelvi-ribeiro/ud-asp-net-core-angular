﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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
    public async Task<IActionResult> Get()
    {
      try
      {
        var results = await _context.Eventos.ToListAsync();
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
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var result = await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id);
        return Ok(result);
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
  }
}