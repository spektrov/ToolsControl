using AutoMapper;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class AccessService : IAccessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccessService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    

    public async Task<AllowedAccessModel> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.AccessRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new ToolsControlException($"Model with id {id} not found in database");
        }

        return _mapper.Map<AllowedAccessModel>(entity);
    }

    public async Task<AllowedAccessModel> CreateAsync(AllowedAccessModel model)
    {
        await Validate(model);
        var entity = _mapper.Map<AllowedAccess>(model);
        _unitOfWork.AccessRepository.Create(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<AllowedAccessModel>(entity);
    }

    public async Task<AllowedAccessModel> UpdateAsync(AllowedAccessModel model)
    {
        await Validate(model);
        var entity = _mapper.Map<AllowedAccess>(model);
        _unitOfWork.AccessRepository.Update(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<AllowedAccessModel>(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.AccessRepository.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.AccessRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task<PagedList<AllowedAccessModel>> GetAccesses(AllowedAccessParameters parameters)
    {
        var entities = _unitOfWork.AccessRepository
            .FindAll(false);

        var models =  _mapper.Map<ICollection<AllowedAccessModel>>(entities);
        
        return PagedList<AllowedAccessModel>.ToPagedList(models, parameters.PageNumber, parameters.PageSize);
    }


    private Task Validate(AllowedAccessModel model)
    {
        var same = _unitOfWork.AccessRepository.FindByCondition(x =>
                x.WorkerId == model.WorkerId && x.EquipmentId == model.EquipmentId, false)
            .Any();

        if (same)
        {
            throw new ToolsControlException("Access already exists.");
        }

        return Task.CompletedTask;
    }
}