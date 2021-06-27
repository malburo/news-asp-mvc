using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApplication.Models
{
    [Table("SubComment")]
    public class SubComment
    {
        [Key]
        public int SubCommentId { get; set; }

        public string UserId { get; set; }

        public int CommentId { get; set; }

        [StringLength(250)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}