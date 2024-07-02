using AutoMapper;
using Ediplan.Application.Contracts;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Factories;
using Ediplan.Application.Features.Assets.Commands.CreateEquipment;
using Ediplan.Application.Features.Assets.Commands.CreatePerson;
using Ediplan.Application.Features.Assets.Commands.CreateRoom;
using Ediplan.Application.Responses;
using Ediplan.Domain.Entities;
using MediatR;

namespace Ediplan.Application.Features.Assets.Commands.CreateAsset;
public class CreateAssetCommandHandler : IRequestHandler<ICreateAssetCommand, CreateAssetCommandResponse>
{
    private IMapper _mapper;
    private Dictionary<Type, ICreateAssetCommandResponse> _responseFactory;

    public CreateAssetCommandHandler(IMapper mapper, IEquipmentRepository equipmentRepository, IPersonRepository personRepository, IRoomRepository roomRepository)
    { 
        _mapper = mapper;;
        _responseFactory = new Dictionary<Type, ICreateAssetCommandResponse>
        {
            { typeof(CreateEquipmentCommand), new CreateEquipmentCommandResponse(equipmentRepository) },
            { typeof(CreatePersonCommand), new CreatePersonCommandResponse(personRepository) },
            { typeof(CreateRoomCommand), new CreateRoomCommandResponse(roomRepository) }
        };
    }

    public async Task<CreateAssetCommandResponse> Handle(ICreateAssetCommand request, CancellationToken cancellationToken)
    {
        if (_responseFactory.TryGetValue(request.GetType(), out var factory))
        {
            return await factory.CreateResponse(request, _mapper, cancellationToken);
        }
        else
        {
            return new CreateAssetCommandResponse
            {
                Success = false,
                Message = $"{request} not a valid command type."
            };
        }
    }
}



//public class CreateAssetCommandHandler : IRequestHandler<ICreateAssetCommand, CreateAssetCommandResponse>
//{
//    private readonly IMapper _mapper;
//    private readonly IEquipmentRepository _equipmentRepository;
//    private readonly IPersonRepository _personRepository;

//    public CreateAssetCommandHandler(IMapper mapper, IEquipmentRepository equipmentRepository, IPersonRepository personRepository)
//    {
//        _mapper = mapper;
//        _equipmentRepository = equipmentRepository;
//        _personRepository = personRepository;
//    }

//    // MediatR Polymorphic Dispatch
//    public async Task<CreateAssetCommandResponse> Handle(ICreateAssetCommand request, CancellationToken cancellationToken)
//    {
//        var response = new CreateAssetCommandResponse();

//        // Resolve command request type and process correct validator and entities.
//        switch (request)
//        {
//            case CreateEquipmentCommand equipmentCommand:
//                // Create response
//                {
//                    response.Asset = new CreateEquipmentDto();

//                    var validator = new CreateEquipmentValidator(_equipmentRepository);
//                    var validationResult = await validator.ValidateAsync(equipmentCommand);

//                    // Insert validation logic

//                    if (response.Success)
//                    {
//                        var equipment = _mapper.Map<Equipment>(equipmentCommand);
//                        equipment = await _equipmentRepository.AddAsync(equipment);
//                        response.Asset = _mapper.Map<CreateEquipmentDto>(equipment);
//                    }
//                }

//                break;
//            case CreatePersonCommand personCommand:
//                {
//                    response.Asset = new CreatePersonDto();

//                    var validator = new CreatePersonValidator(_personRepository);
//                    var validationResult = await validator.ValidateAsync(personCommand);

//                    // Insert validation logic

//                    if (response.Success)
//                    {
//                        var person = _mapper.Map<Person>(personCommand);
//                        person = await _personRepository.AddAsync(person);
//                        response.Asset = _mapper.Map<CreatePersonDto>(person);
//                    }
//                }
//                break;
//            default:
//                response.Success = false;
//                response.Message = $"{request} not a valid command type.";
//                break;

//        }

//        return response;


//    }
//}

