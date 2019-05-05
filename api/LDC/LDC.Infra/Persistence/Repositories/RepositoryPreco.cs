using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using System;

namespace LDC.Infra.Persistence.Repositories
{
    public class RepositoryPreco : RepositoryBase<Preco, Guid>, IRepositoryPreco
    {
        protected readonly LDCContext _context;

        public RepositoryPreco(LDCContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
