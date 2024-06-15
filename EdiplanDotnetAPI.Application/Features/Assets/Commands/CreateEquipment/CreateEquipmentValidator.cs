using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Common;
using EdiplanDotnetAPI.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateEquipment;
public class CreateEquipmentValidator : AbstractValidator<CreateEquipmentCommand>
{
    private readonly IEquipmentRepository _equipmentRepository;

    public CreateEquipmentValidator(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }
}
