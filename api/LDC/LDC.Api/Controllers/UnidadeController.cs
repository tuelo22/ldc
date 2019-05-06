using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LDC.Api.Controllers.Base;
using LDC.Domain.Arguments.Unidade;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Interfaces.Repositories;
using LDC.Domain.Interfaces.Services;
using LDC.Infra.Transactions;


namespace LDC.Api.Controllers
{
    [RoutePrefix("api/Unidade")]
    public class UnidadeController : ControllerBase
    {
        private readonly IServiceUnidade _serviceUnidade;

        public UnidadeController(IUnitOfWork unitOfWork, IServiceUnidade serviceUnidade)
            : base(unitOfWork)
        {
            _serviceUnidade = serviceUnidade;
        }

        [Route("Alterar")]
        [HttpPut]
        public async Task<HttpResponseMessage> Alterar(AlterarUnidadeRequest request)
        {
            try
            {
                var response = _serviceUnidade.Alterar(request);

                return await ResponseAsync(response, _serviceUnidade);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [Route("Adicionar")]
        [HttpPost]
        public async Task<HttpResponseMessage> Adicionar(AdicionarUnidadeRequest request)
        {
            try
            {
                var response = _serviceUnidade.Adicionar(request);

                return await ResponseAsync(response, _serviceUnidade);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [Route("Listar")]
        [HttpGet]
        public async Task<HttpResponseMessage> Listar(UsuarioRequest request)
        {
            try
            {
                var response = _serviceUnidade.Listar(request);

                return await ResponseAsync(response, _serviceUnidade);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [Route("Excluir")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Excluir(Guid id, UsuarioRequest request)
        {
            try
            {
                var response = _serviceUnidade.Desativar(id, request);

                return await ResponseAsync(response, _serviceUnidade);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}