using System.ComponentModel.DataAnnotations;

namespace Phoenix.Web.Models.Election
{
    public enum PROTOCOL_STATUS
    {
        [Display(Name = "")]
        NoProtocol = -1,

        [Display(Name = "Нет данных")]
        NoData = 0,

        [Display(Name = "Частичные данные")]
        PartialData,

        [Display(Name = "Оперативные данные")]
        OperationalData,

        [Display(Name = "Частичный протокол")]
        PartialProtocol,

        [Display(Name = "Неверный протокол")]
        InvalidProtocol,

        [Display(Name = "Верный протокол")]
        ValidProtocol
    }

    public class ProtocolItemValueViewModel
    {
        public string ProtocolId { get; set; }
        public int ProtocolStatus { get; set; }
        public string StatusCss
        {
            get
            {
                string css = string.Empty;
                switch(this.ProtocolStatus)
                {
                    case 0: 
                        css= "protocol-status-0";
                        break;
                    case 1:
                        css = "protocol-status-1";
                        break;
                    case 2:
                        css = "protocol-status-2";
                        break;
                    case 3:
                        css = "protocol-status-3";
                        break;
                    case 4:
                        css = "protocol-status-4";
                        break;
                    case 5:
                        css = "protocol-status-5";
                        break;
                    default:
                        css = "protocol-status-no";
                        break;
                }

                return css;
            }
        }
    }
}
