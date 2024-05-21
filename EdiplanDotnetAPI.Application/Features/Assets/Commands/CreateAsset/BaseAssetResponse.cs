using EdiplanDotnetAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Assets.Commands.CreateAsset;
internal class BaseAssetResponse<T> : BaseResponse
{
    public BaseAssetResponse() : base()
    {
        
    }

    T Asset { get; set; } = default!;
}
