namespace PezzaApi.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; } // Optional, include for debugging only
    }
}