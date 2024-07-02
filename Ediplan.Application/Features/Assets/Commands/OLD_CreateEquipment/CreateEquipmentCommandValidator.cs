using Ediplan.Application.Contracts.Persistence;
using FluentValidation;

namespace Ediplan.Application.Features.Assets.Commands.CreateEquipment;
public class CreateEquipmentCommandValidator : AbstractValidator<CreateEquipmentCommand>
{
    private readonly IEquipmentRepository _equipmentRepository;

    public CreateEquipmentCommandValidator(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;

        // FluentValidation Rules here
    }

    // Custom rules here that require DB access
    
}
