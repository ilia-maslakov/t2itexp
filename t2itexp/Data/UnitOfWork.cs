using t2itexp.Data.EF;

namespace t2itexp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBphoneContext _db;
        private PhoneRepository _phoneRepository;

        public UnitOfWork(DBphoneContext context, PhoneRepository phoneRepository)
        {
            _db = context;
            _phoneRepository = phoneRepository;
        }

        public IRepository<Phone> Phone
        {
            get
            {
                _phoneRepository ??= new PhoneRepository(_db);
                return _phoneRepository;
            }
        }

        int IUnitOfWork.SaveChanges()
        {
            return _db.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
