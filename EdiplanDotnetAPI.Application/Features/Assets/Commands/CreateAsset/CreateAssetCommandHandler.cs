using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;


namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
public class CreateAssetCommandHandler : IRequestHandler<ICreateAssetCommand, CreateAssetCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IEquipmentRepository _equipmentRepository;

    public CreateAssetCommandHandler(IMapper mapper, IEquipmentRepository equipmentRepository)
    {
        _mapper = mapper;
        _equipmentRepository = equipmentRepository;
    }

    // MediatR Polymorphic Dispatch
    public async Task<CreateAssetCommandResponse> Handle(ICreateAssetCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateAssetCommandResponse();

        // Resolve command request type and process correct validator and entities.
        switch (request)
        {
            case CreateEquipmentCommand equipmentCommand:
                // Create response
                response.Asset = new CreateEquipmentDto();

                var validator = new CreateEquipmentValidator(_equipmentRepository);
                var validationResult = await validator.ValidateAsync(equipmentCommand);

                // Insert validation logic

                if (response.Success)
                {
                    var equipment = _mapper.Map<Equipment>(equipmentCommand);
                    equipment = await _equipmentRepository.AddAsync(equipment);
                    response.Asset = _mapper.Map<CreateEquipmentDto>(equipment);
                }

                break;
            default:
                response.Success = false;
                response.Message = $"{request} not a valid command type.";
                break;

        }

        return response; 


    }
}
