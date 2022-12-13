namespace ToolsControl.BLL.Models.Responses;

public class ResultResponse
{
    public bool Success { get; set; }

    public IEnumerable<string>? Errors { get; set; }
}