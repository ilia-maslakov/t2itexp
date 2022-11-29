using t2itexp.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using t2itexp.Models;

namespace t2itexp.Data
{
    public class PhoneRepository : IRepository<Phone>
    {
        private readonly ILogger<UnitOfWork> _logger;
        private readonly DBphoneContext _context;

        public PhoneRepository(DBphoneContext context)
        {
            _context = context;
            _logger = LoggerFactory.Create(options => { }).CreateLogger<UnitOfWork>();
        }

        public int Count()
        {
            return _context.Phone.Count();
        }

        Phone? IRepository<Phone>.Get(int id)
        {
            try
            {
                return _context.Phone.Find(id);
            } catch (Exception e)
            {
                _logger.LogInformation("{Time} Error: {Message}", DateTime.UtcNow.ToLongTimeString(), e.Message);
                return new Phone();
            }
        }

        IEnumerable<Phone> IRepository<Phone>.Get()
        {
            return _context.Phone.ToList();
        }

        public IEnumerable<Phone> Get(int page = 1, int pageSize = 20)
        {
            try
            {
                return _context.Phone.OrderBy(i => i.Code).Skip(pageSize * page).Take(pageSize).ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation("{Time} Error: {Message}", DateTime.UtcNow.ToLongTimeString(), e.Message);
                return new List<Phone>() { };
            }
        }

        IEnumerable<Phone> IRepository<Phone>.Get(Func<Phone, bool> predicate)
        {
            try
            {
                return _context.Phone.Where(predicate).ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation("{Time} Error: {Message}", DateTime.UtcNow.ToLongTimeString(), e.Message);
                return new List<Phone>() { };
            }
        }

        public ValueTask<Phone?> GetAsync(int id)
        {
            var res = _context.Phone.FindAsync(id);
            return res;
        }

        public Task<List<Phone>> GetAsync(int? code)
        {
            return _context.Phone.Where(x => x.Code == (code ?? default)).ToListAsync();
        }

        public Task<List<Phone>> GetAsync(string value)
        {
            return _context.Phone.Where(x => (x.Value ?? "").Contains(value)).ToListAsync();
        }

        public Task<List<Phone>> GetAsync()
        {
            try
            {
                return _context.Phone.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogInformation("{Time} Error: {Message}", DateTime.UtcNow.ToLongTimeString(), e.Message);
                return Task.Run(() => new List<Phone>());
            }
        }


        Phone IRepository<Phone>.Add(Phone item)
        {
            _logger.LogInformation("{Time} Try Add item ({Item})", DateTime.UtcNow.ToLongTimeString(), item);
            _context.Add(item);
            return item;
        }

        ValueTask<EntityEntry<Phone>> IRepository<Phone>.AddAsync(Phone item)
        {
            return _context.AddAsync(item);
        }

        void IRepository<Phone>.Update(Phone item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        void IRepository<Phone>.Remove(int id)
        {
            Phone? p = _context.Phone.Find(id);
            if (p != null)
            {
                _context.Phone.Remove(p);
            }
        }

        void IRepository<Phone>.Remove(Phone item)
        {
            _context.Phone.Remove(item);
        }

        void IRepository<Phone>.Remove(Func<Phone, bool> predicate)
        {
            foreach (Phone o in _context.Phone.Where(predicate).ToList())
            {
                _context.Phone.Remove(o);
            }
            _context.SaveChanges();
        }

        public async Task<int> AddRange(IEnumerable<Phone> items)
        {
            foreach (var p in items)
            {
                // clear Id then it is filled
                p.Id = 0;
                _context.Phone.Add(p);
            }
            try
            {
                int count = await _context.SaveChangesAsync();
                return count;
            }
            catch (Exception e)
            {
                _logger.LogInformation("{Time} Error: {Message}", DateTime.UtcNow.ToLongTimeString(), e.Message);
                return 0;
            }
        }
    }
}