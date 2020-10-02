using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities
{
    public class PromBill
    {
        [Key]
        public int ID { get; set; }
        public double Discount { get; set; }
        public string ItemFree { get; set; }
        public byte ConditionItem { get; set; }
        public int ConditionAmount { get; set; }
        [ForeignKey("Promotion")]
        public int PromotionID { get; set; }
        //Nav property
        public Promotion Promotion { get; set; }
    }
}