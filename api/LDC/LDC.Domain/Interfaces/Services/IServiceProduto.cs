using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories.Base;
using System;

namespace LDC.Domain.Interfaces.Repositories
{
    public interface IRepositoryProduto : IRepositoryBase<Produto, Guid>
    {
    }
}
