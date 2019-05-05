using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using System;

namespace LDC.Infra.Persistence.Repositories
{
    public class RepositoryUsuario : RepositoryBase<Usuario, Guid>, IRepositoryUsuario
    {
        protected readonly LDCContext _context;

        public RepositoryUsuario(LDCContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
