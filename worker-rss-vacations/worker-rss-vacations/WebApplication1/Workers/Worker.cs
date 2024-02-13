

using Microsoft.EntityFrameworkCore;
using Quartz;
using WebApplication1.Entities;

namespace WebApplication1.Workers
{
    public class Worker : IJob
    {
        private readonly ApplicationDbContext dbcontext;

        public Worker(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async  Task Execute(IJobExecutionContext context )
        {
            List<SpecialRequest> requests = await dbcontext.SpecialRequests.Where(sr => sr.State_Id == "Pendiente").ToListAsync();

            foreach (var request in requests)
            {
                request.State_Id = "aprobado";
            }
            dbcontext.UpdateRange(requests);
            await dbcontext.SaveChangesAsync();
         

            var task  = Task.FromResult(requests);
         
        }

      
    }
}
