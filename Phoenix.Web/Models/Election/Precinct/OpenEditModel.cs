using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class PrecinctOpenEditModel
    {
        [JsonPropertyName("id")]
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
        /// Статус открытия избирательного участка
        /// </summary>
        [JsonPropertyName("isOpened")]
        public bool? IsOpened { get; set; }
        
        /// <summary>
        /// Причина неоткрытия избирательного участка
        /// </summary>
        [JsonPropertyName("notOpenedCause")]
        public string NotOpenedCause { get; set; }

        /// <summary>
        /// Количество избирателей по списку
        /// </summary>
        [JsonPropertyName("voters")]
        public int Voters { get; set; }

        /// <summary>
        /// Количество выданных бюллетеней по каждым выборам
        /// </summary>
        [JsonPropertyName("bulletins")]
        public List<PrecinctOpenCouncilEditModel> Bulletins { get; set; }
    }

    public class PrecinctOpenCouncilEditModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public string CouncilName { get; set; }

        [JsonPropertyName("councilBulletins")]
        public int CouncilBulletins { get; set; }
    }
}
