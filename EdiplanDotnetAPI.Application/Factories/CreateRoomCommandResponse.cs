using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreatePerson;
using EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateRoom;
using EdiplanDotnetAPI.Application.Responses;
using EdiplanDotnetAPI.Domain.Entities;

namespace EdiplanDotnetAPI.Application.Factories;
public class CreateRoomCommandResponse : ICreateAssetCommandResponse

{
    private readonly IRoomRepository _roomRepository;

    public CreateRoomCommandResponse(IRoomRepository roomRepository
        )
    {
        _roomRepository = roomRepository;
    }

    public async Task<CreateAssetCommandResponse> CreateResponse(ICreateAssetCommand command, IMapper mapper, CancellationToken cancellationToken)
    {
        var response = new CreateAssetCommandResponse
        {
            Asset = new CreateRoomDto()
        };

        var validator = new CreateRoomValidator(_roomRepository);
        var validationResult = await validator.ValidateAsync((CreateRoomCommand)command);

        // Insert validation logic

        if (response.Success)
        {
            var room = mapper.Map<Room>(command);
            room = await _roomRepository.AddAsync(room);
            response.Asset = mapper.Map<CreateRoomDto>(room);
        }

        return response;
    }
}
