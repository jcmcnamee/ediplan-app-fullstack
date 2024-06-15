using EdiplanDotnetAPI.Application.Contracts.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreatePerson;
internal class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonValidator(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    // Validation logic
}
