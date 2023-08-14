using DiceServer.Database;
using DiceServer.Login.Implementations;
using DiceServer.Login.Interfaces;
using DiceServer.WebSockets;
using DiceServer.WebSockets.Extensions;
using DiceServer.WebSockets.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiceServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IMemoryCache, MemoryCache>();

            services.AddSingleton<ITokenManager, TokenManager>();

            services.AddSingleton<IUserRepository, UserRepository>();
            
            services.AddSingleton<ILogin, LoginService>();
            services.AddSingleton<IRegistrar, Registrar>();

            services.AddSingleton<IWebSocketAddressFactory, WebSocketAddressFactory>();
            services.AddWebSocketManager();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            app.UseRouting();
            app.UseWebSockets();

            app.MapWebSocketManager("/ws", serviceProvider.GetService<WebSocketHandler>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World! This is Dice 1000 server!");
                });

                endpoints.MapControllers();
            });
        }
    }
}
