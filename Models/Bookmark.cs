using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApplication.Models
{
    [Table("Bookmark")]
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }

        public string UserId { get; set; }

        public int PostId { get; set; }

        public DateTime CreateAt { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

    }
}