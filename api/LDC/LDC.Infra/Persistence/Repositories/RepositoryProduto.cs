using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using System;

namespace LDC.Infra.Persistence.Repositories
{
    public class RepositoryProduto : RepositoryBase<Produto, Guid>, IRepositoryProduto
    {
        protected readonly LDCContext _context;

        public RepositoryProduto(LDCContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
