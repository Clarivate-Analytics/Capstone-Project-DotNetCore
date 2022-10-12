using EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Backend.Db_Context;

namespace Project_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmailController : Controller
    {

        private readonly IEmailSender _emailSender;

        private VendorDbContext vendorsDbContext;

        private RegistrationDbContext registrationDbContext;

        public EmailController(VendorDbContext vendorsDbContext, IEmailSender emailSender, RegistrationDbContext registrationDbContext)
        {
            this.vendorsDbContext = vendorsDbContext;
            this.registrationDbContext = registrationDbContext;
            _emailSender = emailSender;
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail([FromRoute] Guid id)
        {
            var rng = new Random();

            var ven = await vendorsDbContext.Vendors.ToListAsync();

            var reg = await registrationDbContext.Registration.ToListAsync();

            var empid = ven.FirstOrDefault(x => x.Orders_ID == id)?.Emp_ID;

            var mailid = reg.FirstOrDefault(x => x.Id == empid)?.Email;

            if (!string.IsNullOrEmpty(mailid))
            {
                var deldate = ven.FirstOrDefault(x => x.Orders_ID == id)?.Date;

                var message = new Message(new string[] { mailid }, "Order Confirmed",
                    "Your order will be delivered on " + deldate + "\n Thank You :)");
                _emailSender.SendEmail(message);
            }
            else
            {
                Console.WriteLine("email not found");
            }
            return Ok(true);
        }


        [HttpGet]
        [Route("update/{id:guid}")]
        [ActionName(nameof(UpdateEmail))]
        public async Task<IActionResult> UpdateEmail([FromRoute] Guid id)
        {
            var rng = new Random();

            var ven = await vendorsDbContext.Vendors.ToListAsync();

            var reg = await registrationDbContext.Registration.ToListAsync();

            var empid = ven.FirstOrDefault(x => x.Orders_ID == id)?.Emp_ID;

            var mailid = reg.FirstOrDefault(x => x.Id == empid)?.Email;

            if (!string.IsNullOrEmpty(mailid))
            {
                var deldate = ven.FirstOrDefault(x => x.Orders_ID == id)?.Date;

                var message = new Message(new string[] { mailid }, "Order Updated",
                    "Order delivery date updated " + deldate + "\n Thank You :)");
                _emailSender.SendEmail(message);
            }
            else
            {
                Console.WriteLine("email not found");
            }
            return Ok(true);
        }
    }

}