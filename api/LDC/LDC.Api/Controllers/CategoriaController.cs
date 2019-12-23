using LDC.Api.Controllers.Base;
using LDC.Domain.Arguments.Categoria;
using LDC.Domain.Arguments.Usuario;
using LDC.Domain.Interfaces.Services;
using LDC.Infra.Transactions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace LDC.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly IServiceCategoria _serviceCategoria;

        public CategoriaController(IUnitOfWork unitOfWork, IServiceCategoria serviceCategoria)
            : base(unitOfWork)
        {
            _serviceCategoria = serviceCategoria;
        }

        [Route("Alterar")]
        [HttpPut]
        public async Task<HttpResponseMessage> Alterar(AlterarCategoriaRequest request)
        {
            try
            {
                var response = _serviceCategoria.Alterar(request);

                return await ResponseAsync(response, _serviceCategoria);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [Route("Adicionar")]
        [HttpPost]
        public async Task<HttpResponseMessage> Adicionar(AdicionarCategoriaRequest request)
        {
            try
            {
                var response = _serviceCategoria.Adicionar(request);

                return await ResponseAsync(response, _serviceCategoria);
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
                var response = _serviceCategoria.Listar(request);

                return await ResponseAsync(response, _serviceCategoria);
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
                var response = _serviceCategoria.Desativar(id, request);

                return await ResponseAsync(response, _serviceCategoria);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

    }
}