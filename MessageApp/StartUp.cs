using MessageApp.Repositories;
using MessageApp.Services;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.OpenApi.Models;

namespace MessageApp;

public class StartUp
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Настройка сервисов
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "MessageApp API", Version = "v1" });
        });
        
        services.AddScoped<MessageService>();
        services.AddScoped<MessageRepository>();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MessageService API v1"));
        }
        
        app.UseWebSockets();
        app.UseMiddleware<WebSocketMiddleware>();
        
        app.UseStaticFiles();
        
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}