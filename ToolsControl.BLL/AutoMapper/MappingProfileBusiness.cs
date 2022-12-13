using AutoMapper;
using ToolsControl.BLL.Models;
using ToolsControl.DAL.Entities;

namespace ToolsControl.BLL.AutoMapper;

public class MappingProfileBusiness : Profile
{
    public MappingProfileBusiness()
    {
        CreateMap<AllowedAccess, AllowedAccessModel>();
        CreateMap<AllowedAccessModel, AllowedAccess>();

        CreateMap<Equipment, EquipmentModel>();
        CreateMap<EquipmentModel, Equipment>();

        CreateMap<EquipmentType, EquipmentTypeModel>();
        CreateMap<EquipmentTypeModel, EquipmentType>();

        CreateMap<JobTitle, JobTitleModel>();
        CreateMap<JobTitleModel, JobTitle>();

        CreateMap<Usage, UsageModel>();
        CreateMap<UsageModel, Usage>();

        CreateMap<User, UserModel>();
        CreateMap<UserModel, User>();

        CreateMap<Worker, WorkerModel>();
        CreateMap<WorkerModel, Worker>();
    }
}