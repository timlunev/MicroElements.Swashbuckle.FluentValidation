// Copyright (c) MicroElements. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentValidation.Validators;
using NJsonSchema;
using NJsonSchema.Generation;

namespace MicroElements.OpenApi.FluentValidation
{
    /// <summary>
    /// RuleContext.
    /// </summary>
    public class RuleContext
    {
        /// <summary>
        /// SchemaProcessorContext.
        /// </summary>
        public JsonSchema Schema { get; }

        /// <summary>
        /// Property name.
        /// </summary>
        public string PropertyKey { get; }

        /// <summary>
        /// Property validator.
        /// </summary>
        public IPropertyValidator PropertyValidator { get; }

        /// <summary>
        /// Gets value indicating that <see cref="PropertyValidator"/> should be applied to collection item instead of property.
        /// </summary>
        public bool IsCollectionValidator { get; }

        /// <summary>
        /// Gets target property schema.
        /// </summary>
        //public JsonSchemaProperty Property => !IsCollectionValidator ? Schema.Schema.Properties[PropertyKey] : Schema.Schema.Properties[PropertyKey].Items;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleContext"/> class.
        /// </summary>
        /// <param name="schema">Swagger schema.</param>
        /// <param name="propertyKey">Property name.</param>
        /// <param name="propertyValidator">Property validator.</param>
        /// <param name="isCollectionValidator">Should be applied to collection items.</param>
        public RuleContext(
            JsonSchema schema,
            string propertyKey,
            IPropertyValidator propertyValidator,
            bool isCollectionValidator = false)
        {
            Schema = schema;
            PropertyKey = propertyKey;
            PropertyValidator = propertyValidator;
            IsCollectionValidator = isCollectionValidator;
        }
    }
}