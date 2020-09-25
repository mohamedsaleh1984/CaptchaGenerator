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
    /// <summary>
    /// Captcha WebAPI Controller
    /// </summary>
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

        /// <summary>
        /// Get People First Names
        /// </summary>
        /// <returns></returns>
        [Route("api/People/GetFirstNames")]
        [HttpGet]
        public List<string> GetFirstNames()
        {
            List<string> fnames = new List<string>();
            foreach (var person in people)
                fnames.Add(person.FirstName);
            
            return fnames;  
        }

        /// <summary>
        /// Get People First Names based on UserId and Age
        /// </summary>
        /// <param name="userId">The unique identifier for the user</param>
        /// <param name="age">User's age</param>
        /// <returns>List of first names...duh</returns>
        [Route("api/People/GetFirstNames/{userId:int}/{age:int}")]
        [HttpGet]
        public List<string> GetFirstNamesParams(int userId,int age)
        {
            List<string> fnames = new List<string>();
            foreach (var person in people)
                fnames.Add(person.FirstName);

            return fnames;
        }

        /// <summary>
        /// Get person based on the person's Id.
        /// GET: api/Captcha/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person Get(int id)
        {
            return people.Where(x => x.Id == id).FirstOrDefault();
        }


        /// <summary>
        /// Post user info
        /// </summary>
        /// <param name="value"></param>
        // POST: api/Captcha
        public void Post(Person value)
        {
            people.Add(value);
        }

        /// <summary>
        /// Replace User's data.
        /// </summary>
        /// <param name="val"></param>
        // PUT: api/Captcha/5
        public void Put(Person val)
        {
            Person prev = people.Where(x => x.Id == val.Id).FirstOrDefault();
            people.Remove(prev);
            people.Add(val);
        }

        /// <summary>
        /// Delete User base on the Id
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Captcha/5
        public void Delete(int id)
        {
            Person prev = people.Where(x => x.Id == id).FirstOrDefault();
            people.Remove(prev);
        }
    }
}
