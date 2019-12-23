using LDC.Api.Controllers.Base;
using LDC.Domain.Arguments.Lista;
using LDC.Domain.Interfaces.Services;
using LDC.Infra.Transactions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LDC.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/lista")]
    public class ListaController : ControllerBase
    {
        private readonly IServiceLista _serviceLista;

        public ListaController(IUnitOfWork unitOfWork, IServiceLista serviceLista)
            : base(unitOfWork)
        {
            _serviceLista = serviceLista;
        }

        [Route("Alterar")]
        [HttpPut]
        public async Task<HttpResponseMessage> Alterar(AlterarListaRequest request)
        {
            try
            {
                var response = _serviceLista.Alterar(request);

                return await ResponseAsync(response, _serviceLista);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [Route("Adicionar")]
        [HttpPost]
        public async Task<HttpResponseMessage> Adicionar(AdicionarListaRequest request)
        {
            try
            {
                var response = _serviceLista.Adicionar(request);

                return await ResponseAsync(response, _serviceLista);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
  
        [Route("Listar")]
        [HttpGet]
        public async Task<HttpResponseMessage> Listar(Guid IdUsuario)
        {
            try
            {
                var response = _serviceLista.Listar(IdUsuario);

                return await ResponseAsync(response, _serviceLista);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [Route("Excluir")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Excluir(Guid id, Guid IdUsuario)
        {
            try
            {
                var response = _serviceLista.Desativar(id, IdUsuario);

                return await ResponseAsync(response, _serviceLista);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);             
            }
        }
    }
}