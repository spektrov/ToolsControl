using AutoMapper;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class WorkerService : IWorkerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WorkerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<WorkerModel> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.WorkerRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new ToolsControlException($"Model with id {id} not found in database");
        }

        return _mapper.Map<WorkerModel>(entity);
    }

    public async Task<WorkerModel> CreateAsync(WorkerModel model)
    {
        var entity = _mapper.Map<Worker>(model);
        _unitOfWork.WorkerRepository.Create(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<WorkerModel>(entity);
    }

    public async Task<WorkerModel> UpdateAsync(WorkerModel model)
    {
        var entity = _mapper.Map<Worker>(model);
        _unitOfWork.WorkerRepository.Update(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<WorkerModel>(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.WorkerRepository.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.WorkerRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task<PagedList<WorkerModel>> GetWorkers(WorkerParameters parameters)
    {
        var entities = _unitOfWork.WorkerRepository
            .FindAll(false);

        var models =  _mapper.Map<ICollection<WorkerModel>>(entities);
        
        return PagedList<WorkerModel>.ToPagedList(models, parameters.PageNumber, parameters.PageSize);
    }
}