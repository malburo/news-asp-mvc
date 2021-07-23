using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace NewsApplication.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public string AuthorId { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [AllowHtml]
        [StringLength(10000)]
        public string Content { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        public int? Views { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Reaction> Reactions { get; set; }

        public virtual ICollection<SubCategory> SubCategory { get; set; }

        public bool isBookmark = false;
    }
}