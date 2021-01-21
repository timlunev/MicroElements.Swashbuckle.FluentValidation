// Copyright (c) MicroElements. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using MicroElements.FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MicroElements.OpenApi.FluentValidation
{
    /// <summary>
    /// OpenApi schema builder.
    /// </summary>
    public static class FluentValidationSchemaBuilder
    {
        public static void ApplyRulesToSchema(
            OpenApiSchema schema,
            Type schemaType,
            IValidatorFactory? validatorFactory,
            IReadOnlyCollection<FluentValidationRule> rules,
            ILogger logger)
        {
            if (validatorFactory == null)
            {
                logger.LogWarning(0, "ValidatorFactory is not provided. Please register FluentValidation.");
                return;
            }

            if (schema == null)
                return;

            IValidator? validator = null;

            try
            {
                validator = validatorFactory.GetValidator(schemaType);
            }
            catch (Exception e)
            {
                logger.LogWarning(0, e, $"GetValidator for type '{schemaType}' fails.");
            }

            if (validator == null)
                return;

            ApplyRulesToSchema(
                schema: schema,
                schemaType: schemaType,
                schemaPropertyNames: null,
                validator: validator,
                rules: rules,
                logger: logger);

            try
            {
                AddRulesFromIncludedValidators(
                    schema: schema,
                    schemaType: schemaType,
                    validator: validator,
                    rules: rules,
                    logger: logger);
            }
            catch (Exception e)
            {
                logger.LogWarning(0, e, $"Applying IncludeRules for type '{schemaType}' fails.");
            }
        }

        /// <summary>
        /// Applies rules from validator.
        /// </summary>
        public static void ApplyRulesToSchema(
            OpenApiSchema schema,
            Type schemaType,
            IEnumerable<string>? schemaPropertyNames,
            IValidator validator,
            IReadOnlyCollection<FluentValidationRule> rules,
            ILogger logger)
        {
            var schemaTypeName = schemaType.Name;

            var lazyLog = new LazyLog(logger,
                l => l.LogDebug($"Applying FluentValidation rules to swagger schema '{schemaTypeName}'."));

            schemaPropertyNames ??= schema.Properties?.Keys ?? Array.Empty<string>();
            foreach (var schemaPropertyName in schemaPropertyNames)
            {
                var validationRules = validator.GetValidationRulesForMemberIgnoreCase(schemaPropertyName).ToArrayDebug();

                foreach (var ruleContext in validationRules)
                {
                    var propertyValidators = ruleContext.PropertyRule.Validators;
                    foreach (var propertyValidator in propertyValidators)
                    {
                        foreach (var rule in rules)
                        {
                            if (rule.Matches(propertyValidator))
                            {
                                try
                                {
                                    var ruleHistoryItem = new RuleHistoryCache.RuleHistoryItem(schemaTypeName, schemaPropertyName, propertyValidator, rule.Name);
                                    if (!schema.ContainsRuleHistoryItem(ruleHistoryItem))
                                    {
                                        lazyLog.LogOnce();

                                        rule.Apply(new RuleContext(
                                            schema,
                                            schemaPropertyName,
                                            propertyValidator,
                                            isCollectionValidator: ruleContext.IsCollectionRule));

                                        logger.LogDebug($"Rule '{rule.Name}' applied for property '{schemaTypeName}.{schemaPropertyName}'.");
                                        schema.AddRuleHistoryItem(ruleHistoryItem);
                                    }
                                    else
                                    {
                                        logger.LogDebug($"Rule '{rule.Name}' already applied for property '{schemaTypeName}.{schemaPropertyName}'.");
                                    }
                                }
                                catch (Exception e)
                                {
                                    logger.LogWarning(0, e, $"Error on apply rule '{rule.Name}' for property '{schemaTypeName}.{schemaPropertyName}'.");
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void AddRulesFromIncludedValidators(
            OpenApiSchema schema,
            Type schemaType,
            IValidator validator,
            IReadOnlyCollection<FluentValidationRule> rules,
            ILogger logger)
        {
            // Note: IValidatorDescriptor doesn't return IncludeRules so we need to get validators manually.
            var validationRules = (validator as IEnumerable<IValidationRule>)
                .NotNull()
                .OfType<PropertyRule>()
                .Where(includeRule => includeRule.HasNoCondition())
                .ToArray();

            var childAdapters = validationRules
                .Where(rule => rule is IIncludeRule)
                .SelectMany(includeRule => includeRule.Validators)
                .OfType<IChildValidatorAdaptor>();

            var childAdapters2 = validationRules
                .SelectMany(rule => rule.Validators)
                .OfType<IChildValidatorAdaptor>()
                .ToArrayDebug();

            childAdapters = childAdapters.Concat(childAdapters2);

            foreach (var childAdapter in childAdapters)
            {
                IValidator? includedValidator = childAdapter.GetValidatorFromChildValidatorAdapter();
                if (includedValidator != null)
                {
                    ApplyRulesToSchema(
                        schema: schema,
                        schemaType: schemaType,
                        schemaPropertyNames: null,
                        validator: includedValidator,
                        rules: rules,
                        logger: logger);

                    AddRulesFromIncludedValidators(
                        schema: schema,
                        schemaType: schemaType,
                        validator: includedValidator,
                        rules: rules,
                        logger: logger);
                }
            }
        }

        public static IValidator? GetValidatorFromChildValidatorAdapter(this IChildValidatorAdaptor childValidatorAdapter)
        {
            // Fake context. We have not got real context because no validation yet.
            var fakeContext = new PropertyValidatorContext(new ValidationContext<object>(null), null, string.Empty);

            // Try to validator with reflection.
            var childValidatorAdapterType = childValidatorAdapter.GetType();
            var getValidatorMethod = childValidatorAdapterType.GetMethod(nameof(ChildValidatorAdaptor<object, object>.GetValidator));
            if (getValidatorMethod != null)
            {
                var validator = (IValidator)getValidatorMethod.Invoke(childValidatorAdapter, new[] {fakeContext});
                return validator;
            }

            return null;
        }
    }
}