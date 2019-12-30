using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
      IQueryable<Evento> query = _context.Eventos
        .Include(c => c.Lotes)
        .Include(c => c.RedeSociais);

      if (includePalestrantes)
      {
        query = query.Include(pe => pe.PalestrantesEventos)
        .ThenInclude(p => p.Palestrante);
      }
      query = query.OrderByDescending(c => c.DataEvento);
      return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
    {
      IQueryable<Evento> query = _context.Eventos
        .Include(c => c.Lotes)
        .Include(c => c.RedeSociais);

      if (includePalestrantes)
      {
        query = query.Include(pe => pe.PalestrantesEventos)
        .ThenInclude(p => p.Palestrante);
      }
      query = query.OrderByDescending(c => c.DataEvento)
        .Where(c => c.Tema.Contains(tema));
      return await query.ToArrayAsync();
    }

    public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes)
    {
      IQueryable<Evento> query = _context.Eventos
        .Include(c => c.Lotes)
        .Include(c => c.RedeSociais);

      if (includePalestrantes)
      {
        query = query.Include(pe => pe.PalestrantesEventos)
        .ThenInclude(p => p.Palestrante);
      }
      query = query.OrderByDescending(c => c.DataEvento)
        .Where(c => c.Id == EventoId);
      return await query.FirstOrDefaultAsync();
    }

    // PALESTRANTE
    public Task<Palestrante[]> GetPalestrantesAsyncByName(bool includePalestrantes)
    {
      throw new System.NotImplementedException();
    } 
    public Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includePalestrantes)
    {
      throw new System.NotImplementedException();
    }      
  }
}
