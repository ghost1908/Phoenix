using System;

namespace Phoenix.Infrastructure.Entities
{
    public class PersonFormStatus
    {
        public Guid ID { get; set; }

        public Guid PSN_ID { get; set; }

        /// <summary>
        /// 0 - не має статусу
        /// 1 - відправлено на доопрацювання
        /// 2 - повернено з доопрацівання
        /// 255 - підтвердженно
        /// </summary>
        public byte STATUS { get; set; }

        public Guid USER_ID { get; set; }

        public DateTime CREATE_DATE { get; set; }

        public string COMMENT { get; set; }

        public virtual int ACTION { get; set; }
    }
}
