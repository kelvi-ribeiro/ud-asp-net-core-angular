using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProAgil.Domain.Identity;
using ProAgil.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProAgil.WebAPI
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
      services.AddDbContext<ProAgilContext>(x => x
      .UseSqlite(Configuration
      .GetConnectionString("DefaultConnection")));

      IdentityBuilder builder = services.AddIdentityCore<User>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;

      });

      builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
      builder.AddEntityFrameworkStores<ProAgilContext>();
      builder.AddRoleValidator<RoleValidator<Role>>();
      builder.AddRoleManager<RoleManager<Role>>();
      builder.AddSignInManager<SignInManager<User>>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                              .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                      };
                    }
                );

      services.AddScoped<IProAgilRepository, ProAgilRepository>();
      services.AddControllers();
      services.AddCors();
      services.AddAutoMapper();
      services.AddMvc(
          options =>
          {
            options.EnableEndpointRouting = false;
            var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
          })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // app.UseHttpsRedirection();
      app.UseAuthentication();
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseRouting();

      app.UseAuthorization();
      app.UseStaticFiles();
      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
        RequestPath = new PathString("/Resources")
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
