using System;
using System.Net;
using FiscalNg.Common.Exceptions.Enums;

namespace FiscalNg.Common.Exceptions {
    /// <summary>
    /// Base exception for all api exceptions. Return http error 500 
    /// </summary>
    public class FiscalNgBaseException : Exception {

        /// <summary>
        /// Http status code
        /// </summary>
        public virtual int StatusCode { get; } = (int)HttpStatusCode.InternalServerError;

        /// <summary>
        /// A short, human-readable summary of the problem
        /// </summary>
        public virtual string Title { get; } = "There has been an error when processing your request.";

        /// <summary>
        /// Error code presented as a number in string. Look supported error codes from <see cref="ErrorCode"/> enum.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Base constructor
        /// </summary>
        public FiscalNgBaseException() {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode">Error code. See <see cref="ErrorCode"/> for supported codes</param>
        public FiscalNgBaseException(string message, ErrorCode errorCode)
            : base(message) {
            ErrorCode = (int)errorCode;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode">Error code</param>
        /// <param name="innerException"></param>
        public FiscalNgBaseException(string message, ErrorCode errorCode, Exception innerException)
            : base(message, innerException) {
            ErrorCode = (int)errorCode;
        }
	}
}
