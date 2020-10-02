using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities
{
    public class Post
    {
        [Key]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public string PostContent { get; set; }
        //Nav property
        public Product Products { get; set; }
    }
}