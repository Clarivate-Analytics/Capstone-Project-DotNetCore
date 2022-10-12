using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Backend.Db_Context;
using Project_Backend.Models;
using Project_Backend.Models.VM;

namespace Project_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //parent 

    public class OrdersController : Controller
    {
        private readonly OrdersDbContext ordersDbContext;

        private readonly RegistrationDbContext registrationDbContext;

        public OrdersController(OrdersDbContext ordersDbContext, RegistrationDbContext registrationDbContext)
        {
            this.ordersDbContext = ordersDbContext;
            this.registrationDbContext = registrationDbContext;
        }

        //GET all orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var order = await ordersDbContext.Orders.ToListAsync();
            var res = await registrationDbContext.Registration.ToListAsync();
            IEnumerable<OrdersVM> ordersVMs = (IEnumerable<OrdersVM>)order.Select(x => new OrdersVM
            {
                OrderId = x.OrderId,
                Emp_ID = x.Emp_ID,
                EmpName = res.FirstOrDefault(y => y.Id == x.Emp_ID)?.FirstName + " " + res.FirstOrDefault(y => y.Id == x.Emp_ID)?.LastName,
                Furniture = x.Furniture,
                Equipment = x.Equipment,
                Address = x.Address,
                Response = x.Response,
                Adm_ID = x.Adm_ID
            });
            return Ok(ordersVMs);
        }


        //GET single order data
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName(nameof(GetSingleOrder))]
        public async Task<IActionResult> GetSingleOrder([FromRoute] Guid id)
        {

            var order = await ordersDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (order == null)
            {
                Console.WriteLine("employee not found");
                return NotFound("Employee not found");
            }
            Console.WriteLine("employee exist");
            return Ok(order);
        }


        //GET orders of single employee
        [HttpGet]
        [Route("emp_orders/{eid:guid}")]
        [ActionName(nameof(GetSingleEmpOrder))]
        public async Task<IActionResult> GetSingleEmpOrder([FromRoute] Guid eid)
        {
            var query = from Orders in ordersDbContext.Orders where Orders.Emp_ID == eid select Orders;
            var ord = await query.ToListAsync();
            return Ok(ord);
        }


        //GET furniture of single employee
        [HttpGet]
        [Route("furniture/{eid:guid}")] 
        [ActionName(nameof(GetSingleEmpOrder))]
        public async Task<IActionResult> GetEmpOrderFurniture([FromRoute] Guid eid)
        {
            var query = from Orders in ordersDbContext.Orders where Orders.Emp_ID == eid select Orders.Furniture;
            var ord = await query.ToListAsync();
            return Ok(ord);
        }


        //GET IT equipment of single employee
        [HttpGet]
        [Route("equipment/{eid:guid}")] 
        [ActionName(nameof(GetEmpOrderITEquipment))]
        public async Task<IActionResult> GetEmpOrderITEquipment([FromRoute] Guid eid)
        {
            var query = from Orders in ordersDbContext.Orders where Orders.Emp_ID == eid select Orders.Equipment;
            var ord = await query.ToListAsync();
            return Ok(ord);
        }



        //POST single order data
        [HttpPost]
        [ActionName(nameof(AddOrd))]
        public async Task<IActionResult> AddOrd([FromBody] Orders order)
        {
            await ordersDbContext.Orders.AddAsync(order);
            await ordersDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddOrd), order.OrderId, order);
        }


        [HttpPut]
        [Route("{id:guid}")]
        [ActionName(nameof(UpdateOrder))]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] Orders data)
        {
            Console.WriteLine(data);

            var existing_vendor = await ordersDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (existing_vendor != null)
            {
                existing_vendor.OrderId = id;
                existing_vendor.Emp_ID = existing_vendor.Emp_ID;
                existing_vendor.Furniture = existing_vendor.Furniture;
                existing_vendor.Equipment = existing_vendor.Equipment;
                existing_vendor.Address = existing_vendor.Address;
                existing_vendor.Adm_ID = data.Adm_ID;
                existing_vendor.Response = data.Response;
                await ordersDbContext.SaveChangesAsync();
                return Ok(existing_vendor);
            }

            return NotFound("Employee not found");
        }


    }
}