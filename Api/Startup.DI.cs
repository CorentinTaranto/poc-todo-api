using System;
using Core.Repository;
using Core.Services;
using EntityFramework.Repository;
using Infrastructure.Services;

namespace Api;

public partial class Startup
{
    private void ConfigureDependencyInjection(IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        // -- Models --.
        services.AddScoped<IBoardService, BoardService>();

        services.AddScoped<IBoardRepository, BoardRepository>();

        services.AddScoped<ITodoRepository, TodoRepository>();

        services.AddScoped<ITodoService, TodoService>();
    }
}

