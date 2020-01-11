using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
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
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(int EventoId)
    {
      try
      {
        var file = Request.Form.Files[0];
        var folderName = Path.Combine("Resources", "images");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (file.Length > 0)
        {
          var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
          var fullPath = Path.Combine(pathToSave, filename.Replace("\""," ").Trim());
          using(var stream = new FileStream(fullPath, FileMode.Create))
          {
            file.CopyTo(stream);
          }
        }
        return Ok();
      }
      catch (System.Exception)
      {

        return this
        .StatusCode(
          StatusCodes.Status500InternalServerError,
          "Banco de Dados Falhou"
          );
      }
      return BadRequest("Erro ao tentar realizar upload");
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
          return Created($"/api/evento/{evento.Id}", _mapper.Map<EventoDto>(evento));
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
    public async Task<IActionResult> Put(int EventoId, EventoDto model)
    {
      try
      {
        model.Id = EventoId;
        var evento = await _repo.GetEventoAsyncById(EventoId, false);

        if (evento == null) return NotFound();

        _mapper.Map(model, evento);
        _repo.Update(evento);
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
