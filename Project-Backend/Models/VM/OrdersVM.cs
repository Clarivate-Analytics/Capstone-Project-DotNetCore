using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_Backend.Models.VM
{
    public class OrdersVM
    {
        public Guid OrderId { get; set; }

        public Guid Emp_ID { get; set; }

        public string Furniture { get; set; }

        public string Equipment { get; set; }

        public string Address { get; set; }

        public int Response { get; set; }

        public string Adm_ID { get; set; }

        public string EmpName { get; set; }
    }
}
