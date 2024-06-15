using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreatePerson;
using EdiplanDotnetAPI.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateRoom;
internal class CreateRoomValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly IAsyncRepository<Person> _personRepository;

    public CreateRoomValidator(IAsyncRepository<Person> personRepository)
    {
        _personRepository = personRepository;
    }

    // Custom validation logic
}
