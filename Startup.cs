using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimBasedAuthorization.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ClaimBasedAuthorization
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
            //DI Injections
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAudienceService, AudienceService>();


            ConfigureAuthentication(services);
            services.AddMvc(
               options =>
               {
                   var policy = new AuthorizationPolicyBuilder()
                                       .RequireAuthenticatedUser()
                                       .Build();
                   options.Filters.Add(new AuthorizeFilter(policy));
               });
        }



        public void ConfigureAuthentication(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(AuthService.secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                // Validate the JWT issuer (Iss) claim
                ValidateIssuer = false,
                //ValidIssuers = validIssuerList,

                // Validate the JWT audience (Aud) claim
                ValidateAudience = false,
                //ValidAudiences = validAudienceList,

                // Validate token expiration
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
