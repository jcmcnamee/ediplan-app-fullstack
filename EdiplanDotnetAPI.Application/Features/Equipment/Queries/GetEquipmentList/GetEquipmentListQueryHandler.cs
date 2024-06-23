using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Equipment.Queries.GetEquipmentList;
public class GetEquipmentListQueryHandler : IRequestHandler<GetEquipmentListQuery, List<EquipmentListVm>>
{
    private readonly IMapper _mapper;
    private readonly IEquipmentRepository _equipmentRepository;

    public GetEquipmentListQueryHandler(IMapper mapper, IEquipmentRepository equipmentRepository)
    {
        _mapper = mapper;
        _equipmentRepository = equipmentRepository;
    }
    public async Task<List<EquipmentListVm>> Handle(GetEquipmentListQuery request, CancellationToken cancellationToken)
    {
        var allEquipment = (await _equipmentRepository.ListAllAsync());
        return _mapper.Map<List<EquipmentListVm>>(allEquipment);
    }
}
