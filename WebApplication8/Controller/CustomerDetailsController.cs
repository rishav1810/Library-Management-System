using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication8.Controller
{
    public class CustomerDetailsController : ApiController
    {
        // GET api/<controller>
        LSMEntities context =new LSMEntities();
        public List<customerDetail> Get()
        {
            var cd=context.customerDetails.ToList();
            return cd;
        }

        // GET api/<controller>/5
        public List<customerDetail> Get(int id)
        {
            var cdById=from c in context.customerDetails where c.customerId == id select c;
            return cdById.ToList();
        }

        // POST api/<controller>
        public void Post([FromBody] customerDetail value)
        {
            if (value != null)
            {
                context.customerDetails.Add(value);
                context.SaveChanges();
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var cdById = (from c in context.customerDetails where c.customerId == id select c).FirstOrDefault();
            if (cdById != null)
            {
                context.customerDetails.Remove(cdById);
                context.SaveChanges();
            }
        }
    }
}