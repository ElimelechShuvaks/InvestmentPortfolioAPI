using System;
using System.Net;

namespace InvestmentPortfolioAPI.Exceptions
{
    /// <summary>
    /// Represents errors that occur during API calls.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code returned by the API.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class with a specified error message and status code.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        public ApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class with a specified error message, status code, and a reference to the inner exception that caused this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="statusCode">The HTTP status code returned by the API.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ApiException(string message, HttpStatusCode statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
