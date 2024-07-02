using AutoMapper;
using Ediplan.Application.Contracts.Persistence;
using Ediplan.Application.Exceptions;
using Ediplan.Domain.Common;
using Ediplan.Domain.Entities;
using MediatR;


namespace Ediplan.Application.Features.Assets.Commands.DeleteAsset;
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
