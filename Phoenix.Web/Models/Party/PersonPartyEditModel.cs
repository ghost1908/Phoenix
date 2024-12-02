using System;

namespace Phoenix.Web.Models.Party
{
    public class PersonPartyEditModel
    {
        public string Id { get; set; }

        /// <summary>
        /// Дата заявления на вступление в партию
        /// </summary>
        public string EntryDate { get; set; }

        /// <summary>
        /// Дата принятия в партию
        /// </summary>
        public string AdoptionDate { get; set; }

        /// <summary>
        /// Номер документа о принятии в партию
        /// </summary>
        public string AdoptionNumber { get; set; }

        /// <summary>
        /// Коментарий
        /// </summary>
        public string AdoptionComment { get; set; }

        /// <summary>
        /// Дата выхода/исключения из партии
        /// </summary>
        public string DisposalDate { get; set; }

        /// <summary>
        /// Номер документа о выходе из партии
        /// </summary>
        public string DisposalNumber { get; set; }

        /// <summary>
        /// Причина выхода/исключения из партии
        /// </summary>
        public string DisposalCause { get; set; }

        /// <summary>
        /// Комментарий к причине выхода/исключения из партии
        /// </summary>
        public string DisposalComment { get; set; }

        /// <summary>
        /// Номер партийного билета
        /// </summary>
        public string TicketNumber { get; set; }

        public string Action { get; set; }
    }
}
