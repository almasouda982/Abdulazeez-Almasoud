using System.Runtime.CompilerServices;
using University.Data;

namespace University.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            WaitlistEntryRepository = new WaitlistEntryRepository(_db);
        }
        public IWaitlistEntryRepository WaitlistEntryRepository { get; }
    }
}
