using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NB.MOBILEPO.BAL.Helpers
{
    /// <summary>
    /// Defines available Genders
    /// </summary>
    public enum Gender
    {
        Male = 1,
        Female,
        Other
    }
    
    /// <summary>
    /// Defines available Email subjects
    /// </summary>
    public enum EmailSubjects
    {
        NewUser=1,
        ChangePassword
    }
}
