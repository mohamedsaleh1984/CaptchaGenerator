using CaptchaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;


namespace CaptchaWebAPI.Controllers
{
    public class CaptchaController : ApiController
    {
        List<Person> people = new List<Person>();
        public CaptchaController()
        {
            people.Add(new Person { FirstName = "Mohamed", LastName = "Saleh",Id=1 });
            people.Add(new Person { FirstName = "Tolulope", LastName = "Adyemouia", Id = 2 }) ;
            people.Add(new Person { FirstName = "Mostafa", LastName = "Abdelzahir",Id =3 });

        }

        // GET: api/Captcha
        public List<Person> Get()
        { 
            return people;
        }

        [Route("api/People/GetFirstNames")]
        [HttpGet]
        public List<string> GetFirstNames()
        {
            List<string> fnames = new List<string>();

            foreach (var person in people)
            {
                fnames.Add(person.FirstName);
            }


            return fnames;  
        }

        // GET: api/Captcha/5
        public Person Get(int id)
        {
            return people.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Captcha
        public void Post(Person value)
        {
            people.Add(value);
        }

        // PUT: api/Captcha/5
        public void Put(Person val)
        {
            Person prev = people.Where(x => x.Id == val.Id).FirstOrDefault();
            people.Remove(prev);
            people.Add(val);
        }

        //// DELETE: api/Captcha/5
        //public void Delete(int id)
        //{
        //}
    }
}
