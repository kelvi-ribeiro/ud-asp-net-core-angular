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
    Task<Evento[]> GetEventoAsyncById(int EventoId, bool includePalestrantes);

    // PALESTRANTE
    Task<Evento[]> GetPalestrantesAsyncByName(bool includePalestrantes);
    Task<Evento[]> GetPalestranteAsyncById(int PalestranteId, bool includePalestrantes);


  }
}
