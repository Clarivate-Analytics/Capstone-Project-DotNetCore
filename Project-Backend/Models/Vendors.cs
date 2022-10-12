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

        [ForeignKey("Registration")]
        public Guid Emp_ID { get; set; }

        public string Furniture { get; set; }

        public string Equipment { get; set; }

        public string Address { get; set; }

        public string Date { get; set; }

        public string Ven_ID { get; set; }


    }
}
