using System;
using System.Net;
using System.Reflection;
using System.Text;
using FiscalNg.Api.Helpers;
using FiscalNg.Common.Extensions;
using FiscalNg.Common.Helpers;
using FiscalNg.Common.Models.System.OperationResults;
using FiscalNg.Database.AccessLog.Models.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Serilog;
using DateTime = System.DateTime;

namespace FiscalNg.Api.Filters {
    /// <summary>
    /// Store the logs for FiscalNgApi controller accesses
    /// </summary>
    public class LoggingFilterAttribute : ActionFilterAttribute {
        private const int StringMaxLength = 4000;

        private readonly LogData _data = new LogData {
            ApiVersion = Assembly.GetEntryAssembly()?.GetName().Version.ToString()
        };

        public LoggingFilterAttribute() {
            
        }

        /// <summary>
        /// Before the controller execution stores the access parameters and time,
        /// which would be written to db after the controller named execution
        /// </summary>
        /// <param name="filterContext"><see cref="ActionExecutingContext"/>object. </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            try {
                _data.EndpointName = filterContext.HttpContext?.Request.Host + filterContext.HttpContext?.Request.Path;
                _data.HttpMethod = filterContext.HttpContext?.Request.Method.ToUpper();
                _data.ClientIp = filterContext.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                var str = new StringBuilder(0, StringMaxLength);
                foreach (var arg in filterContext.ActionArguments) {
                    str.Append(arg.Key);
                    str.Append("=");
                    str.Append(JsonConvert.SerializeObject(arg.Value, new PasswordReplaceJsonConverter()));
                    str.Append("; ");
                }

                _data.Parameters = str.ToString().TrimEnd().TrimEnd(';').Truncate(StringMaxLength);

                _data.OperationStart = DateTime.Now;
            }
            catch (Exception e) {
                // Just log the error and don't propagate it, as this is internal issue.
                // would then be returned as result to user
               Log.Error(e, "AccessLog exception:");

            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            try {
                _data.DurationMs = (int)(DateTime.Now - _data.OperationStart).TotalMilliseconds;
                string str;

                if (filterContext.Result is OkObjectResult okObjectResult)
                {
                    str = JsonConvert.SerializeObject(okObjectResult.Value);
                }
                else if (filterContext.Result is ObjectResult objectResult)
                {
                    str = JsonConvert.SerializeObject(objectResult.Value);

                    if (objectResult.StatusCode != null)
                    {
                        _data.HttpResult = (int)objectResult.StatusCode;
                    }
                    else
                    {
                        _data.HttpResult = (int)HttpStatusCode.BadRequest;
                    }

                    var requestResult = objectResult.Value as RequestResult;
                    var errorMessages = requestResult?.GetErrorMessages() ?? string.Empty;
                    _data.ExceptionMsg = errorMessages.Truncate(StringMaxLength);
                }
                else if (filterContext.Exception != null)
                {
                    str = JsonConvert.SerializeObject(filterContext.Exception);
                    _data.HttpResult = filterContext.Exception.GetStatusCode();
                    _data.ExceptionMsg = filterContext.Exception.Message.Truncate(StringMaxLength);
                }
                else
                {
                    str = JsonConvert.SerializeObject(filterContext.Result);
                    _data.HttpResult = filterContext.HttpContext.Response.StatusCode;
                }

                _data.OutputJSON = str.Truncate(StringMaxLength);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
