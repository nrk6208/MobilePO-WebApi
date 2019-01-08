using NB.MOBILEPO.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Interfaces
{
    public interface IReceiptService
    {
        void PostReceipt(ReceiptModel model);
    }
}
