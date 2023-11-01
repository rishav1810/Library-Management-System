using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication8.Controller
{
    public class BookDetailController : ApiController
    {
        // GET api/<controller>
        LSMEntities context=new LSMEntities();
        public List<BookDetail> Get()
        {
            var bd=context.BookDetails.ToList();
            return bd;
        }

        // GET api/<controller>/5
        public List<BookDetail> Get(int id)
        {
            var bookByid = (from b in context.BookDetails where b.bookId == id select b).ToList();
             return bookByid;
        }

        // POST api/<controller>
        public void Post([FromBody] BookDetail bd) {
            if (bd != null)
            {
                context.BookDetails.Add(bd);
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
            var bookByid = (from b in context.BookDetails where b.bookId == id select b).FirstOrDefault();
            if(bookByid != null)
            {
                context.BookDetails.Remove(bookByid);
                context.SaveChanges();
            }
        }
    }
}