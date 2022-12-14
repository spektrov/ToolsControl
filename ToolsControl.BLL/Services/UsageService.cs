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
        Validate(model);
        var entity = _mapper.Map<Usage>(model);
        _unitOfWork.UsageRepository.Create(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<UsageModel>(entity);
    }

    public async Task<UsageModel> UpdateAsync(UsageModel model)
    {
        Validate(model);
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
    
    
    private static void Validate(UsageModel model)
    {
        if (model.Finish < model.Start)
        {
            throw new ToolsControlException("Something went wrong.");
        }
    }
}