using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using System;

namespace LDC.Infra.Persistence.Repositories
{
    public class RepositoryCategoria : RepositoryBase<Categoria, Guid>, IRepositoryCategoria
    {
        protected readonly LDCContext _context;

        public RepositoryCategoria(LDCContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
