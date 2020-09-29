using System;
using System.Net;
using FiscalNg.Common.Exceptions;

namespace FiscalNg.Api.Helpers
{
    /// <summary>
    /// Extensions for Exception
    /// </summary>
    public static class ExceptionHelper {
        /// <summary>
        /// Return correct status code for exception.
        /// By default 500 code
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static int GetStatusCode(this Exception ex)
        {
            switch (ex)
            {
                case FiscalNgArgumentException _:
                    return (int)HttpStatusCode.BadRequest;
                case FiscalNgUnauthorizedException _:
                    return (int)HttpStatusCode.Unauthorized;
                case FiscalNgSecurityException _:
                    return (int)HttpStatusCode.Forbidden;
                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }
	}
}
