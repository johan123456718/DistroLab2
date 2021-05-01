using System;
using System.Collections.Generic;

namespace DistroLab2.Models.ViewModels
{
    /// <summary>
    /// General information of the user's inbox 
    /// </summary>
    public class ReadViewModel
    {
        public List<String> UserList { get; set; }
        public int NrOfRead { get; set; }
        public int NrOfDeleted { get; set; }
        public int TotNrOfMessages { get; set; }

    }
}
