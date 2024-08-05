using MySocialMedia.Logic.Services;

namespace MySocialMedia.API
{
    public class Startup
    {
        public IConfiguration Configuration { get;}
        public Startup(IConfiguration p_configuration)
        {
            Configuration = p_configuration;    
        }
        public void ConfigureServices(IServiceCollection p_services)
        {
            p_services.AddControllers(); 
            p_services.AddEndpointsApiExplorer(); 
            p_services.AddSwaggerGen();
            p_services.AddMvc();
            p_services.AddSingleton<IUserService>(x => new UserService()); 
        }
        public void Configure(IApplicationBuilder p_app , IWebHostEnvironment p_env)
        {
            if (p_env.IsDevelopment())
            {
                p_app.UseDeveloperExceptionPage();
                p_app.UseSwagger(); 
                p_app.UseSwaggerUI(); 
            }


            p_app.UseRouting();

            p_app.UseAuthorization();
            p_app.UseEndpoints(enpoints => 
            { 
                enpoints.MapControllers();
            });
        }
    }

}
