using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PalestrantesController : ControllerBase
  {
    private IProAgilRepository _repo;
    public PalestrantesController(IProAgilRepository repo)
    {
      this._repo = repo;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var results = await _repo.GetPalestrantesAsync(true);
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
    [HttpGet("getByNome/{nome}")]
    public async Task<IActionResult> Get(string nome)
    {
      try
      {
        var results = await _repo.GetPalestrantesAsyncByNome(nome, true);
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
    [HttpGet("{PalestranteId}")]
    public async Task<IActionResult> Get(int PalestranteId)
    {
      try
      {
        var results = await _repo.GetPalestranteAsyncById(PalestranteId, true);
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
    public async Task<IActionResult> Post(Palestrante model)
    {
      try
      {
        _repo.Add(model);
        if (await _repo.SaveChangeAsync())
        {
          return Created($"/api/palestrantes/{model.Id}", model);
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
    [HttpPut]
    public async Task<IActionResult> Put(int PalestranteId, Palestrante model)
    {
      try
      {
          var evento = await _repo.GetPalestranteAsyncById(PalestranteId, false);
          if(evento == null) return NotFound();
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
    [HttpDelete]
    public async Task<IActionResult> Delete(int PalestranteId)
    {
      try
      {
          var evento = await _repo.GetPalestranteAsyncById(PalestranteId, false);
          if(evento == null) return NotFound();
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
