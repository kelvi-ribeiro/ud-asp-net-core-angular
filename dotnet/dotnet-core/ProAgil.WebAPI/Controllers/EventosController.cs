using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.DTOs;

namespace ProAgil.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventosController : ControllerBase
  {
    private IProAgilRepository _repo;
    public IMapper _mapper { get; }
    public EventosController(IProAgilRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var eventos = await _repo.GetAllEventosAsync(true);
        var results = _mapper.Map<EventoDto[]>(eventos);
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
    [HttpGet("getByTema/{tema}")]
    public async Task<IActionResult> Get(string tema)
    {
      try
      {
        var eventos = await _repo.GetAllEventosAsyncByTema(tema, true);
        var results = _mapper.Map<EventoDto>(eventos);
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
    [HttpGet("{EventoId}")]
    public async Task<IActionResult> Get(int EventoId)
    {
      try
      {
        var evento = await _repo.GetEventoAsyncById(EventoId, true);
        var results = _mapper.Map<EventoDto>(evento);
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
    [HttpPost]
    public async Task<IActionResult> Post(EventoDto eventoDto)
    {
      try
      {
        var evento = _mapper.Map<Evento>(eventoDto);
        _repo.Add(evento);
        if (await _repo.SaveChangeAsync())
        {
          return Created($"/api/evento/{evento.Id}", evento);
        }
      }
      catch (System.Exception)
      {

        return this
        .StatusCode(
          StatusCodes.Status500InternalServerError,
          "Banco de Dados Falhou"
          );
      }
      return BadRequest();
    }
    [HttpPut("{EventoId}")]
    public async Task<IActionResult> Put(int EventoId, Evento model)
    {
      try
      {
        var evento = await _repo.GetEventoAsyncById(EventoId, false);
        if (evento == null) return NotFound();
        _repo.Update(model);
        if (await _repo.SaveChangeAsync())
        {
          return NoContent();
        }
      }
      catch (System.Exception)
      {

        return this
        .StatusCode(
          StatusCodes.Status500InternalServerError,
          "Banco de Dados Falhou"
          );
      }
      return BadRequest();
    }
    [HttpDelete("{EventoId}")]
    public async Task<IActionResult> Delete(int EventoId)
    {
      try
      {
        var evento = await _repo.GetEventoAsyncById(EventoId, false);
        if (evento == null) return NotFound();
        _repo.Delete(evento);
        if (await _repo.SaveChangeAsync())
        {
          return NoContent();
        }
      }
      catch (System.Exception)
      {

        return this
        .StatusCode(
          StatusCodes.Status500InternalServerError,
          "Banco de Dados Falhou"
          );
      }
      return BadRequest();
    }
  }
}
