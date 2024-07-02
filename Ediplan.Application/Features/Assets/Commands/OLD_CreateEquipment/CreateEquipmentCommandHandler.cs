using AutoMapper;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
using Ediplan.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ediplan.Application.Features.Assets.Commands.CreateEquipment;
public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, CreateEquipmentCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly ILogger<CreateEquipmentCommandHandler> _logger;

    public CreateEquipmentCommandHandler(IMapper mapper, IEquipmentRepository equipmentRepository, ILogger<CreateEquipmentCommandHandler> logger)
    {
        _mapper = mapper;
        _equipmentRepository = equipmentRepository;
        _logger = logger;
    }

    async Task<CreateEquipmentCommandResponse> IRequestHandler<CreateEquipmentCommand, CreateEquipmentCommandResponse>.Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        // Create custom response
        var createEquipmentCommandResponse = new CreateEquipmentCommandResponse();

        // Create validator and inject repo in case of custom rules
        var validator = new CreateEquipmentCommandValidator(_equipmentRepository);
        var validationResult = await validator.ValidateAsync(request);

        // If validation fails
        if(validationResult.Errors.Count > 0)
        {
            createEquipmentCommandResponse.Success = false;
            createEquipmentCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                createEquipmentCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }

            throw new ValidationException(validationResult);
        }

        // If validation is successful
        if(createEquipmentCommandResponse.Success)
        {
            // Map and add entity
            var equipment = _mapper.Map<Equipment>(request);
            equipment = await _equipmentRepository.AddAsync(equipment);

            // Response DTO
            createEquipmentCommandResponse.Equipment = _mapper.Map<CreateEquipmentDto>(equipment);
        }
    }
}
