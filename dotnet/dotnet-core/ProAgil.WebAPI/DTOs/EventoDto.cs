using System.Collections.Generic;

namespace ProAgil.WebAPI.DTOs
{
  public class EventoDto
  {
    public int Id { get; set; }
    public string Local { get; set; }
    public string imagemURL { get; set; }
    public string DataEvento { get; set; }
    public string Tema { get; set; }
    public int QtdPessoas { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Lote { get; set; }
    public List<LoteDto> Lotes { get; set; }
    public List<RedeSocialDto> RedeSociais { get; set; }
    public List<PalestranteDto> Palestrantes { get; set; }
  }
}
