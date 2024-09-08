namespace SalesApi.CrossCutting.Exceptions;

public class SalesApiErrorResult
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
