using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Exceptions;
using EdiplanDotnetAPI.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetDetail;
public class GetAssetDetailQueryHandler : IRequestHandler<GetAssetDetailQuery, IAssetDetailVm>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Asset> _assetRepo;
    private readonly IDictionary<string, Type> _assetTypeMap;

    public GetAssetDetailQueryHandler(IMapper mapper, IAsyncRepository<Asset> assetRepo)
    {
        _mapper = mapper;
        _assetRepo = assetRepo;
        _assetTypeMap = new Dictionary<string, Type>
        {
            {"Equipment", typeof(EquipmentDetailVm)},
            {"Person", typeof(PersonDetailVm)},
            {"Room", typeof(RoomDetailVm) }
        };
    
    }

    public async Task<IAssetDetailVm> Handle(GetAssetDetailQuery request, CancellationToken cancellationToken)
    {
        var asset = await _assetRepo.GetByIdAsync(request.Id);
        if (asset == null)
        {
            throw new NotFoundException(nameof(asset), request.Id);
        }

        // Try and get type, output view model type on success
        if (!_assetTypeMap.TryGetValue(asset.Type, out var vmType))
        {
            // Needs more suitable exception type...
            throw new NotFoundException($"Unknown asset type: {asset.Type}", request.Id);
        }

        var assetDetailDto = (IAssetDetailVm)_mapper.Map(asset, typeof(Asset), vmType);

        return assetDetailDto;
    }
}
