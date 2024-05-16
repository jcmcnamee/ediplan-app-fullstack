using AutoMapper;
using EdiplanDotnetAPI.Application.Contracts.Persistence;
using EdiplanDotnetAPI.Application.Exceptions;
using EdiplanDotnetAPI.Domain.Common;
using EdiplanDotnetAPI.Domain.Entities;
using MediatR;


namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.DeleteAsset;
public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Asset> _assetRepository;

    public DeleteAssetCommandHandler(IMapper mapper, IAsyncRepository<Asset> assetRepository)
    {
        _mapper = mapper;
        _assetRepository = assetRepository;
    }
    public async Task Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = await _assetRepository.GetByIdAsync(request.Id);

        if(asset == null)
        {
            throw new NotFoundException(nameof(asset), request.Id);
        }

        await _assetRepository.DeleteAsync(asset);
    }
}
