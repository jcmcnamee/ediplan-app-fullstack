using AutoMapper;
using Ediplan.Application.Contracts;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Features.Assets.Commands.CreatePerson;
using Ediplan.Application.Features.Assets.Commands.CreateRoom;
using Ediplan.Application.Responses;
using Ediplan.Domain.Entities;

namespace Ediplan.Application.Factories;
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
