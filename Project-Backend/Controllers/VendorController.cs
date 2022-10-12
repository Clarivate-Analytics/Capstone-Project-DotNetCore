using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Backend.Models;
using Project_Backend.Db_Context;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : Controller
    {
        private readonly VendorDbContext vendorDbContext;

        public VendorController(VendorDbContext vendorDbContext)
        {
            this.vendorDbContext = vendorDbContext;
        }

        //GET all data
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var vendor = await vendorDbContext.Vendors.ToListAsync();
            return Ok(vendor);
        }

        //POST single data
        [HttpPost]
        [ActionName(nameof(AddData))]
        public async Task<IActionResult> AddData([FromBody] Vendors vendor)
        {
            await vendorDbContext.Vendors.AddAsync(vendor);
            await vendorDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddData), vendor.ID, vendor);
        }


        //PUT Date and VendorId
        [HttpPut]
        [Route("{id:guid}")]
        [ActionName(nameof(UpdateData))]
        public async Task<IActionResult> UpdateData([FromRoute] Guid id, [FromBody] Vendors vendor)
        {
            var existing_vendor = await vendorDbContext.Vendors.FirstOrDefaultAsync(x => x.Orders_ID == id);
            if (existing_vendor != null)
            {
                Console.WriteLine(existing_vendor);
                existing_vendor.ID = existing_vendor.ID;
                existing_vendor.Orders_ID = existing_vendor.Orders_ID;
                existing_vendor.Emp_ID = existing_vendor.Emp_ID;
                existing_vendor.Furniture = existing_vendor.Furniture;
                existing_vendor.Equipment = existing_vendor.Equipment;
                existing_vendor.Address = existing_vendor.Address;
                existing_vendor.Date = vendor.Date;
                existing_vendor.Ven_ID = vendor.Ven_ID;
                await vendorDbContext.SaveChangesAsync();
                return Ok(existing_vendor);
            }

            return NotFound("Employee not found");


        }


    }
}