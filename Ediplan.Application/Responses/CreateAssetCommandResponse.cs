using Ediplan.Application.Contracts;

namespace Ediplan.Application.Responses;
public class CreateAssetCommandResponse : BaseResponse
{
    public CreateAssetCommandResponse() : base()
    {

    }

    public ICreateAssetDto Asset { get; set; } = default!;
}
