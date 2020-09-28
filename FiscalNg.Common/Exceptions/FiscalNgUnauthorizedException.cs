using System;
using System.Net;
using FiscalNg.Common.Exceptions.Enums;

namespace FiscalNg.Common.Exceptions {
   public class FiscalNgUnauthorizedException: FiscalNgBaseException {
       public override int StatusCode { get; } = (int)HttpStatusCode.Unauthorized;

       /// <inheritdoc />
       public override string Title { get; } = "User is unauthorized.";

       /// <inheritdoc />
       public FiscalNgUnauthorizedException(string message, ErrorCode errorCode) : base(message, errorCode) {
       }

       /// <inheritdoc />
       public FiscalNgUnauthorizedException(string message, ErrorCode errorCode, Exception innerException) : base(message, errorCode, innerException) {
       }
   }
}
