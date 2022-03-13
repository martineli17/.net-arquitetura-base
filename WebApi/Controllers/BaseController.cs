using AutoMapper;
using Core.Base;
using Core.Objetos;
using Crosscuting.Notificacao;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        protected readonly IMapper Mapper;
        protected readonly INotificador Notificador;
        protected readonly IMediatorCustom Mediator;

        protected BaseController(IServiceProvider serviceProvider)
        {
            Mapper = (IMapper)serviceProvider.GetService(typeof(IMapper));
            Notificador = (INotificador)serviceProvider.GetService(typeof(INotificador));
            Mediator = (IMediatorCustom)serviceProvider.GetService(typeof(IMediatorCustom));
        }

        protected ActionResult CustomResponse(int responsePositivo = 200, int responseNegativo = 400)
        {
            if(Notificador.GetMensagens().Any(x => MensagensValidador.NotFound.Contains(x.Texto)))
                return StatusCode(404, Notificador.GetMensagens().Select(x => x));

            if (Notificador.Contain())
                return StatusCode(responseNegativo, Notificador.GetMensagens().Select(x => x));
                
            return StatusCode(responsePositivo);
        }

        protected ActionResult CustomResponse<TReturn>(TReturn returnObject = default, int responsePositivo = 200, int responseNegativo = 400)
        {
            if (Notificador.Contain())
                return StatusCode(responseNegativo, Notificador.GetMensagens());
            else
                return StatusCode(responsePositivo, returnObject);
        }
    }
}
