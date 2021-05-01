using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DistroLab2.Areas.Identity.Models
{
    /// <summary>
    /// Class that  represents the users stored in the database
    /// </summary>
    public class LoginUser : IdentityUser
    {
        public virtual List<LoginDetails> LoginDetails { get; set; }
    }
}
