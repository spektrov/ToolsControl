using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.BLL.Models.Responses;

namespace ToolsControl.BLL.Interfaces;

public interface IUserService
{
    public Task<PagedList<UserModel>> GetUsersAsync(UserParameters parameters);

    public Task<ResultResponse> SignupAsync(UserModel signupRequest, string password);

    public Task<SigninResponse> SigninAsync(string email, string password);

    public Task<UserModel> GetByIdAsync(Guid userId);

    public Task<ResultResponse> UpdateInfoAsync(UserModel user);

    public Task<bool> DeleteByIdAsync(Guid id);

    public Task<bool> LogoutAsync(Guid userId);

    public Task AddToRole(Guid userId, string? role);

    public Task<ResultResponse> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
}