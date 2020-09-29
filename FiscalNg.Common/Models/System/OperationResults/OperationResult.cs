namespace FiscalNg.Common.Models.System.OperationResults
{
    /// <summary>
    /// Internal operation result wrapper
    /// </summary>
    public class OperationResult<T> {
        /// <summary>
        /// Request result
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Request success indicator
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string Error { get; set; }

        protected internal OperationResult(bool success, string error, T data = default) {
            Success = success;
            Error = error;
            Data = data;
        }
	}

    public class OperationResult : OperationResult<string> {
        protected OperationResult(bool success, string error, string data = default) : base(success, error, data) { }

        public static OperationResult Fail(string error = null)
        {
            return new OperationResult(false, error);
        }

        public static OperationResult Ok(string data = default)
        {
            return new OperationResult(true, null, data);
        }

        /// <summary>
        /// Returns failure result
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns><see cref="OperationResult{T}"/></returns>
        public static OperationResult<TResult> Fail<TResult>(string error)
        {
            return new OperationResult<TResult>(false, error);
        }

        /// <summary>
        /// Returns success result
        /// </summary>
        /// <param name="data">Result data</param>
        /// <returns><see cref="OperationResult{T}"/></returns>
        public static OperationResult<TResult> Ok<TResult>(TResult data = default)
        {
            return new OperationResult<TResult>(true, null, data);
        }
    }
}
