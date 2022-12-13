namespace ToolsControl.BLL.Models.Responses;

public class SigninResponse
{
    public string AccessToken { get; set; } = string.Empty;
    
    public string RefreshToken { get; set; } = string.Empty;
}