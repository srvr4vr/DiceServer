using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using DiceServer.WebSockets.Handlers;
using DiceServer.WebSockets.Invokers;

namespace DiceServer.WebSockets.Extensions
{
    public static class WebSocketMiddlewareExtensions
    {
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder applicationBuilder, PathString path, WebSocketHandler handler) => 
            applicationBuilder.Map(path, (app) => app.UseMiddleware<WebSocketManagerMiddleware>(handler));

        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<WebSocketConnectionManager>();
            services.AddSingleton<IBroadcaster, Broadcaster>();
            services.AddSingleton<IInvokerResolver, InvokerResolver>();
            services.AddSingleton<WebSocketHandler>();

            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes.Where(exportType =>
                typeof(IInvoker).IsAssignableFrom(exportType) && !exportType.IsAbstract))
            {
                services.AddSingleton(typeof(IInvoker), type);
            }

            return services;
        }

        public static string GetParam(this HttpContext context, string paramName)
            => context.Request.Query[paramName];

        public static string GetUserId(this HttpContext context)
            => context.GetParam("userId");

        public static bool TryGetTokenGuid(this HttpContext context, out Guid guid) 
            => Guid.TryParse(context.GetParam("accessToken"), out guid);
    }
}
