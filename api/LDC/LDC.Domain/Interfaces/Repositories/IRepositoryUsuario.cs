using LDC.Domain.Entities;
using LDC.Domain.Interfaces.Repositories.Base;
using System;

namespace LDC.Domain.Interfaces.Repositories
{
    public interface IRepositoryUsuario : IRepositoryBase<Usuario, Guid>
    {
    }
}
