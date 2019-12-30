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
        .Where(p => p.Tema.ToLower().Contains(tema.ToLower()));
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
    public async Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
       .Include(c => c.RedeSociais);

      if (includeEventos)
      {
        query = query.Include(pe => pe.PalestrantesEventos)
        .ThenInclude(p => p.Evento);
      }
      query = query.Where(p => p.Id == PalestranteId);
      return await query.FirstOrDefaultAsync();
    }
    public async Task<Palestrante[]> GetPalestrantesAsync(bool includeEventos)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
       .Include(c => c.RedeSociais);

      if (includeEventos)
      {
        query = query.Include(pe => pe.PalestrantesEventos)
        .ThenInclude(p => p.Evento);
      }
      query = query.OrderBy(c => c.Nome);
      return await query.ToArrayAsync();
    }
    public async Task<Palestrante[]> GetPalestrantesAsyncByNome(string nome, bool includeEventos)
    {
      IQueryable<Palestrante> query = _context.Palestrantes
       .Include(c => c.RedeSociais);

      if (includeEventos)
      {
        query = query.Include(pe => pe.PalestrantesEventos)
        .ThenInclude(p => p.Evento);
      }
      query = query.OrderBy(c => c.Nome)
      .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

      return await query.ToArrayAsync();
    }
  }
}
