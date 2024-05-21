using EdiplanDotnetAPI.Application.Contracts;
using EdiplanDotnetAPI.Application.Responses;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
public class CreateAssetCommandResponse : BaseResponse
{
    public CreateAssetCommandResponse() : base()
    {
        
    }

    public ICreateAssetDto Asset { get; set; } = default!;
}
