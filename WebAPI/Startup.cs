using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebAPI.Model;
using WebAPI.Repositories;
using Service.ICheapPaymentGateway;
using Service.IExpensivePaymentGateway;
using Service.IPremiumPaymentService;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
        .AddJwtBearer("JwtBearer", jwtBearerOptions =>
        {
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret Key You Devise")),
                ValidateIssuer = false,
                //ValidIssuer = "The name of the issuer",
                ValidateAudience = false,
                //ValidAudience = "The name of the audience",
                ValidateLifetime = true, //validate the expiration and not before values in the token
                ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
            };
        });
            services.AddControllers();
            services.AddDbContext<EntityDBContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:AccountDB"]));
            services.AddScoped<IRepository<PaymentStatustbl>, PaymentRepository>();
            services.AddSingleton<ICheapPayService, CheapPayService>();
            services.AddSingleton<IExpPayService, ExpPayService>();
            services.AddSingleton<IPrePayService, PrePayService>();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
