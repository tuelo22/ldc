using LDC.Infra.Persistence;

namespace LDC.Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LDCContext _context;

        public UnitOfWork(LDCContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
