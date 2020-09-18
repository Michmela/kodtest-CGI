using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using kodtest_CGI.Models;

namespace kodtest_CGI.Controllers
{
    public class BusinesscardController : ApiController
    {
        //GET Retrieve 
        public IHttpActionResult GetAllBusinesscard()
        {
            IList<BusinesscardViewModel> businesscards = null;
            using (var x = new CRUDDBEntities())
            {
                businesscards = x.businesscard
                    .Select(c => new BusinesscardViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SurName = c.SurName,
                        Telephone = c.Telephone,
                        Email = c.Email
                    }).ToList<BusinesscardViewModel>();
            }

            if (businesscards.Count == 0)
            {
                return NotFound();
            }
     
            return Ok(businesscards);
        }

        //POST Create 
        public IHttpActionResult PostNewBusinesscard(BusinesscardViewModel businesscard)
        {
            if (!ModelState.IsValid)
                return BadRequest("Inkorrekt inmatning, prova igen!");

            using (var x = new CRUDDBEntities())
            {
                x.businesscard.Add(new businesscard()
                {
                    Id = businesscard.Id,
                    Name = businesscard.Name,
                    SurName = businesscard.SurName,
                    Telephone = businesscard.Telephone,
                    Email = businesscard.Email,
                });
                x.SaveChanges();
            }

            return Ok("Visitkortet lades till!");
        }

        //PUT Update based on id
        public IHttpActionResult PutBusinesscard(BusinesscardViewModel businesscard)
        {
            if (!ModelState.IsValid)
                return BadRequest("Inkorrekt inmatning, prova igen!");

            using (var x = new CRUDDBEntities())
            {
                var controlExistingBusinesscard = x.businesscard.Where(c => c.Id == businesscard.Id)
                    .FirstOrDefault<businesscard>();
                if (controlExistingBusinesscard != null)
                {
                    controlExistingBusinesscard.Name = businesscard.Name;
                    controlExistingBusinesscard.SurName = businesscard.SurName;
                    controlExistingBusinesscard.Telephone = businesscard.Telephone;
                    controlExistingBusinesscard.Email = businesscard.Email;

                    x.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }

        //DELETE Delete based on id
        public IHttpActionResult DeleteBusinesscard(int id)
        {
            if (id <= 0)
                return BadRequest("Inkorrekt inmatning, prova igen!");
            using (var x = new CRUDDBEntities())
            {
                var businesscard = x.businesscard
                    .Where(c => c.Id == id)
                    .FirstOrDefault();

                x.Entry(businesscard).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }
    }
}
