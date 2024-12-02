using System.Collections.Generic;

namespace Phoenix.Web.Models.Person
{
    public class PersonListViewModel
    {
        public List<PersonViewModel> Persons { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public string ItemsPerPage { get; set; }
        public PersonFilterModel Filter { get; set; }

        public PersonListViewModel()
        {
            this.Persons = new List<PersonViewModel>();
            this.PaginationInfo = new PaginationInfo();
            this.Filter = new PersonFilterModel();
        }
    }
}
