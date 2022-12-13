namespace ToolsControl.BLL.Models.Responses;

public class FileResponse
{
    public byte[] Content { get; set; } = Array.Empty<byte>();

    public string ContentType { get; set; } = string.Empty;
}