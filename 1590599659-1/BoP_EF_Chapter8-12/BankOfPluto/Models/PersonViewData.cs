using System;
using System.Collections.Generic;
using BoP.Core.Domain;

namespace BankOfPluto.Model
{
    public class PersonViewData
    {
        public Person Person{ get; set; }
        public List<Person> Persons { get; set; }

    }
}
