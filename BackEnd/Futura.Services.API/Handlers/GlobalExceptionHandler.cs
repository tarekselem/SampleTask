using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Web.Http.Tracing;

namespace Futura.Services.API.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        #region || == Public Methods == ||
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(() =>
            {
                var logger = new AppLogger();
                logger.Error(context.Request, "Controller: " + context.RequestContext.Url, TraceLevel.Error, context.Exception);

                var errorMessage = GetErrorMessage(context);
                var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new
                    {
                        Message = errorMessage
                    });
                response.Headers.Add("X-Error", errorMessage);
                context.Result = new ResponseMessageResult(response);
            });
        }
        #endregion

        #region Private Methods
        private static string GetErrorMessage(ExceptionHandlerContext context)
        {
            var exceptionMessage = string.Empty;
            if (context.Exception.InnerException == null)
            {
                exceptionMessage = context.Exception.Message;
            }
            else
            {
                exceptionMessage = context.Exception.InnerException.Message;
            }
            return exceptionMessage;
        }
        #endregion
    }
}