using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Web.Http;

namespace WebApplication8.Controller
{
    public class BookLendController : ApiController
    {
        // GET api/<controller>
        LSMEntities context=new LSMEntities();
        public List<bookLend> Get()
        {
            var bl=context.bookLends.ToList();
            return bl;
        }

        // GET api/<controller>/5
        public List<bookLend> Get(int id)
        {
            var blById= from b in context.bookLends where b.bookingId == id select b;
            return blById.ToList();
            
        }

        // POST api/<controller>
        public void Post([FromBody] bookLend value)
        {
            if (value != null)
            {
                context.bookLends.Add(value);
                var reference=(from q in context.BookDetails where q.bookId == value.bookId select q).FirstOrDefault();
                var quantity = reference.Availability;
                if (quantity > 0)
                {
                    quantity -= 1;
                }
                reference.Availability=quantity;
                
                context.SaveChanges();  
            }
        }

        // PUT api/<controller>/5
        
        public void Put(int id, [FromBody] bookLend value)
        {
            
            var statusref=(from s in context.bookLends where s.bookingId==id select s).FirstOrDefault();
            statusref.bookId = value.bookId;
            statusref.customerId=value.customerId;
            statusref.returnStatus=value.returnStatus;  
           
            if (statusref != null && statusref.returnStatus.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
            {
                var bookDetailRef= (from q in context.BookDetails where q.bookId== statusref.bookId select q).FirstOrDefault();
                if (bookDetailRef != null)
                {
                    var quantity = bookDetailRef.Availability;
                    quantity += 1;
                    bookDetailRef.Availability=quantity;
                }
            }
            context.SaveChanges();
        }     

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var blById = (from b in context.bookLends where b.bookingId == id select b).FirstOrDefault();
            if(blById != null)
            {
                context.bookLends.Remove(blById);
                context.SaveChanges();
            }
            

        }
    }
}