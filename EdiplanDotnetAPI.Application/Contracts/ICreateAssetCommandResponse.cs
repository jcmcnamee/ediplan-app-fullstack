using EdiplanDotnetAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Contracts;
public interface ICreateAssetCommandResponse
{
    bool Success { get; set; }
    string Message { get; set; }
    ICreateAssetDto AssetDto { get; }
}
