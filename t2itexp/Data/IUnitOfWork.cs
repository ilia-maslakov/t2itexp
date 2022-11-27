using t2itexp.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace t2itexp.Data
{
    public interface IUnitOfWork
    {
        IRepository<Phone> Phone { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
