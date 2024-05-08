using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
public class GetAssetsListQueryHandler : IRequestHandler<GetAssetsListQuery, List<AssetListVm>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Asset> _assetRepository;

    public GetAssetsListQueryHandler(IMapper mapper, IAsyncRepository<Asset> assetRepository)
    {
        _mapper = mapper;
        _assetRepository = assetRepository;
    }

    public async Task<List<AssetListVm>> Handle(GetAssetsListQuery request, CancellationToken cancellationToken)
    {
        var allAssets = (await _assetRepository.ListAllAsync()).OrderBy(x => x.CreatedDate);

        return _mapper.Map<List<AssetListVm>>(allAssets);
    }
}
