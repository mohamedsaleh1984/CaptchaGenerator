using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaptchaWebAPI.Models
{
    /// <summary>
    /// Represents one specific user.
    /// </summary>
    public class Person
    {

        /// <summary>
        /// User unique id
        /// </summary>
        public int Id { get; set; } = 0;
        /// <summary>
        /// User First Name
        /// </summary>
        public string FirstName { get; set; } = "";
        /// <summary>
        /// User Last Name
        /// </summary>
        public string LastName { get; set; } = "";
    }
}