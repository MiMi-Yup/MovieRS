using System.Net;
using System.Text.Json;

namespace MovieRS.API.Error
{
    public class ApiException : Exception
    {
        private string _message;
        public string[] Errors { get; set; }
        public new string Message
        {
            get => _message;
            set
            {
                _message = value;
            }
        }
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(string message = "Server Error", string[]? errors = null) : base(message)
        {
            StatusCode = HttpStatusCode.InternalServerError;
            _message = message;
            Errors = errors != null ? errors : new string[] { message };
        }

        public ApiException(string message = "Server Error", HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string[]? errors = null) : base(message)
        {
            StatusCode = statusCode;
            _message = message;
            Errors = errors != null ? errors : new string[] { message };
        }

        public override string ToString() => JsonSerializer.Serialize(new
        {
            message = Message,
            statusCode = StatusCode,
            errors = Errors
        });
    }
}
