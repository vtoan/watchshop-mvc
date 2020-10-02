using System.ComponentModel.DataAnnotations;

namespace aspcore_watchshop.Entities
{
    public class Fee
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }
        public double Cost { get; set; }
    }
}