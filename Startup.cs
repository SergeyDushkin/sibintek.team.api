using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sibintek.db.mongodb;
using sibintek.http;
using sibintek.http.middleware;
using sibintek.sibmobile.core;
using sibintek.team.mapping;

namespace sibintek.team.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddMvcOptions(options => {
                    options.Filters.Add(typeof(PaginationResultFilter));
                });
            
            var dbSettings = Configuration.GetSection("TeamDatabaseSettings").Get<MongoDbSettings>();

            services.AddSingleton<IDatabaseSettings>(dbSettings);
            services.AddSingleton<ICommandHandlerFactory, DefaultCommandHandlerFactory>();
            services.AddTransient<IConversationRepository, ConversationRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ConversationService>();
            services.AddTransient<UserService>();
            
            
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);

            // TODO: сделать автоматическую загрузку обработчиков команд
            services.AddTransient<SendDirectMessageCommandHandler>();
            services.AddTransient<SetMessageReadCommandHandler>();
            services.AddTransient<CreateUserCommandHandler>();
            
            //services.Register<ICommandHandler>(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Transient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseHeaderAuthentication();
            app.UseCommandRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

