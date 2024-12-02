using System;

namespace Phoenix.Infrastructure.Entities
{
    public class PersonEvent
    {
        public Guid ID { get; set; }
        public Guid PRE_ID { get; set; }
        public Guid PSN_ID { get; set; }
        public DateTime REG_DATE { get; set; }

        /// <summary>
        /// 0 - чужой
        /// 1 - свой
        /// 2 - потенциальный
        /// 3 - нейтральный
        /// </summary>
        public byte PSN_STATUS { get; set; }
        public string EVT_COMMENT { get; set; }
        public Guid USER_ID { get; set; }
        public DateTime? EVT_CREATE { get; set; }

        public virtual int ACTION { get; set; }
    }

    public class PersonEventData
    {
        public Guid ID { get; set; }
        public Guid PSN_ID { get; set; }
        public Guid PRJT_ID { get; set; }
        public string PRJT_NAME { get; set; }
        public Guid EVT_ID { get; set; }
        public string EVT_NAME { get; set; }
        public bool EVT_TYPE { get; set; }
        public string EVT_COMMENT { get; set; }
        public DateTime REG_DATE { get; set; }

        /// <summary>
        /// 0 - чужой
        /// 1 - свой
        /// 2 - потенциальный
        /// 3 - нейтральный
        /// </summary>
        public byte PSN_STATUS { get; set; }

        public Guid USER_ID { get; set; }
        public DateTime? EVT_CREATE { get; set; }
    }
}
