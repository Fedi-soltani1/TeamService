using TeamService.Data;
using TeamService.Repositories;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();  
        services.AddSingleton<TeamContext>(sp => new TeamContext("mongodb://localhost:27017"));
        services.AddTransient<TeamRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
