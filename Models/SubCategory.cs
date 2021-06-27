using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApplication.Models
{
    [Table("SubCategory")]
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }

        public int CategoryId { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}