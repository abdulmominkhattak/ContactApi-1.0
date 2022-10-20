using ExampleAPI.Data;
using ExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [ApiController] //Anotate that is it API controller.
    [Route("api/[Controller]")]

    public class ContactController : Controller
    {
        private readonly ContactDbContext _dbcontext;

        public ContactController(ContactDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult GetContact()
        {
            return Ok(_dbcontext.Contact.ToList());
            //return View();
        }

        [HttpPost]
        public IActionResult AddContact(AddContactRequest addcontactrq)
        {

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addcontactrq.Address,
                Name = addcontactrq.Name,
                PhoneNo = addcontactrq.PhoneNo

            };

            _dbcontext.Contact.Add(contact);
            _dbcontext.SaveChanges();

            return Ok(contact);

        }

        [HttpPut]
        [Route("{id:guid}")]

        public IActionResult UpdateContact([FromRoute] Guid id, UpdateContactRequest updatecontatrequest)
        {

            var contactexits = _dbcontext.Contact.Find(id);

            if (contactexits != null)
            {
                contactexits.Name = updatecontatrequest.Name;
                contactexits.Address = updatecontatrequest.Address;
                contactexits.PhoneNo = updatecontatrequest.PhoneNo;

                _dbcontext.SaveChanges();
                return Ok(contactexits);

            }
            return NotFound();

        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetContact([FromRoute] Guid id)
        {
            var contactexits = _dbcontext.Contact.Find(id);
            if (contactexits == null)
            {
                return NotFound();
            }
            return Ok(contactexits);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteContact([FromRoute] Guid id)
        {
            var toDelete = _dbcontext.Contact.Find(id);
            if(toDelete != null)
            {
                _dbcontext.Remove(toDelete);
                _dbcontext.SaveChanges();
                return Ok("Done");
            }
            return NotFound();  
        }

    }
}
