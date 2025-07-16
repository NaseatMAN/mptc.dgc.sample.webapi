using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using mptc.dgc.sample.application.Middleware;
using mptc.dgc.sample.webapi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomApiVersioning();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = InvalidModelStateResponse.ProduceErrorResponse;
});
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});
builder.Services.AddApplicationService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerConfiguration(provider);

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
