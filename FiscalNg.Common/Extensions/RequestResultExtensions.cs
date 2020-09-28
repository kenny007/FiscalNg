using System.Linq;
using FiscalNg.Common.Models.System.OperationResults;

namespace FiscalNg.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="RequestResult"/> and <see cref="RequestResult{T}"/>
    /// </summary>
    public static  class RequestResultExtensions {
        /// <summary>
        /// Combines error messages in one string
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string GetErrorMessages(this RequestResult result)
        {
            var errMessages = result.Error.Messages.Select(x => x.Text);
            var jointErrorMessages = string.Join('/', errMessages);

            return jointErrorMessages;
        }

        /// <summary>
        /// Combines error messages in one string
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string GetErrorMessages<T>(this RequestResult<T> result)
        {
            var errMessages = result.Error.Messages.Select(x => x.Text);
            var jointErrorMessages = string.Join('/', errMessages);

            return jointErrorMessages;
        }

        /// <summary>
        /// Combines error messages in one string
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string GetErrorCodes<T>(this RequestResult<T> result)
        {
            var errCodes = result.Error.Messages.Select(x => x.ErrorCode);
            var jointCodes = string.Join('/', errCodes);

            return jointCodes;
        }
    }
}
