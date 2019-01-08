using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.RestModels
{
    public class UserFullInfoRestModel : UserBasicInfoRestModel
    {
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
