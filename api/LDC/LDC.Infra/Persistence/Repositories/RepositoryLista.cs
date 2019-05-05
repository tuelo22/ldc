using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using System;

namespace LDC.Infra.Persistence.Repositories
{
    public class RepositoryLista : RepositoryBase<Lista, Guid>, IRepositoryLista
    {
        protected readonly LDCContext _context;

        public RepositoryLista(LDCContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
