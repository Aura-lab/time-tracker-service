namespace TimeTrackerService.Dtos
{
    public class ApiResponse<T>
    {
        public T Data { get; set; } = default!;
        public string? Message { get; set; }
        public int? Code { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }

        public ApiResponse() { }

        public ApiResponse(T data, string? message = null, int? code = null)
        {
            Data = data;
            Message = message;
            Code = code;
        }
    }
}
