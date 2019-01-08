using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Interfaces
{
    public interface IGateEntriesService
    {
        bool PostGateEntry(GateEntryModel model, out string NewGateEntryNumber);
        GateEntriesRestModel GetGateEntry(string gateEntryNumber);
    }
}
