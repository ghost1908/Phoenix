using Phoenix.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Person
{
    public enum PERSON_FORM_STATUS : byte
    {
        [Display(Name = "Без статусу")]
        None = 0,

        [Display(Name = "Відправлено на доопрацювання")]
        ToRework = 1,

        [Display(Name = "Повернуто з доопрацювання")]
        FromRework = 2,

        [Display(Name = "Підтверджено")]
        Accepted = 255
    }

    public class PersonFormStatusViewModel
    {
        public Guid Id { get; set; }
        
        public byte FormStatus { get; set; }

        public string FormStatusName { get { return ((PERSON_FORM_STATUS)this.FormStatus).GetAttribute<DisplayAttribute>().Name; } }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreateDate { get; set; }

        public string Comment { get; set; }

        public TABLE_ROW_ACTION Action { get; set; }
    }

    public class PersonFormStatusEditModel
    {
        public string Id { get; set; }
        public string FormStatus { get; set; }
        public string UserId { get; set; }
        public string CreateDate { get; set; }
        public string Comment { get; set; }
        public string Action { get; set; }
    }
}
