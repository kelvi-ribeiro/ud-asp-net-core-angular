using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
  public class ProAgilRepository : IProAgilRepository
  {
    private ProAgilContext _context;

    public ProAgilRepository(ProAgilContext context)
      {
      _context = context;
    }
    // GERAIS
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }
    
    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<bool> SaveChangeAsync()
    {
      return (await _context.SaveChangesAsync() > 0);
    }

    // EVENTO
    public Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
    {
      throw new System.NotImplementedException();
    }

    public Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
    {
      throw new System.NotImplementedException();
    }

    public Task<Evento[]> GetEventoAsyncById(int EventoId, bool includePalestrantes)
    {
      throw new System.NotImplementedException();
    }

    // PALESTRANTE
    public Task<Evento[]> GetPalestranteAsyncById(int PalestranteId, bool includePalestrantes)
    {
      throw new System.NotImplementedException();
    }

    public Task<Evento[]> GetPalestrantesAsyncByName(bool includePalestrantes)
    {
      throw new System.NotImplementedException();
    }

  }
}
