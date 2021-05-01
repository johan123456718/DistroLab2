using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DistroLab2.Models.ViewModels
{
    /// <summary>
    /// A model for sending the message
    /// </summary>
    public class SendFormViewModel
    {
        public MessageViewModel MessageViewModel { get; set; }
        public List<SelectListItem> Users { get; set; }
    }
}
