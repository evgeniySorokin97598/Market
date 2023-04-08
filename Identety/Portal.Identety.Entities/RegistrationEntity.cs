using Market.IdentetyServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Dto
{
    public  class RegistrationEntity : IdentetyUser
    {
        //public string Id { get; set; }
        public string Name { get; set; }
        
        public string LastName { get; set; }
        public string City { get; set; }
       
        public int Age { get; set; }
        public DateTime DateBirthday { get; set; }
        public bool IsMan { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
