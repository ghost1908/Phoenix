using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election.Online
{
    public class TotalProtocolByCouncil
    {
        public string CouncilId { get; set; }

        public string CouncilName { get; set; }
        public int TotalStatuses { get; set; }
        public List<TotalProtocolStatus> Statuses { get; set; }
        public List<TotalProtocolCandidateItem> Candidates { get; set; }
    }

    public class TotalProtocolStatus
    {
        public int ProtocolStatusId { get; set; }
        public string ProtocolStatusName
        {
            get
            {
                switch (this.ProtocolStatusId)
                {
                    case 0:
                        return "Нет данных";
                    case 1:
                        return "Частичные данных";
                    case 2:
                        return "Оперативные данных";
                    case 3:
                        return "Частичный протокол";
                    case 4:
                        return "Неверный протокол";
                    case 5:
                        return "Верный протоколол";
                    default:
                        return "Статус неизвестен";
                }
            }
        }
        public int ProtocolStatusCount { get; set; }
        public double ProtocolStatusPercent { get; set; }
    }

    public class TotalProtocolCandidateItem
    {
        public int WatchOrder { get; set; }
        public string CandidateName { get; set; }
        public long CandidateValue { get; set; }
        public float CandidatePercent { get; set; }
    }
}
