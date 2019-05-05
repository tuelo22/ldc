using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using System;

namespace LDC.Infra.Persistence.Repositories
{
    public class RepositoryUnidade : RepositoryBase<Unidade, Guid>, IRepositoryUnidade
    {
        protected readonly LDCContext _context;

        public RepositoryUnidade(LDCContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
