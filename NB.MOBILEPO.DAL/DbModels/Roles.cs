using System;
using System.Collections.Generic;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public short RoleId { get; set; }
        public string RoleName { get; set; }
        public short RoleRank { get; set; }
        public string RoleDescription { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
