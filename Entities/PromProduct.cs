using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities
{
    public class PromProduct
    {
        [Key]
        public int ID { get; set; }
        public double Discount { get; set; }
        [StringLength(250)]
        public string ProductIDs { get; set; }
        public int? CategoryID { get; set; }
        public int? BandID { get; set; }
        [ForeignKey("Promotion")]
        public int PromotionID { get; set; }
        //Nav property
        public Promotion Promotion { get; set; }
    }
}