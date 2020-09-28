using System;
using System.Security;
using FiscalNg.Common.Exceptions.Enums;
using FiscalNg.Common.Exceptions.Interfaces;

namespace FiscalNg.Common.Exceptions {
    /// <summary>
    /// FiscalNg extension to ArgumentException
    /// </summary>
    public class FiscalNgSecurityException : SecurityException, IFiscalNgBaseException {
        /// <inheritdoc />
        public ErrorCode Result { get; }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="result"><see cref="ErrorCode"/></param>
        /// <param name="innerException">Original exception</param>
        public FiscalNgSecurityException(string message, ErrorCode result, Exception innerException)
            : base(message, innerException) {
            Result = result;
        }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="result"><see cref="ErrorCode"/></param>
        public FiscalNgSecurityException(string message, ErrorCode result)
            : base(message) {
            Result = result;
        }

        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="result"><see cref="Result"/></param>
        public FiscalNgSecurityException(ErrorCode result) {
            Result = result;
        }
    }
}
