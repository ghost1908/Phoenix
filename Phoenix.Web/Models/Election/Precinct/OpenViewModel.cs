using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class PrecinctOpenViewModel
    {
        public string Id { get; set; }
        
        /// <summary>
        /// Номер избирательного участка
        /// </summary>
        public string Number { get; set; }
        
        /// <summary>
        /// Наименование района
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// Наименование общины
        /// </summary>
        public string CommunityName { get; set; }
        
        /// <summary>
        /// Статус открытия избирательного участка
        /// </summary>
        public bool? IsOpened { get; set; }
        
        /// <summary>
        /// Причина неоткрытия избирательного участка
        /// </summary>
        public string NotOpenedCause { get; set; }

        /// <summary>
        /// Количество избирателей по списку
        /// </summary>
        public int? Voters { get; set; }

        public string BackgroudStyle
        {
            get
            {
                if (this.IsOpened.HasValue)
                {
                    if (IsOpened.Value)
                        return "background: #38eb47;";
                    else
                        return "background: #e85e5e;";
                }

                return string.Empty;
            }
        }
    }
}
