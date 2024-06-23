using EdiplanDotnetAPI.Application.Contracts;

namespace EdiplanDotnetAPI.Application.Responses;
public class CreateAssetCommandResponse : BaseResponse
{
    public CreateAssetCommandResponse() : base()
    {

    }

    public ICreateAssetDto Asset { get; set; } = default!;
}
