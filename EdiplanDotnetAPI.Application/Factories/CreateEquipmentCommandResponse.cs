using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
using EdiplanDotnetAPI.Application.Responses;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Application.Factories;
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
