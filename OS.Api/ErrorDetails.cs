using System.Text.Json;

namespace OS.Api
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public List<string> Errors { get; set; } = new List<string>();

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
