using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election.Online
{
    public class ProtocolStatusViewModel
    {
        public List<ProtocolStatusItemViewModel> Rows { get; set; }
        public List<ProtocolStatusTypeViewModel> Columns { get; set; }
    }

    public class ProtocolStatusTypeViewModel
    {
        public int StatusID { get; set; }
        public string StatusName
        {
            get
            {
                switch (this.StatusID)
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
        public int StatusCount { get; set; }
    }

    public class ProtocolStatusItemViewModel
    {
        public int WatchOrder { get; set; }
        public string CouncilName { get; set; }
        public List<ProtocolStatusTypeViewModel> Statuses { get; set; }

        public List<ProtocolStatusItemViewModel> Children { get; set; }
    }
}
