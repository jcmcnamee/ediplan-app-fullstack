using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.BookingGroups.Commands.CreateBookingGroup;
public class CreateBookingGroupCommandHandler : IRequestHandler<CreateBookingGroupCommand, CreateBookingGroupCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<BookingGroup> _bookingGroupRepository;

    public CreateBookingGroupCommandHandler(IMapper mapper, IAsyncRepository<BookingGroup> bookingGroupRepository)
    {
        _mapper = mapper;
        _bookingGroupRepository = bookingGroupRepository;
    }

    public async Task<CreateBookingGroupCommandResponse> Handle(CreateBookingGroupCommand request, CancellationToken cancellationToken)
    {
        // Create custom response
        var createBookingGroupCommandResponse = new CreateBookingGroupCommandResponse();

        // Create validator
        var validator = new CreateBookingGroupValidator();
        var validationResult = await validator.ValidateAsync(request);

        // If error
        if(validationResult.Errors.Count > 0)
        {
            createBookingGroupCommandResponse.Success = false;
            createBookingGroupCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                createBookingGroupCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        // If Successful
        if(createBookingGroupCommandResponse.Success)
        {
            // Create new entity
            var bookingGroup = new BookingGroup()
            {
                Name = request.Name
            };
            // Add entity to DB
            bookingGroup = await _bookingGroupRepository.AddAsync(bookingGroup);
            // Map entity to response DTO
            createBookingGroupCommandResponse.BookingGroup = _mapper.Map<CreateBookingGroupDto>(bookingGroup);
        }

        // Return a response that contains data and possible validation errors
        return createBookingGroupCommandResponse;
    }
}
