using CRUDAPI.Data;
using CRUDAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ShowDetailController : Controller
    {
        //Use dbContext to talk to in memory database
        private readonly CRUDAPIDbContext dbContext;

        public ShowDetailController(CRUDAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetShowDetails()
        {
            return Ok(await dbContext.ShowDetails.ToListAsync());

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetShowDetail([FromRoute] Guid id)
        {
            //Try to find the id in the database
            var showdetail = await dbContext.ShowDetails.FindAsync(id);
            if (showdetail == null)
            {
                return NotFound();
            }
            return Ok(showdetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddShowDetail(ShowDetailRequest showDetailRequest)
        {
            var showdetail = new ShowDetail()
            {
                Id = Guid.NewGuid(),
                ShowName= showDetailRequest.ShowName,
                Genre = showDetailRequest.Genre,
                Year = showDetailRequest.Year,
                Type = showDetailRequest.Type
            };

            //add to the data base
            await dbContext.ShowDetails.AddAsync(showdetail);
            //save the changes in the database after adding object into it
            await dbContext.SaveChangesAsync();
            return Ok(showdetail);
        }

        [HttpPut]
        [Route("{id:guid}")]  //put the id in the route which is of type guid
        public async Task<IActionResult> UpdateShowDetail([FromRoute] Guid id, ShowDetailRequest showDetailRequest)
        {
            //Try to find the id in the database
            var showdetail = await dbContext.ShowDetails.FindAsync(id);
            if (showdetail != null)
            {
                showdetail.Genre = showDetailRequest.Genre;
                showdetail.Type = showDetailRequest.Type;
                showdetail.ShowName = showDetailRequest.ShowName;
                showdetail.Year = showDetailRequest.Year;

                //save the database after making changes
                await dbContext.SaveChangesAsync();
                return Ok(showdetail);
            }

            //return saying the id is not found in the database
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteShowDetail([FromRoute] Guid id)
        {
            var showdetail = await dbContext.Contacts.FindAsync(id);
            if (showdetail != null)
            {
                dbContext.Remove(showdetail);
                await dbContext.SaveChangesAsync();
                return Ok(showdetail);
            }
            return NotFound();
        }

    }
    
}

