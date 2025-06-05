using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace mptc.dgc.sample.webapi.Filter;

public  class ApiVersionDateParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null) return;

        var apiVersionParam = operation.Parameters.FirstOrDefault(p => p.Name == "api-version");
        if (apiVersionParam != null)
        {
            apiVersionParam.Required = true;
            apiVersionParam.Schema.Type = "string";
            apiVersionParam.Schema.Format = "date";
            apiVersionParam.Description = "API version in yyyy-MM-dd format";
            //apiVersionParam.Example = new Microsoft.OpenApi.Any.OpenApiString("2025-05-01");
            apiVersionParam.Schema.Default = new Microsoft.OpenApi.Any.OpenApiString("2025-05-01");
        }
    }
}