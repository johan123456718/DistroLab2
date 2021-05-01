using System;
using System.ComponentModel.DataAnnotations;

namespace DistroLab2.Models
{

    /// <summary>
    /// A message object for creating a message
    /// </summary>
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String SenderId { get; set; }

        [Required]
        public String RecipantId { get; set; }

        [Required]
        [MaxLength(50)]
        public String Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public String Content { get; set; }

        [Required]
        public DateTime TimeSent { get; set; }

        [Required]
        public Boolean IsRead { get; set; }

        [Required]
        public Boolean IsDeleted { get; set; }

    }
}
