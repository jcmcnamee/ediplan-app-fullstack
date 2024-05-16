using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Responses;
using EdiplanDotnetAPI.Domain.Common;
using MediatR;


namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
public class CreateAssetCommandHandler : IRequestHandler<ICreateAssetCommand, BaseResponse>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Asset> _assetRepository;

    public CreateAssetCommandHandler(IMapper mapper, IAsyncRepository<Asset> assetRepository)
    {
        _mapper = mapper;
        _assetRepository = assetRepository;
    }

    public Task<BaseResponse> Handle(ICreateAssetCommand command, CancellationToken cancellationToken)
    {
        switch (command)
        {
            case CreateEquipmentCommand equipmentCommand:
                break;

        }
    }
}
