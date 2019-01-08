using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Interfaces
{
    public interface IUserService
    {
        bool IsExisted(long userId, out UserBasicInfoRestModel user);
        long? GetUserId(string userName);
    }
}
