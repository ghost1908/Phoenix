using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix.Web.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phoenix.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.Web
{
    public static class MvcHtmlHelpers
    {
        public static HtmlString YesNo( this IHtmlHelper htmlHelper, bool yesNo)
        {
            return new HtmlString(yesNo ? "Так" : "Ні");
        }

        public static HtmlString ShortDate(this IHtmlHelper htmlHelper, DateTime value)
        {
            return new HtmlString(string.Format("{0:dd.MM.yyyy}", value));
        }

        public static HtmlString ShortDateTime(this IHtmlHelper htmlHelper, DateTime value)
        {
            return new HtmlString(string.Format("{0:dd.MM.yyyy HH:mm}", value));
        }

        public static HtmlString LongDateTime(this IHtmlHelper htmlHelper, DateTime value)
        {
            return new HtmlString(string.Format("{0:dd.MM.yyyy HH:mm:ss}", value));
        }

        public static HtmlString DateNull(this IHtmlHelper htmlHelper, DateTime? value)
        {
            if (value.HasValue)
                return new HtmlString(string.Format("{0:dd.MM.yyyy}", value.Value));
            else
                return new HtmlString(string.Empty);
        }

        public static HtmlString DateTimeNull(this IHtmlHelper htmlHelper, DateTime? value)
        {
            if (value.HasValue)
                return new HtmlString(string.Format("{0:dd.MM.yyyy HH:mm}", value.Value));
            else
                return new HtmlString(string.Empty);
        }

        public static HtmlString PersonStatus(this IHtmlHelper htmlHelper, byte? value)
        {
            if (value.HasValue)
                return new HtmlString(((PERSON_EVENT_STATUS)value.Value).GetAttribute<DisplayAttribute>().Name);
            else
                return new HtmlString("Невідомо");
        }
    }
}
