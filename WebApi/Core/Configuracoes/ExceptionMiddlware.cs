using Microsoft.AspNetCore.Http;

namespace WebApi.Core.Configuracoes
{
    public class ExceptionMiddlware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
