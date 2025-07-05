namespace TimeTrackerService.Tests
{
    public class ApiResponse<T>
    {
        public T Data { get; set; } = default!;
        public string? Message { get; set; }
        public int? Code { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
