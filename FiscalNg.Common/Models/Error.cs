using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using FiscalNg.Common.Extensions;

namespace FiscalNg.Common.Models {
    /// <summary>
    /// Error structure
    /// </summary>
    public class Error {
        /// <summary>
        /// Error Messages in supported languages 
        /// </summary>
        public List<ErrorTranslation> Messages { get; set; }
        /// <summary>
        /// Used error code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        ///  Exception stack in string format
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Default ctor
        /// </summary>
        public Error() { }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="code">Error code</param>
        public Error(int code) {
            Code = code;
        }
		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="message"></param>
		public Error(string message)
		{
			Messages = new ErrorTranslation(message, CultureInfo.CurrentCulture.TwoLetterISOLanguageName).MakeCollection().ToList();
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="langCode">Language code</param>
		/// <param name="message">Error message</param>
		public Error(string langCode, string message)
		{
			Messages = new ErrorTranslation(message, langCode).MakeCollection().ToList();
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="messages">Array of messages</param>
		/// <param name="langCode">Language code</param>
		public Error(string[] messages, string langCode = null)
		{
			Messages = new List<ErrorTranslation>();
			foreach (var msg in messages)
			{
				Messages.Add(new ErrorTranslation(msg, langCode ?? CultureInfo.CurrentCulture.TwoLetterISOLanguageName));
			}
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="status">Status code</param>
		/// <param name="messages">Array of messages</param>
		/// <param name="langCode">Language code</param>
		public Error(int status, string[] messages, string langCode = null)
		{
			Code = status;
			Messages = new List<ErrorTranslation>();
			foreach (var msg in messages)
			{
				Messages.Add(new ErrorTranslation(msg, langCode ?? CultureInfo.CurrentCulture.TwoLetterISOLanguageName));
			}
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="errors">Errors</param>
		/// <param name="statusCode">Status code</param>
		public Error(IEnumerable<ErrorTranslation> errors, HttpStatusCode statusCode)
		{
			Messages = errors.ToList();
			Code = (int)statusCode;
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="errors">Errors</param>
		public Error(IEnumerable<ErrorTranslation> errors)
		{
			Messages = errors.ToList();
		}
	}

	/// <summary>
	/// Class for storing error translations
	/// </summary>
	public class ErrorTranslation
	{
		/// <summary>
		/// Language codes in ISO 639-2 style 
		/// </summary>
		[StringLength(3, MinimumLength = 3)]
		public string LanguageCode { get; set; }

		/// <summary>
		/// Validation property name
		/// </summary>
		public string PropertyName { get; set; }

		/// <summary>
		/// Translation for given language code 
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Error code
		/// </summary>
		public string ErrorCode { get; set; }

		/// <summary>
		/// Ctor
		/// </summary>
		public ErrorTranslation() { }

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="text"></param>
		/// <param name="langCode"></param>
		public ErrorTranslation(string text, string langCode)
		{
			LanguageCode = langCode;
			Text = text;
		}

		/// <summary>
		/// Ctor
		/// </summary>
		/// <param name="errorCode">Error code</param>
		/// <param name="message">Error message</param>
		/// <param name="languageCode">Language code</param>
		public ErrorTranslation(string errorCode, string message, string languageCode)
		{
			ErrorCode = errorCode;
			Text = message;
			LanguageCode = languageCode;
		}
	}
}
