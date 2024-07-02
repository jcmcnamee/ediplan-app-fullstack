using Ediplan.Application.Contracts;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Domain.Common;
using Ediplan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Assets.Commands.CreateEquipment;
public class CreateEquipmentValidator : AbstractValidator<CreateEquipmentCommand>
{
    private readonly IEquipmentRepository _equipmentRepository;

    public CreateEquipmentValidator(IEquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }
}
