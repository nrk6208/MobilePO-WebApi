using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.RestModels
{
    public class UserTokenResponse
    {
        public string Token { get; set; }
        public UserBasicInfoRestModel User { get; set; }
    }
}
