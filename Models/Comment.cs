using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApplication.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string UserId { get; set; }

        public int PostId { get; set; }

        [StringLength(250)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } 

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        public virtual ICollection<SubComment> SubComment { get; set; }
    }
}