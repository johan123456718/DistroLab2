using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistroLab2.Models.ViewModels
{
    /// <summary>
    /// A model for showing amount of logins, get the last login time and the username. 
    /// </summary>
    public class IndexViewModel
    {
        public String UserName { get; set; }

        public int NrOfLogins { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
