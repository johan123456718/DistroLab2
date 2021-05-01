using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistroLab2.Models.ViewModels
{

    /// <summary>
    /// Represents the content of a message
    /// </summary>
    public class MessageViewModel
    {
        public int Id;
        public String Sender { get; set; }

        [Required]
        public String Recipant { get; set; }

        [Required]
        [MaxLength(50)]
        public String Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public String Content { get; set; }


        public DateTime TimeSent { get; set; }

   
        public Boolean IsRead { get; set; }


        public Boolean IsDeleted { get; set; }

    }
}
