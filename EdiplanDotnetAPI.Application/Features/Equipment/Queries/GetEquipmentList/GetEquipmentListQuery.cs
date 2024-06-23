using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Features.Equipment.Queries.GetEquipmentList;
public class GetEquipmentListQuery : IRequest<List<EquipmentListVm>>
{

}
