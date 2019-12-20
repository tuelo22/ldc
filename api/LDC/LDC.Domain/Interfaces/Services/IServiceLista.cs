﻿using LDC.Domain.Arguments.Base;
using LDC.Domain.Arguments.Lista;
using LDC.Domain.Interfaces.Services.Base;
using System;
using System.Collections.Generic;

namespace LDC.Domain.Interfaces.Services
{
    public interface IServiceLista : IServiceBase
    {
        AdicionarListaResponse Adicionar(AdicionarListaRequest request);

        ResponseBase Alterar(AlterarListaRequest request);

        IEnumerable<ListaResponse> Listar(Guid IdUsuario);

        ResponseBase Desativar(Guid Id, Guid IdUsuario);
    }
}
