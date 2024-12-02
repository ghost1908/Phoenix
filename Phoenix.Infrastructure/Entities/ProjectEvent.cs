using System;

namespace Phoenix.Infrastructure.Entities
{
    public class ProjectEvent
    {
        public Guid ID { get; set; }
        public string EVT_NAME { get; set; }

        /// <summary>
        /// false - касание
        /// true - мероприятие
        /// </summary>
        public bool EVT_TYPE { get; set; }
        /// <summary>
        /// Флаг - доступ по типу анкеты (член партии, депутат, сторонник и т.д.)
        /// Порядок флагов -> 7 6 5 4 3 2 1 0
        /// 0 - доступен всем
        /// 1 - только сторонникам
        /// 2 - только сотрудникам
        /// 3 - только членам партии
        /// 4 - только депутатам
        /// 5 - не используется
        /// 6 - не используется
        /// 7 - не используется
        /// </summary>
        public byte EVT_ACCESS { get; set; }
        public Guid PRJT_ID { get; set; }
        public DateTime EVT_START { get; set; }
        public DateTime EVT_END { get; set; }
        public Guid ORG_ID { get; set; }
    }
}
