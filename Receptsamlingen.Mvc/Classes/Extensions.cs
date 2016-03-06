using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Receptsamlingen.Mvc.Classes
{
	public static class Extensions
	{
		public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, IEnumerable<SelectListItem> listOfValues)
		{
			var sb = new StringBuilder();

			if (listOfValues != null)
			{
				sb.Append("<table>");

				foreach (var item in listOfValues)
				{
					sb.Append("<tr>");

					var label = htmlHelper.Label(item.Value, HtmlEncode(item.Text));
					var checkbox = htmlHelper.CheckBox(item.Text, new { id = item.Value }).ToHtmlString();

					sb.AppendFormat("{0}{1}", "<td>" + checkbox + "</td>", "<td>" + label + "</td>");

					sb.Append("</tr>");
				}
			}
			sb.Append("</table>");
			return MvcHtmlString.Create(sb.ToString());
		}

        public static string StripHtml(this string input)
        {
            var pattern = "<[^>]*>";
            return Regex.Replace(input, pattern, string.Empty);
        }

        public static string HtmlDecode(this string text)
		{
			return HttpUtility.HtmlDecode(text);
		}

		public static string HtmlEncode(this string text)
		{
			return HttpUtility.HtmlEncode(text);
		}
	}
}