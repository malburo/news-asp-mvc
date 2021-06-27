using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApplication.Models
{
    [Table("Reaction")]
    public class Reaction
    {
        [Key]
        public int ReactionId { get; set; }

        public string UserId { get; set; }

        public int PostId { get; set; }

        public bool? Like { get; set; }

        public bool? Dislike { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}