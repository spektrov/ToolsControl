using AutoMapper;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class JobTitleService : IJobTitleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public JobTitleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<JobTitleModel> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.JobTitleRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new ToolsControlException($"Model with id {id} not found in database");
        }

        return _mapper.Map<JobTitleModel>(entity);
    }

    public async Task<JobTitleModel> CreateAsync(JobTitleModel model)
    {
        var entity = _mapper.Map<JobTitle>(model);
        _unitOfWork.JobTitleRepository.Create(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<JobTitleModel>(entity);
    }

    public async Task<JobTitleModel> UpdateAsync(JobTitleModel model)
    {
        var entity = _mapper.Map<JobTitle>(model);
        _unitOfWork.JobTitleRepository.Update(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<JobTitleModel>(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.JobTitleRepository.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.JobTitleRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task<PagedList<JobTitleModel>> GetJobTitles(JobTitleParameters parameters)
    {
        var entities = _unitOfWork.JobTitleRepository
            .FindAll(false);

        var models =  _mapper.Map<ICollection<JobTitleModel>>(entities);
        
        return PagedList<JobTitleModel>.ToPagedList(models, parameters.PageNumber, parameters.PageSize);
    }
}