using AutoMapper;
using ToolsControl.BLL.Models;
using ToolsControl.WebAPI.Models;

namespace ToolsControl.WebAPI.AutoMapper;

public class MappingProfileApi : Profile
{
    public MappingProfileApi()
    {
        CreateMap<UserSignupRequest, UserModel>();

        CreateMap<AccessCreateRequest, AllowedAccessModel>();

        CreateMap<EquipmentCreateRequest, EquipmentModel>();
        CreateMap<EquipmentUpdateRequest, EquipmentModel>();

        CreateMap<EquipmentTypeCreateRequest, EquipmentTypeModel>();
        CreateMap<EquipmentTypeUpdateRequest, EquipmentTypeModel>();

        CreateMap<JobTitleCreateRequest, JobTitleModel>();
        CreateMap<JobTitleUpdateRequest, JobTitleModel>();

        CreateMap<UsageCreateRequest, UsageModel>();
        CreateMap<UsageUpdateRequest, UsageModel>();

        CreateMap<WorkerCreateRequest, WorkerModel>();
        CreateMap<WorkerUpdateRequest, WorkerModel>();
    }
}