using System;
using System.Collections.Generic;

namespace Phoenix.Web.Models.Person
{
    using Phoenix.Web.Helpers;
    using Phoenix.Web.Models.Event;
    using Phoenix.Web.Models.Party;
    using Phoenix.Web.Models.Organization;
    using Phoenix.Web.Models.Territory;
    using System.ComponentModel.DataAnnotations;

    public class PersonViewModel
    {
        public Guid PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public GENDER Gender { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:dd.MM.yyyy}")]
        public DateTime? BirthDate { get; set; }
        public bool Vote { get; set; }
        public AddressViewModel AddressRegistration { get; set; }
        public AddressViewModel AddressLiving { get; set; }
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
        public DateTime? CreateDate { get; set; }
        public byte? LastStatus { get; set; }
        public string LastStatusName { get { return this.LastStatus.HasValue ? ((PERSON_EVENT_STATUS)this.LastStatus.Value).GetAttribute<DisplayAttribute>().Name : "Невідомо"; } }
        public byte LastFormStatus { get; set; }
        public string LastFormStatusName { get { return ((PERSON_FORM_STATUS)this.LastFormStatus).GetAttribute<DisplayAttribute>().Name; } }

        public List<PersonFormStatusViewModel> FormStatuses { get; set; }
        public PersonInfoViewModel PersonInfo { get; set; }
        public List<PersonPositionViewModel> PersonPositions { get; set; }
        public List<PersonEventViewModel> PersonEvents { get; set; }
        public List<PersonPartyViewModel> PersonParties { get; set; }

        public PersonViewModel()
        {
            this.FormStatuses = new List<PersonFormStatusViewModel>();
            this.PersonPositions = new List<PersonPositionViewModel>();
            this.PersonEvents = new List<PersonEventViewModel>();
            this.PersonParties = new List<PersonPartyViewModel>();
        }
    }

    public class PersonInfoViewModel
    {
        public Passport Passport { get; set; }
        public string TaxNumber { get; set; }
    }

    public class Passport
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public string Issue { get; set; }
    }

    public enum GENDER
    {
        [Display(Name = "Женский")]
        Female,
        [Display(Name = "Мужской")]
        Male
    }
}
