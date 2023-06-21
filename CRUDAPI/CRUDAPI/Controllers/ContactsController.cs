using CRUDAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        //Use dbContext to talk to in memory database
        private readonly CRUDAPIDbContext dbContext;

        public ContactsController(CRUDAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(dbContext.Contacts.ToList());
           
        }
    }
}
