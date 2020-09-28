using System;
using FiscalNg.Common.Exceptions.Enums;
using FiscalNg.Common.Exceptions.Interfaces;

namespace FiscalNg.Common.Exceptions {
    /// <summary>
    /// ArgumentNullException wrapper for FiscalNg
    /// </summary>
    public class FiscalNgArgumentNullException : ArgumentNullException, IFiscalNgBaseException {
        /// <inheritdoc />
        public ErrorCode Result { get; }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="argumentName">Argument that caused the exception</param>
        /// <param name="result"><see cref="ErrorCode"/></param>
        public FiscalNgArgumentNullException(string argumentName, string message, ErrorCode result)
            : base(argumentName, message) {
            Result = result;
        }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="result"><see cref="ErrorCode"/></param>
        /// <param name="innerException">Original exception</param>
        public FiscalNgArgumentNullException(string message, ErrorCode result, Exception innerException)
            : base(message, innerException) {
            Result = result;
        }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="argumentName">Argument that caused the exception</param>
        /// <param name="result"><see cref="ErrorCode"/></param>
        public FiscalNgArgumentNullException(string argumentName, ErrorCode result)
            : base(argumentName)
        {
            Result = result;
        }
	}
}
