using System.ComponentModel.DataAnnotations;

namespace Project_Backend.Models
{
    public class Registration
    {
        [Key]
        public Guid Id { get; set; }
        public string EmpID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Orders> order { get; set; }

    }
}
