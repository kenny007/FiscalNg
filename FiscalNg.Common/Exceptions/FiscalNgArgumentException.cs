using System;
using FiscalNg.Common.Exceptions.Enums;
using FiscalNg.Common.Exceptions.Interfaces;

namespace FiscalNg.Common.Exceptions {
    /// <summary>
    /// FiscalNg extension to ArgumentException
    /// </summary>
    public class FiscalNgArgumentException: ArgumentException, IFiscalNgBaseException {
        /// <inheritdoc />
        public ErrorCode Result { get; }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="argumentName"></param>
        /// <param name="result"></param>
        public FiscalNgArgumentException(string message, string argumentName, ErrorCode result) : base(message, argumentName) {
            Result = result;
        }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="argumentName">Argument that caused the exception</param>
        /// <param name="result"><see cref="ErrorCode"/></param>
        /// <param name="innerException">Original exception</param>
        public FiscalNgArgumentException(string message, string argumentName, ErrorCode result, Exception innerException)
            : base(message, argumentName, innerException) {
            Result = result;
        }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="result"><see cref="ErrorCode"/></param>
        /// <param name="innerException">Original exception</param>
        public FiscalNgArgumentException(string message, ErrorCode result, Exception innerException)
            : base(message, innerException) {
            Result = result;
        }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="result"><see cref="ErrorCode"/></param>
        public FiscalNgArgumentException(string message, ErrorCode result)
            : base(message) {
            Result = result;
        }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="result"><see cref="Result"/></param>
        public FiscalNgArgumentException(ErrorCode result) {
            Result = result;
        }
    }
}
