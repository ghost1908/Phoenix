using Phoenix.Web.Models.Person;
using Phoenix.Web.Models.Territory;
using System;

namespace Phoenix.Web.Models.Report
{
    public class PersonBirthdayViewModel
    {
        public Guid PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public GENDER Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Vote { get; set; }
        public AddressViewModel AddressRegistration { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsDeputy { get; set; }
        public bool IsPartyMember { get; set; }
        public bool IsCongratulated { get; set; }
        public string IsCongratulatedStyle
        {
            get
            {
                if (this.IsCongratulated)
                    return "background-color: #0F0;";
                else
                    return string.Empty;
            }
        }
    }
}
