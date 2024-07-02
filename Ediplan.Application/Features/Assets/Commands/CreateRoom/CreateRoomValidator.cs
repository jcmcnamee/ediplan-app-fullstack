using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Features.Assets.Commands.CreatePerson;
using Ediplan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Features.Assets.Commands.CreateRoom;
internal class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
{
    private readonly IAsyncRepository<Room> _roomRepository;

    public CreateRoomValidator(IAsyncRepository<Room> roomRepository)
    {
        _roomRepository = roomRepository;
    }

    // Custom validation logic
}
