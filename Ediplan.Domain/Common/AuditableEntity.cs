using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ediplan.Domain.Common;

public abstract class AuditableEntity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set;}
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
