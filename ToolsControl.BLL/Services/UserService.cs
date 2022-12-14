using AutoMapper;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Extensions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.BLL.Models.Responses;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class UserService : IUserService
{
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenService = tokenService;
    }


    public async Task<PagedList<UserModel>> GetUsersAsync(UserParameters parameters)
    {
        var entities =  _unitOfWork.UserRepository
            .FindAll(false)
            .Filter()
            .Search(parameters)
            .Sort(parameters.OrderBy)
            .ToList();

        var models = await GetMappedUsersWithRoles(entities);

        return PagedList<UserModel>
            .ToPagedList(models, parameters.PageNumber, parameters.PageSize);
    }
    

    public async Task<UserModel> GetByIdAsync(Guid userId)
    {
        var entity = await _unitOfWork.UserRepository.FindByIdAsync(userId);
        if (entity == null) throw new ToolsControlException("User not found");

        var model = _mapper.Map<UserModel>(entity);
        model.Role = (await _unitOfWork.UserRepository.GetRolesAsync(entity)).FirstOrDefault();
        
        return model;
    }


    public async Task<ResultResponse> SignupAsync(UserModel signupRequest, string password)
    {
        var user = _mapper.Map<User>(signupRequest);
        user.UserName = Guid.NewGuid().ToString();

        var resultCreate = await _unitOfWork.UserRepository.CreateAsync(user, password);
        if (!resultCreate.Succeeded) 
        { 
            var errors = resultCreate.Errors.Select(e => e.Description);
            return new ResultResponse { Success = false, Errors = errors };
        }

        signupRequest.Role ??= "Worker";
        
        if (!await _unitOfWork.UserRepository.AddToRoleAsync(user, signupRequest.Role))
        {
            return new ResultResponse { Success = false };
        }

        return new ResultResponse { Success = true };
    }
    
    
    public async Task<SigninResponse> SigninAsync(string email, string password)
    {
        var user = await _unitOfWork.UserRepository.FindByEmailAsync(email);

        if (user == null || !await _unitOfWork.UserRepository.CheckPasswordAsync(user, password))
        {
            throw new ToolsControlException("Email or password not valid");
        }

        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

        var accessToken = await _tokenService.GenerateAccessTokenAsync(user);

        return new SigninResponse { AccessToken = accessToken, RefreshToken = refreshToken };
    }
    

    public async Task<ResultResponse> UpdateInfoAsync(UserModel user)
    {
        var entity = await _unitOfWork.UserRepository.FindByIdAsync(user.Id);
        if (entity == null) return new ResultResponse {Success = false, Errors = new []{"Not found."}};
        
        entity.Email = user.Email;
        entity.FirstName = user.FirstName;
        entity.LastName = user.LastName;
        entity.BirthDate = user.BirthDate;

        var result = await _unitOfWork.UserRepository.UpdateAsync(entity);
        
        return new ResultResponse {Success = result.Succeeded, 
            Errors = result.Errors.Select(x => x.Description)};
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.UserRepository.FindByIdAsync(id);

        return await _unitOfWork.UserRepository.DeleteAsync(entity!);
    }


    public async Task<bool> LogoutAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);
        if (user == null) return false;

        var result = await _tokenService.RemoveRefreshTokenAsync(user);

        return result;
    }


    public async Task AddToRole(Guid userId, string? role)
    {
        var entity  = await _unitOfWork.UserRepository.FindByIdAsync(userId);

        if (entity != null && role != null)
        {
            await _unitOfWork.UserRepository.RemoveFromAllRoles(entity);
            await _unitOfWork.UserRepository.AddToRoleAsync(entity, role);
        }
    }

    public async Task<ResultResponse> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var entity  = await _unitOfWork.UserRepository.FindByIdAsync(userId);

        if (entity == null) return new ResultResponse { Success = false, Errors = new[] { "Not found" } };

        var response = await _unitOfWork.UserRepository
            .ChangePasswordAsync(entity, currentPassword, newPassword);

        return new ResultResponse
        {
            Success = response.Succeeded, Errors = response.Errors.Select(x => x.Description)
        };
    }


    private async Task<ICollection<UserModel>> GetMappedUsersWithRoles(IEnumerable<User> users)
    {
        var result = new List<UserModel>();
        foreach (var user in users)
        {
            var role = (await _unitOfWork.UserRepository.GetRolesAsync(user)).FirstOrDefault();
            var model = _mapper.Map<UserModel>(user);
            model.Role = role;
            result.Add(model);
        }

        return result;
    }
}