using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Serialization;

namespace CommandAPI
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
      var builder = new NpgsqlConnectionStringBuilder();
      builder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
      builder.Username = Configuration["UserID"];
      builder.Password = Configuration["Password"];
      services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(builder.ConnectionString));
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
      {
        opt.Audience = Configuration["ResourceId"];
        opt.Authority = $"{Configuration["Instance"]}{Configuration["TenantId"]}";
      });

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddControllers().AddNewtonsoftJson(s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
      services.AddScoped<ICommandAPIRepository, SqlCommandAPIRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CommandContext context)
    {
      context.Database.Migrate();
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
