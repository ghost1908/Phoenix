using System;

namespace Phoenix.Infrastructure.Entities
{
    public class PersonParty
    {
        public Guid ID { get; set; }

        /// <summary>
        /// ID анкеты
        /// </summary>
        public Guid PSN_ID { get; set; }

        /// <summary>
        /// Дата заявления на вступление в партию
        /// </summary>
        public DateTime DATE_ENTRY { get; set; }

        /// <summary>
        /// Дата принятия в партию
        /// </summary>
        public DateTime? DATE_ADOPTION { get; set; }

        /// <summary>
        /// Номер документа (например, протокола)
        /// </summary>
        public string ADOPTION_NUMBER { get; set; }

        /// <summary>
        /// Коментарий
        /// </summary>
        public string ADOPTION_COMMENT { get; set; }

        /// <summary>
        /// Дата выхода/исключения из партии
        /// </summary>
        public DateTime? DATE_DISPOSAL { get; set; }

        /// <summary>
        /// Номер документа о выходе (например, протокола или пустое значение, если по собственному желанию)
        /// </summary>
        public string DISPOSAL_NUMBER { get; set; }

        /// <summary>
        /// Причина выхода/исключения из партии
        /// </summary>
        public byte? DISPOSAL_CAUSE { get; set; }

        /// <summary>
        /// Комментарий к причине выхода/исключения из партии
        /// </summary>
        public string DISPOSAL_COMMENT { get; set; }

        /// <summary>
        /// Номер партийного билета
        /// </summary>
        public string TICKET_NUMBER { get; set; }

        public Guid USER_ID { get; set; }

        public DateTime DATE_CREATE { get; set; }

        public virtual byte ACTION { get; set; }
    }
}
