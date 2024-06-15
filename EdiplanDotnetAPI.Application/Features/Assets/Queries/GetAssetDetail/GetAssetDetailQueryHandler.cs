using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Exceptions;
using EdiplanDotnetAPI.Domain.Common;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;
public class GetAssetDetailQueryHandler : IRequestHandler<GetAssetDetailQuery, AssetDetailVm>
{
    private readonly IMapper _mapper;
    private readonly IAssetRepository _assetRepo;

    public GetAssetDetailQueryHandler(IMapper mapper, IAssetRepository assetRepo)
    {
        _mapper = mapper;
        _assetRepo = assetRepo;
    }

    public async Task<AssetDetailVm> Handle(GetAssetDetailQuery request, CancellationToken cancellationToken)
    {
        var asset = await _assetRepo.GetByIdAsync(request.Id);

        if (asset == null)
        {
            throw new NotFoundException(nameof(Asset), request.Id);
        }

        AssetDetailVm detailVm = new AssetDetailVm();

        switch (asset.GetType().Name)
        {
            case "Equipment":
                detailVm = _mapper.Map<EquipmentDetailVm>(asset);
                break;
            case "Person":
                detailVm = _mapper.Map<PersonDetailVm>(asset);
                break;
            case "Room":
                detailVm = _mapper.Map<RoomDetailVm>(asset);
                break;
            default:
                // some kind of exception here
                break;
        }
        return detailVm;

    }

}