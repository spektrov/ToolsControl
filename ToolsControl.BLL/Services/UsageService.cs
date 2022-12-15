using AutoMapper;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class UsageService : IUsageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public UsageService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UsageModel> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.UsageRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new ToolsControlException($"Model with id {id} not found in database");
        }

        return _mapper.Map<UsageModel>(entity);
    }

    public async Task<UsageModel> CreateAsync(UsageModel model)
    {
        await Validate(model);
        var entity = _mapper.Map<Usage>(model);
        _unitOfWork.UsageRepository.Create(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<UsageModel>(entity);
    }

    public async Task<UsageModel> UpdateAsync(UsageModel model)
    {
        await Validate(model);
        var entity = _mapper.Map<Usage>(model);
        _unitOfWork.UsageRepository.Update(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<UsageModel>(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.UsageRepository.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.UsageRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task<PagedList<UsageModel>> GetUsages(UsageParameters parameters)
    {
        var entities = _unitOfWork.UsageRepository
            .FindAll(false);

        var models =  _mapper.Map<ICollection<UsageModel>>(entities);
        
        return PagedList<UsageModel>.ToPagedList(models, parameters.PageNumber, parameters.PageSize);
    }
    
    
    private async Task Validate(UsageModel model)
    {
        var access = _unitOfWork.AccessRepository.FindByCondition(x =>
            x.EquipmentId == model.EquipmentId && x.WorkerId == model.WorkerId, false)
            .Any();
        if (!access)
        {
            throw new ToolsControlException("Worker has no access to this equipment.");
        }
        
        
        var equipment = await _unitOfWork.EquipmentRepository.GetByIdAsync(model.EquipmentId);
        if (equipment != null && (!equipment.IsAbleToWork || !equipment.IsAvailable))
        {
            throw new ToolsControlException("Equipment isn't available");
        }
        
        if (model.Finish != null && model.Finish < model.Start)
        {
            throw new ToolsControlException("Something went wrong.");
        }
    }
}