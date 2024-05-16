using EdiplanDotnetAPI.Application.Contracts.Persistence;
using FluentValidation;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
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
