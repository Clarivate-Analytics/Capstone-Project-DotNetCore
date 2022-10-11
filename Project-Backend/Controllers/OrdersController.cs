using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Backend.Db_Context;
using Project_Backend.Models;

namespace Project_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //parent 

    public class OrdersController : Controller
    {
        private readonly OrdersDbContext ordersDbContext;

        public OrdersController(OrdersDbContext ordersDbContext)
        {
            this.ordersDbContext = ordersDbContext;
        }

        //GET all orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var order = await ordersDbContext.Orders.ToListAsync();
            return Ok(order);
        }


        //GET single order data
        [HttpGet]
        [Route("{id:guid}")] //child ------ ~/order/{id}
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
        [Route("emp_orders/{eid}")] //child ------ ~/order/{id}
        [ActionName(nameof(GetSingleEmpOrder))]
        public async Task<IActionResult> GetSingleEmpOrder([FromRoute] string eid)
        {
            var query = from Orders in ordersDbContext.Orders where Orders.Emp_ID == eid select Orders;
            var ord = await query.ToListAsync();
            return Ok(ord);
        }


        //GET furniture of single employee
        [HttpGet]
        [Route("furniture/{eid}")] //child ------ ~/order/{id}
        [ActionName(nameof(GetSingleEmpOrder))]
        public async Task<IActionResult> GetEmpOrderFurniture([FromRoute] string eid)
        {
            var query = from Orders in ordersDbContext.Orders where Orders.Emp_ID == eid select Orders.Furniture;
            var ord = await query.ToListAsync();
            return Ok(ord);
        }


        //GET IT equipment of single employee
        [HttpGet]
        [Route("equipment/{eid}")] //child ------ ~/order/{id}
        [ActionName(nameof(GetEmpOrderITEquipment))]
        public async Task<IActionResult> GetEmpOrderITEquipment([FromRoute] string eid)
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

        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName(nameof(DeleteOrder))]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            var existing_order = await ordersDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (existing_order != null)
            {
                ordersDbContext.Remove(existing_order);
                await ordersDbContext.SaveChangesAsync();
                return Ok(existing_order);
            }

            return NotFound("Employee not found");
        }

    }
}