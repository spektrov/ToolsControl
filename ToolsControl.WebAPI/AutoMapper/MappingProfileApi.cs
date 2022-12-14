using AutoMapper;
using ToolsControl.BLL.Models;
using ToolsControl.WebAPI.Models;

namespace ToolsControl.WebAPI.AutoMapper;

public class MappingProfileApi : Profile
{
    public MappingProfileApi()
    {
        CreateMap<UserSignupRequest, UserModel>();
    }
}