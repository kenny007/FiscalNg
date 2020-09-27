using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FiscalNg.Core.Validations.Base {
    public class BaseValidator<T> : AbstractValidator<T>, IValidatorInterceptor {

        public IValidationContext BeforeMvcValidation(ControllerContext controllerContext, IValidationContext validationContext) {
			return validationContext;
		}

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, IValidationContext validationContext, ValidationResult result) {
            var projections = result.Errors.Select(failure => {
                var messageModel = new FluentValidationResultMetaData(failure.PropertyName, failure.AttemptedValue, 
                    failure.ErrorCode, failure.ErrorMessage, failure.FormattedMessagePlaceholderValues);

                return new ValidationFailure(failure.PropertyName, JsonConvert.SerializeObject(messageModel));
            });

            return new ValidationResult(projections);
        }

    }

    public class FluentValidationResultMetaData
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, object> MessagePlaceHolders { get; set; }

        public FluentValidationResultMetaData(string propertyName, object propertyValue, string errorCode, 
            string errorMessage, Dictionary<string, object> messagePlaceHolders)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            MessagePlaceHolders = messagePlaceHolders;
        }

        public string GetErrorTranslationKey() {
            return ErrorCode.Replace('_', '.');
        }

        public string GetPropertyName() {
            return PropertyName.Split('.').LastOrDefault();
        }

        public string GetErrorCode(bool isFluentValidationMessage) {
            var propertyName = GetPropertyName();

            return !string.IsNullOrWhiteSpace(propertyName) && isFluentValidationMessage &&
                   !string.IsNullOrWhiteSpace(ErrorCode)
                ? $"{propertyName}_{ErrorCode.Replace("Validator",string.Empty)}"
                : ErrorCode?.Replace("Validator", string.Empty);
        }
    }
}
