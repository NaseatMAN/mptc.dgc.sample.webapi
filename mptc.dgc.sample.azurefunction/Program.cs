using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mptc.dgc.sample.infrastructure.Models;

var builder = FunctionsApplication.CreateBuilder(args);
builder.Services.AddDbContext<SampleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.ConfigureFunctionsWebApplication();
builder.Build().Run();
