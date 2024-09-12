using AutoMapper;
using Ediplan.Application.Contracts;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Features.Assets.Commands.CreateEquipment;
using Ediplan.Application.Responses;
using Ediplan.Domain.Entities;

namespace Ediplan.Application.Factories;
public class CreateEquipmentCommandResponse : ICreateAssetCommandResponse

{
    private readonly IEquipmentRepository _equipmentRepository;

    public CreateEquipmentCommandResponse(IEquipmentRepository equipmentRepository
        )
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<CreateAssetCommandResponse> CreateResponse(ICreateAssetCommand command, IMapper mapper, CancellationToken cancellationToken)
    {
        var response = new CreateAssetCommandResponse
        {
            Asset = new CreateEquipmentDto()
        };

        var validator = new CreateEquipmentValidator(_equipmentRepository);
        var validationResult = await validator.ValidateAsync((CreateEquipmentCommand)command);

        // Insert validation logic
        if (response.Success)
        {
            var equipment = mapper.Map<Equipment>(command);
            equipment = await _equipmentRepository.AddAsync(equipment);
            response.Asset = mapper.Map<CreateEquipmentDto>(equipment);
        }

        return response;
    }
}
