using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreatePerson;
using EdiplanDotnetAPI.Application.Responses;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Application.Factories;
public class CreatePersonCommandResponse : ICreateAssetCommandResponse

{
    private readonly IPersonRepository _personRepository;

    public CreatePersonCommandResponse(IPersonRepository personRepository
        )
    {
        _personRepository = personRepository;
    }

    public async Task<CreateAssetCommandResponse> CreateResponse(ICreateAssetCommand command, IMapper mapper, CancellationToken cancellationToken)
    {
        var response = new CreateAssetCommandResponse
        {
            Asset = new CreatePersonDto()
        };

        var validator = new CreatePersonValidator(_personRepository);
        var validationResult = await validator.ValidateAsync((CreatePersonCommand)command);

        // Insert validation logic

        if (response.Success)
        {
            var person = mapper.Map<Person>(command);
            person = await _personRepository.AddAsync(person);
            response.Asset = mapper.Map<CreatePersonDto>(person);
        }

        return response;
    }
}
