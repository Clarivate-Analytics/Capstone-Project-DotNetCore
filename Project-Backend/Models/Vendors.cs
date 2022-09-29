using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_Backend.Models
{
    public class Vendors
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("Orders")]
        public Guid Orders_ID { get; set; }

        public virtual Orders Orders { get; set; }

        public string Date { get; set; }
    }
}
