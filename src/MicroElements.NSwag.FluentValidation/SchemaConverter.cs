using Microsoft.OpenApi.Models;
using NJsonSchema;

namespace MicroElements.OpenApi.FluentValidation
{
    public static class SchemaConverter
    {
        public static OpenApiSchema ToOpenApiSchema(JsonSchema jsonSchema)
        {
            return new OpenApiSchema();
        }
    }

    public class NJsonSchemaAdapter : OpenApiSchema
    {
        private JsonSchema jsonSchema;

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public decimal? Minimum { get; set; }
    }
}