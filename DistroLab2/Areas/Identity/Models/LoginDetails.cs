using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistroLab2.Areas.Identity.Models
{
    /// <summary>
    /// Class that represents logindetails stored in the database
    /// </summary>
    public class LoginDetails 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime LoginDate { get; set; }

        public virtual LoginUser User {get; set;}
        [ForeignKey("User")]
        public string UserId { get; set; } 
    }
}
