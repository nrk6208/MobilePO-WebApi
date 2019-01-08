using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Interfaces
{
    public interface ICommonService
    {
        long CurrentUserId { get; }
        long CurrentSupplierId { get; }
        bool IsSameSupplierUser(long? supplierId);
    }
}
