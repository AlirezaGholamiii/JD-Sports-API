using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner.Filters
{
    public class DefaultHeaderFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //if (string.Equals(context.ApiDescription.HttpMethod, HttpMethod.Post.Method, StringComparison.InvariantCultureIgnoreCase))
            //{
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "api-key",
                In = ParameterLocation.Header,
                Required = true,
                Example = new OpenApiString("1s5sa5w5asd5asd5asd")
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "auth-token",
                In = ParameterLocation.Header,
                Required = true,
                Example = new OpenApiString("1234567890")
            });
            //}
        }
    }
}
