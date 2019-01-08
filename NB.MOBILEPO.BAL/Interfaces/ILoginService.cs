using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.Interfaces
{
    public interface ILoginService
    {
        UserTokenResponse Authenticate(LoginModel model, long userId);
    }
}
