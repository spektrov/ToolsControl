using AutoMapper;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EquipmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<EquipmentModel> GetByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.EquipmentRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new ToolsControlException($"Model with id {id} not found in database");
        }

        return _mapper.Map<EquipmentModel>(entity);
    }

    public async Task<EquipmentModel> CreateAsync(EquipmentModel model)
    {
        var entity = _mapper.Map<Equipment>(model);
        _unitOfWork.EquipmentRepository.Create(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<EquipmentModel>(entity);
    }

    public async Task<EquipmentModel> UpdateAsync(EquipmentModel model)
    {
        var entity = _mapper.Map<Equipment>(model);
        _unitOfWork.EquipmentRepository.Update(entity);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<EquipmentModel>(entity);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await _unitOfWork.EquipmentRepository.GetByIdAsync(id);
        if (entity != null)
        {
            _unitOfWork.EquipmentRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task<PagedList<EquipmentModel>> GetEquipments(EquipmentParameters parameters)
    {
        var entities = _unitOfWork.EquipmentRepository
            .FindAll(false);

        var models =  _mapper.Map<ICollection<EquipmentModel>>(entities);
        
        return PagedList<EquipmentModel>.ToPagedList(models, parameters.PageNumber, parameters.PageSize);
    }
}