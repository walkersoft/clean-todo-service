using MediatR;
using System.Diagnostics;

namespace CleanTodo.Core.Behaviors
{
    public class DebugNotificationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            Debug.WriteLine("Handling request {0}", args: request.GetType().Name);
            var response = await next();
            sw.Stop();
            Debug.WriteLine("Finished request {0} in {1} ms.", request.GetType().Name, sw.ElapsedMilliseconds.ToString());

            return response;
        }
    }
}
