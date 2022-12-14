using AutoMapper;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class EquipmentTypeService : IEquipmentTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EquipmentTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<EquipmentTypeModel> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.EquipmentTypeRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new ToolsControlException($"Model with id {id} not found in database");
        }

        return _mapper.Map<EquipmentTypeModel>(entity);
    }

    public async Task<EquipmentTypeModel> CreateAsync(EquipmentTypeModel model)
    {
        var entity = _mapper.Map<EquipmentType>(model);
        _unitOfWork.EquipmentTypeRepository.Create(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<EquipmentTypeModel>(entity);
    }

    public async Task<EquipmentTypeModel> UpdateAsync(EquipmentTypeModel model)
    {
        var entity = _mapper.Map<EquipmentType>(model);
        _unitOfWork.EquipmentTypeRepository.Update(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<EquipmentTypeModel>(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.EquipmentTypeRepository.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.EquipmentTypeRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task<PagedList<EquipmentTypeModel>> GetEquipmentTypes(EquipmentTypeParameters parameters)
    {
        var entities = _unitOfWork.EquipmentTypeRepository
            .FindAll(false);

        var models =  _mapper.Map<ICollection<EquipmentTypeModel>>(entities);
        
        return PagedList<EquipmentTypeModel>.ToPagedList(models, parameters.PageNumber, parameters.PageSize);
    }
}