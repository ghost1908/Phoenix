using Phoenix.Web.Models.Event;
using Phoenix.Web.Models.Organization;
using Phoenix.Web.Models.Party;
using System;
using System.Collections.Generic;

namespace Phoenix.Web.Models.Person
{
    public class PersonEditModel
    {
        //[JsonPropertyName("id")]
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int Gender { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:dd.MM.yyyy}")]
        public DateTime? BirthDate { get; set; }
        //public bool Vote { get; set; }
        public string RegistrationID { get; set; }
        public string RegistrationStreetID { get; set; }
        public string RegistrationBuilding { get; set; }
        public string RegistrationApartment { get; set; }
        public string LivingID { get; set; }
        public string LivingStreetID { get; set; }
        public string LivingBuilding { get; set; }
        public string LivingApartment { get; set; }
        public string Phone { get; set; }
        public bool HasTelegram { get; set; }
        public bool HasViber { get; set; }
        public bool HasWhatsapp { get; set; }
        public string Email { get; set; }
        public bool HasFacebook { get; set; }
        public bool HasInstagram { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsDeputy { get; set; }
        public bool IsPartyMember { get; set; }
        public bool IsDeleted { get; set; }
        public PersonInfoEditModel PersonInfo { get; set; }
        public PERSON_FORM_STATUS LastFormStatus { get; set; }
        public List<PersonFormStatusEditModel> FormStatuses { get; set; }
        public List<PersonPositionEditModel> Positions { get; set; }
        public List<PersonEventEditModel> Events { get; set; }
        public List<PersonPartyEditModel> Parties { get; set; }
    }

    public class PersonInfoEditModel
    {
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssue { get; set; }
        public string TaxNumber { get; set; }
    }
}
