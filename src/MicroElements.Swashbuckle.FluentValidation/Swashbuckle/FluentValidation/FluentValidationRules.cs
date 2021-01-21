// Copyright (c) MicroElements. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentValidation;
using MicroElements.OpenApi.FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MicroElements.Swashbuckle.FluentValidation
{
    /// <summary>
    /// Swagger <see cref="ISchemaFilter"/> that uses FluentValidation validators instead System.ComponentModel based attributes.
    /// </summary>
    public class FluentValidationRules : ISchemaFilter
    {
        private readonly IValidatorFactory? _validatorFactory;
        private readonly ILogger _logger;
        private readonly IReadOnlyList<FluentValidationRule> _rules;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentValidationRules"/> class.
        /// </summary>
        /// <param name="validatorFactory">Validator factory.</param>
        /// <param name="rules">External FluentValidation rules. Rule with the same name replaces default rule.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> for logging. Can be null.</param>
        public FluentValidationRules(
            IValidatorFactory? validatorFactory = null,
            IEnumerable<FluentValidationRule>? rules = null,
            ILoggerFactory? loggerFactory = null)
        {
            _validatorFactory = validatorFactory;
            _logger = loggerFactory?.CreateLogger(typeof(FluentValidationRules)) ?? NullLogger.Instance;
            _rules = FluentValidationRuleProvider.CreateDefaultRules().OverrideRules(rules);
        }

        /// <inheritdoc />
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            Type schemaType = context.Type;

            FluentValidationSchemaBuilder.ApplyRulesToSchema(
                schema: schema,
                schemaType: schemaType,
                validatorFactory: _validatorFactory,
                rules: _rules,
                logger: _logger);
        }
    }
}