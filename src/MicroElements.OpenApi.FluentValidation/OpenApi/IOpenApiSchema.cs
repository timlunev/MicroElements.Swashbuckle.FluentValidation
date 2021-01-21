// Copyright (c) MicroElements. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace MicroElements.OpenApi
{
    public interface IOpenApiSchema
    {
        /// <summary>
        /// Follow JSON Schema definition. Short text providing information about the data.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// Value MUST be a string. Multiple types via an array are not supported.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// While relying on JSON Schema's defined formats,
        /// the OAS offers a few additional predefined formats.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public decimal? Maximum { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public bool? ExclusiveMaximum { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public decimal? Minimum { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public bool? ExclusiveMinimum { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// This string SHOULD be a valid regular expression, according to the ECMA 262 regular expression dialect
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public decimal? MultipleOf { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// The default value represents what would be assumed by the consumer of the input as the value of the schema if one is not provided.
        /// Unlike JSON Schema, the value MUST conform to the defined type for the Schema Object defined at the same level.
        /// For example, if type is string, then default can be "foo" but cannot be 1.
        /// </summary>
        public IOpenApiAny Default { get; set; }

        /// <summary>
        /// Relevant only for Schema "properties" definitions. Declares the property as "read only".
        /// This means that it MAY be sent as part of a response but SHOULD NOT be sent as part of the request.
        /// If the property is marked as readOnly being true and is in the required list,
        /// the required will take effect on the response only.
        /// A property MUST NOT be marked as both readOnly and writeOnly being true.
        /// Default value is false.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Relevant only for Schema "properties" definitions. Declares the property as "write only".
        /// Therefore, it MAY be sent as part of a request but SHOULD NOT be sent as part of the response.
        /// If the property is marked as writeOnly being true and is in the required list,
        /// the required will take effect on the request only.
        /// A property MUST NOT be marked as both readOnly and writeOnly being true.
        /// Default value is false.
        /// </summary>
        public bool WriteOnly { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public IList<IOpenApiSchema> AllOf { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public IList<IOpenApiSchema> OneOf { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public IList<IOpenApiSchema> AnyOf { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public OpenApiSchema Not { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public ISet<string> Required { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// Value MUST be an object and not an array. Inline or referenced schema MUST be of a Schema Object
        /// and not a standard JSON Schema. items MUST be present if the type is array.
        /// </summary>
        public OpenApiSchema Items { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public int? MaxItems { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public int? MinItems { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public bool? UniqueItems { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// Property definitions MUST be a Schema Object and not a standard JSON Schema (inline or referenced).
        /// </summary>
        public IDictionary<string, IOpenApiSchema> Properties { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public int? MaxProperties { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public int? MinProperties { get; set; }

        /// <summary>
        /// Indicates if the schema can contain properties other than those defined by the properties map.
        /// </summary>
        public bool AdditionalPropertiesAllowed { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// Value can be boolean or object. Inline or referenced schema
        /// MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public OpenApiSchema AdditionalProperties { get; set; }


        /// <summary>
        /// Adds support for polymorphism. The discriminator is an object name that is used to differentiate
        /// between other schemas which may satisfy the payload description.
        /// </summary>
        public OpenApiDiscriminator Discriminator { get; set; }

        /// <summary>
        /// A free-form property to include an example of an instance for this schema.
        /// To represent examples that cannot be naturally represented in JSON or YAML,
        /// a string value can be used to contain the example with escaping where necessary.
        /// </summary>
        public IOpenApiAny Example { get; set; }

        /// <summary>
        /// Follow JSON Schema definition: https://tools.ietf.org/html/draft-fge-json-schema-validation-00
        /// </summary>
        public IList<IOpenApiAny> Enum { get; set; }

        /// <summary>
        /// Allows sending a null value for the defined schema. Default value is false.
        /// </summary>
        public bool Nullable { get; set; }
    }

    public class MsOpenApiSchema : IOpenApiSchema
    {
        private OpenApiSchema _openApiSchemaImplementation;

        /// <inheritdoc />
        public string Title
        {
            get => _openApiSchemaImplementation.Title;
            set => _openApiSchemaImplementation.Title = value;
        }

        /// <inheritdoc />
        public string Type
        {
            get => _openApiSchemaImplementation.Type;
            set => _openApiSchemaImplementation.Type = value;
        }

        /// <inheritdoc />
        public string Format
        {
            get => _openApiSchemaImplementation.Format;
            set => _openApiSchemaImplementation.Format = value;
        }

        /// <inheritdoc />
        public string Description
        {
            get => _openApiSchemaImplementation.Description;
            set => _openApiSchemaImplementation.Description = value;
        }

        /// <inheritdoc />
        public decimal? Maximum
        {
            get => _openApiSchemaImplementation.Maximum;
            set => _openApiSchemaImplementation.Maximum = value;
        }

        /// <inheritdoc />
        public bool? ExclusiveMaximum
        {
            get => _openApiSchemaImplementation.ExclusiveMaximum;
            set => _openApiSchemaImplementation.ExclusiveMaximum = value;
        }

        /// <inheritdoc />
        public decimal? Minimum
        {
            get => _openApiSchemaImplementation.Minimum;
            set => _openApiSchemaImplementation.Minimum = value;
        }

        /// <inheritdoc />
        public bool? ExclusiveMinimum
        {
            get => _openApiSchemaImplementation.ExclusiveMinimum;
            set => _openApiSchemaImplementation.ExclusiveMinimum = value;
        }

        /// <inheritdoc />
        public int? MaxLength
        {
            get => _openApiSchemaImplementation.MaxLength;
            set => _openApiSchemaImplementation.MaxLength = value;
        }

        /// <inheritdoc />
        public int? MinLength
        {
            get => _openApiSchemaImplementation.MinLength;
            set => _openApiSchemaImplementation.MinLength = value;
        }

        /// <inheritdoc />
        public string Pattern
        {
            get => _openApiSchemaImplementation.Pattern;
            set => _openApiSchemaImplementation.Pattern = value;
        }

        /// <inheritdoc />
        public decimal? MultipleOf
        {
            get => _openApiSchemaImplementation.MultipleOf;
            set => _openApiSchemaImplementation.MultipleOf = value;
        }

        /// <inheritdoc />
        public IOpenApiAny Default
        {
            get => _openApiSchemaImplementation.Default;
            set => _openApiSchemaImplementation.Default = value;
        }

        /// <inheritdoc />
        public bool ReadOnly
        {
            get => _openApiSchemaImplementation.ReadOnly;
            set => _openApiSchemaImplementation.ReadOnly = value;
        }

        /// <inheritdoc />
        public bool WriteOnly
        {
            get => _openApiSchemaImplementation.WriteOnly;
            set => _openApiSchemaImplementation.WriteOnly = value;
        }

        /// <inheritdoc />
        public IList<IOpenApiSchema> AllOf
        {
            get => _openApiSchemaImplementation.AllOf;
            set => _openApiSchemaImplementation.AllOf = value;
        }

        /// <inheritdoc />
        public IList<IOpenApiSchema> OneOf
        {
            get => _openApiSchemaImplementation.OneOf;
            set => _openApiSchemaImplementation.OneOf = value;
        }

        /// <inheritdoc />
        public IList<IOpenApiSchema> AnyOf
        {
            get => _openApiSchemaImplementation.AnyOf;
            set => _openApiSchemaImplementation.AnyOf = value;
        }

        /// <inheritdoc />
        public OpenApiSchema Not
        {
            get => _openApiSchemaImplementation.Not;
            set => _openApiSchemaImplementation.Not = value;
        }

        /// <inheritdoc />
        public ISet<string> Required
        {
            get => _openApiSchemaImplementation.Required;
            set => _openApiSchemaImplementation.Required = value;
        }

        /// <inheritdoc />
        public OpenApiSchema Items
        {
            get => _openApiSchemaImplementation.Items;
            set => _openApiSchemaImplementation.Items = value;
        }

        /// <inheritdoc />
        public int? MaxItems
        {
            get => _openApiSchemaImplementation.MaxItems;
            set => _openApiSchemaImplementation.MaxItems = value;
        }

        /// <inheritdoc />
        public int? MinItems
        {
            get => _openApiSchemaImplementation.MinItems;
            set => _openApiSchemaImplementation.MinItems = value;
        }

        /// <inheritdoc />
        public bool? UniqueItems
        {
            get => _openApiSchemaImplementation.UniqueItems;
            set => _openApiSchemaImplementation.UniqueItems = value;
        }

        /// <inheritdoc />
        public IDictionary<string, IOpenApiSchema> Properties
        {
            get => _openApiSchemaImplementation.Properties;
            set => _openApiSchemaImplementation.Properties = value;
        }

        /// <inheritdoc />
        public int? MaxProperties
        {
            get => _openApiSchemaImplementation.MaxProperties;
            set => _openApiSchemaImplementation.MaxProperties = value;
        }

        /// <inheritdoc />
        public int? MinProperties
        {
            get => _openApiSchemaImplementation.MinProperties;
            set => _openApiSchemaImplementation.MinProperties = value;
        }

        /// <inheritdoc />
        public bool AdditionalPropertiesAllowed
        {
            get => _openApiSchemaImplementation.AdditionalPropertiesAllowed;
            set => _openApiSchemaImplementation.AdditionalPropertiesAllowed = value;
        }

        /// <inheritdoc />
        public OpenApiSchema AdditionalProperties
        {
            get => _openApiSchemaImplementation.AdditionalProperties;
            set => _openApiSchemaImplementation.AdditionalProperties = value;
        }

        /// <inheritdoc />
        public OpenApiDiscriminator Discriminator
        {
            get => _openApiSchemaImplementation.Discriminator;
            set => _openApiSchemaImplementation.Discriminator = value;
        }

        /// <inheritdoc />
        public IOpenApiAny Example
        {
            get => _openApiSchemaImplementation.Example;
            set => _openApiSchemaImplementation.Example = value;
        }

        /// <inheritdoc />
        public IList<IOpenApiAny> Enum
        {
            get => _openApiSchemaImplementation.Enum;
            set => _openApiSchemaImplementation.Enum = value;
        }

        /// <inheritdoc />
        public bool Nullable
        {
            get => _openApiSchemaImplementation.Nullable;
            set => _openApiSchemaImplementation.Nullable = value;
        }
    }
}
