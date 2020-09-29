using System.Globalization;
using System.Linq;
using System.Net;
using FiscalNg.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FiscalNg.Common.Models.System.OperationResults {
    public class RequestResult<T> {
        /// <summary>
        /// Request result
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Request success indicator
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Error object with messages
        /// </summary>
        public Error Error { get; set; }
        /// <summary>
        /// Default ctor
        /// </summary>
        protected RequestResult() { }

        protected internal RequestResult(T data, bool success, string errorMessage = null) {
            Data = data;
            Success = success;
            Error = errorMessage != null ? new Error(errorMessage) : null;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="data">Data of type T</param>
        /// <param name="success">Success flag</param>
        /// <param name="error">Error object with messages</param>
        protected internal RequestResult(T data, bool success, Error error) {
            Data = data;
            Success = success;
            Error = error;
        }
        public override string ToString() {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }

    public sealed class RequestResult : RequestResult<string> {
        private RequestResult(bool success, string errorMessage = null): base(null, success, errorMessage) { }

        [JsonConstructor]
        private RequestResult(bool success, Error error) : base(null, success, error) { }

        private RequestResult<T> Ok<T>(T data = default) {
            return new RequestResult<T>(data, true);
        }

        private static RequestResult Ok() {
            return new RequestResult(true);
        }

        public static RequestResult<T> Ok<T>(string errorCode, string errorMessage, T data = default) {
            var message = new ErrorTranslation(errorCode, errorMessage, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            var error = new Error(message.MakeCollection());
            return new RequestResult<T>(data, true, error);
        }

        public static RequestResult Ok(string errorCode, string errorMessage) {
            var message = new ErrorTranslation(errorCode, errorMessage, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            var error = new Error(message.MakeCollection());
            return new RequestResult(true, error);
        }

        public static RequestResult Fail(string errorMessage) {
            return new RequestResult(false, errorMessage);
        }

        public static RequestResult Fail(Error error) {
            return new RequestResult(false, error);
        }

        public static RequestResult Fail(string errorCode, string errorMessage) {
            var message = new ErrorTranslation(errorCode, errorMessage, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            var error = new Error(message.MakeCollection());
            return new RequestResult(false, error);
        }

        public static RequestResult Fail(HttpStatusCode statusCode, string errorCode, string errorMessage) {
            var message = new ErrorTranslation(errorCode, errorMessage, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            var error = new Error(message.MakeCollection(), statusCode);
            return new RequestResult(false, error);
        }

        public static RequestResult<T> Fail<T>(string errorMessage, T data = default) {
            return new RequestResult<T>(data, false, errorMessage);
        }

        public static RequestResult<T> Fail<T>(string[] errorMessages, T data = default) {
            var messages = errorMessages.Select(x =>
                new ErrorTranslation(x, CultureInfo.CurrentCulture.TwoLetterISOLanguageName));
            var error = new Error(messages);
            return new RequestResult<T>(data, false, error);
        }

        public static RequestResult<T> Fail<T>(Error error, T data = default) {
            return new RequestResult<T>(data, false, error);
        }

        public static RequestResult<T> Fail<T>(string errorCode, string errorMessage, T data = default) {
            var message = new ErrorTranslation(errorCode, errorMessage, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            var error = new Error(message.MakeCollection());
            return new RequestResult<T>(data, false, error);
        }

        public static RequestResult<T> Fail<T>(HttpStatusCode statusCode, string errorCode, string errorMessage, T data = default) {
            var message = new ErrorTranslation(errorCode, errorMessage, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            var error = new Error(message.MakeCollection(), statusCode);
            return new RequestResult<T>(data, false, error);
        }
    }

}
