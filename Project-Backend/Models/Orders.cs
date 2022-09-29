using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_Backend.Models
{
    public class Orders
    {
        [Key]
        public Guid OrderId { get; set; }

        public string Furniture { get; set; }

        public string Equipment { get; set; }

        public string Address { get; set; }

        [ForeignKey("Registration")]
        public Guid Registration_ID { get; set; }

        public virtual Registration Registration { get; set; }
    }
}