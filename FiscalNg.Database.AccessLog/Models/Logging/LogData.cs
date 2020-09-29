﻿using System;

namespace FiscalNg.Database.AccessLog.Models.Logging
{
    /// <summary>
    /// Api(s) access logging data
    /// </summary>
    public class LogData {
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// API name - FiscalNgCore etc
        /// </summary>
        public string ApiName { get; set; } = "FiscalNgCore";
        /// <summary>
        /// API version which is in use
        /// </summary>
        public string ApiVersion { get; set; } = string.Empty;

        /// <summary>
        /// Identity company database the request was made to
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// DateTiem stamp when the operation was initiated
        /// </summary>
        public DateTime OperationStart { get; set; }

        /// <summary>
        /// Duration of the request handling in milliseconds
        /// </summary>
        public int DurationMs { get; set; }

        /// <summary>
        /// Name and functionality that was called
        /// </summary>
        public string EndpointName { get; set; } = string.Empty;

        /// <summary>
        /// Parameter values with what the functionality was called
        /// </summary>
        public string Parameters { get; set; } = string.Empty;

        /// <summary>
        /// Operation result
        /// </summary>
        public int HttpResult { get; set; } = 200;

        /// <summary>
        /// Exception that was generated by API, only filled when operation fails
        /// </summary>
        public string ExceptionMsg { get; set; } = string.Empty;

        /// <summary>
        /// Endpoint output. Limited to 4000 symbols.
        /// </summary>
        public string OutputJSON { get; set; } = string.Empty;

        /// <summary>
        /// HTTP method (GET,POST,PUT,DELETE)
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// Client IP address
        /// </summary>
        public string ClientIp { get; set; }
    }
}
