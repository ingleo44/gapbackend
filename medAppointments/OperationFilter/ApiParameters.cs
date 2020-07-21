using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace medAppointments.OperationFilter
{
    public class ApiParameters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var isOdata = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is EnableQueryAttribute);
            if (!isOdata) return;

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$filter",
                Description = "Filtra los Resultados usando la Sintaxis de OData.",
                Required = false,
                AllowEmptyValue = true,
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$select",
                Description = "Selecciona los campos a mostrar",
                Required = false,
                AllowEmptyValue = true,
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "String"

                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$orderby",
                Description = "Ordena los resultados usando la sintaxis Odata.",
                Required = false,
                AllowEmptyValue = true,
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "String"

                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$skip",
                Description = "El número de resultados para saltar.",
                Required = false,
                AllowEmptyValue = true,
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "String"

                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$top",
                Description = "El número de resultados a mostrar.",
                Required = false,
                AllowEmptyValue = true,
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "String"

                }
            });
        }
    }
}