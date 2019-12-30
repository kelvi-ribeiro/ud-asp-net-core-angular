using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
  public interface IProAgilRepository
  {
    // Geral
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveChangeAsync();

    // EVENTOS
    Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);
    Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
    Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes);

    // PALESTRANTE
    Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includePalestrantes);
    Task<Palestrante[]> GetPalestrantesAsync(bool includePalestrantes);
    Task<Palestrante[]> GetPalestrantesAsyncByNome(string nome, bool includePalestrantes);

  }
}
