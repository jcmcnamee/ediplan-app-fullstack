using MediatR;


namespace Ediplan.Application.Features.Assets.Queries.GetAssetDetail;
public class GetAssetDetailQuery : IRequest<AssetDetailVm>
{
    public int Id { get; set; }
}
