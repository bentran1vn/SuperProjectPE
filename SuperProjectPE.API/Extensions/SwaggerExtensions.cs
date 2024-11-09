using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SuperProjectPE.API.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Your API", Version = "v1" });
            // options

            // Define the security scheme for JWT Bearer
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token in the text input below.\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR...' "
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            
            // Configure Swagger to work with OData
            options.DocInclusionPredicate((docName, apiDesc) => true);
            
            options.OperationFilter<ODataOperationFilter>();

            options.EnableAnnotations();
            
        });
    }
}

public class ODataOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

        var oDataQueryAttributes = context.ApiDescription.ActionDescriptor.EndpointMetadata
            .OfType<Microsoft.AspNetCore.OData.Query.EnableQueryAttribute>();

        if (oDataQueryAttributes.Any())
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$select",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Description = "Select specific fields (comma-separated)"
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$filter",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Description = "Filter results using OData syntax"
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$orderby",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema { Type = "string" },
                Required = false,
                Description = "Order results (e.g., name desc, price asc)"
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$skip",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema { Type = "integer" },
                Required = false,
                Description = "Skip number of records"
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$top",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema { Type = "integer" },
                Required = false,
                Description = "Take number of records"
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$count",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema { Type = "boolean" },
                Required = false,
                Description = "Include count of total records"
            });
        }
    }
}