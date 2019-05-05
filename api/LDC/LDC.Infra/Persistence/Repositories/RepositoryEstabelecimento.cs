using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories;
using LDC.Infra.Persistence.Repositories.Base;
using System;

namespace LDC.Infra.Persistence.Repositories
{
    public class RepositoryEstabelecimento : RepositoryBase<Estabelecimento, Guid>, IRepositoryEstabelecimento
    {
        protected readonly LDCContext _context;

        public RepositoryEstabelecimento(LDCContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
