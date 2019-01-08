using System;
using System.Collections.Generic;
using System.Text;

namespace NB.MOBILEPO.BAL.RestModels
{
    public class UserBasicInfoRestModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long SupplierId { get; set; }
        public int RoleId { get; set; }
        public int RoleRank { get; set; }
    }
}
