using System;
using System.Collections.Generic;

namespace Phoenix.Infrastructure.Entities
{
    public class Person
    {
        public Guid ID { get; set; }
        public string LNAME { get; set; }
        public string FNAME { get; set; }
        public string MNAME { get; set; }
        public bool? GENDER { get; set; }
        public DateTime? BDATE { get; set; }
        public bool VOTE { get; set; }
        public Guid? ADDR_REG { get; set; }
        public string ADDR_REG_ROOM { get; set; }
        public Guid? ADDR_HOME { get; set; }
        public string ADDR_HOME_ROOM { get; set; }
        public string PHONE { get; set; }
        public bool HAS_TELEGRAM { get; set; }
        public bool HAS_VIBER { get; set; }
        public bool HAS_WHATSAPP { get; set; }
        public string EMAIL { get; set; }
        public bool HAS_FACEBOOK { get; set; }
        public bool HAS_INSTAGRAM { get; set; }
        public bool IS_EMPLOYEE { get; set; }
        public bool IS_DEPUTY { get; set; }
        public bool IS_PARTY_MEMBER { get; set; }
        public bool IS_DELETED { get; set; }
        public DateTime? DCREATE { get; set; }
    }

    public class PersonList
    {
        public long TotalCount { get; set; }
        public List<Person> Persons { get; set; }
    }
}
