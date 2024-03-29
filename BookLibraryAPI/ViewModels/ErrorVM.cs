using Newtonsoft.Json;

namespace BookLibraryAPI.ViewModels
{
    public class ErrorVM
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}