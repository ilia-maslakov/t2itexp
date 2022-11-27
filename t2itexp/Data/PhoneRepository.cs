using t2itexp.Data.EF;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        Phone? IRepository<Phone>.Get(int id)
        {
            try
            {
                return _context.Phone.Find(id);
            } catch (Exception e)
            {
                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} Error: {e.Message}");
                return null;
            }
        }

        IEnumerable<Phone> IRepository<Phone>.Get()
        {
            return _context.Phone.ToList();
        }

        public IEnumerable<Phone>? Get(int page = 1, int pageSize = 20)
        {
            try
            {
                var res = _context.Phone.OrderBy(i => i.Code).Skip(pageSize * page).Take(pageSize).ToList();
                return res;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} Error: {e.Message}");
                return null;
            }
        }

        IEnumerable<Phone> IRepository<Phone>.Get(Func<Phone, bool> predicate)
        {
            return _context.Phone.Where(predicate).ToList();
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
                _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} Error: {e.Message}");

                return Task.Run(() => new List<Phone>());
            }
        }


        Phone IRepository<Phone>.Add(Phone item)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} Try Add phone ({item})");
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
        }
    }
}