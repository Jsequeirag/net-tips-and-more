using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/")]
    public class requestController:ControllerBase
    {
        private readonly ApplicationDbContext context;

        public requestController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SpecialRequest>>> Get()
        {
           var request = await context.SpecialRequests.Where(sr=>sr.State_Id=="Pendiente").ToListAsync();
            await context.AddRangeAsync(request);
            await context.SaveChangesAsync();
            return Ok(request); 
        }
    }
}
