using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Helpers;
using EdiplanDotnetAPI.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Queries.GetAssetsList;
public class GetAssetsListQueryHandler : IRequestHandler<GetAssetsListQuery, PagedList<AssetListVm>>
{
    private readonly IMapper _mapper;
    private readonly IAssetRepository _assetRepository;


    public GetAssetsListQueryHandler(IMapper mapper, IAssetRepository assetRepository)
    {
        _mapper = mapper;
        _assetRepository = assetRepository;
    }

    public async Task<PagedList<AssetListVm>> Handle(GetAssetsListQuery request, CancellationToken cancellationToken)
    {

        var allAssets = (await _assetRepository.ListAllAsync(request));
        return _mapper.Map<PagedList<AssetListVm>>(allAssets);
    }
}
