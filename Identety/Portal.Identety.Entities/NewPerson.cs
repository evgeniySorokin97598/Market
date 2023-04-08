using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Market.IdentetyServer.Entities
{
    public class NewPerson : RegistrationEntity
    {
        public string Id { get; set; }

    }
}
