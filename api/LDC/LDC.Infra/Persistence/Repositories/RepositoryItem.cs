using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using System;

namespace LDC.Infra.Persistence.Repositories
{
    public class RepositoryItem : RepositoryBase<Item, Guid>, IRepositoryItem
    {
        protected readonly LDCContext _context;

        public RepositoryItem(LDCContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
