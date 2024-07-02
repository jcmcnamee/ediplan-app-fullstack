using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediplan.Application.Contracts;
public interface IAssetDetailVm
{
    public int Id { get; set; }
    public abstract string Type { get; set; }
    public string Name { get; set; } 
    public decimal? Rate { get; set; }
    public decimal? RateUnit { get; set; }
    public decimal? Value { get; set; }
}
