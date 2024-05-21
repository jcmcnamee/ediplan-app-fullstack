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

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
public class CreateEquipmentValidator : AbstractValidator<CreateEquipmentCommand>
{
    private readonly IAsyncRepository<Equipment> _equipmentRepository;

    public CreateEquipmentValidator(IAsyncRepository<Equipment> equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }
}
