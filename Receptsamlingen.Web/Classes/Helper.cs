using System.Text.RegularExpressions;

namespace Receptsamlingen.Web.Classes
{
	public static class Helper
	{
		public static string CapitalizeFirstLetter(string text)
		{
			var textFirstLetter = text.Substring(0, 1);
			var textFirstLetterUpper = textFirstLetter.ToUpper();
			var textRestOfLetters = text.Substring(1, text.Length - 1);
			var result = textFirstLetterUpper + textRestOfLetters;
			return result;
		}

		public static bool ValidateEmail(string emailaddress)
		{
			var isValid = false;
			const string expression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
			                          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
			                          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			var regex = new Regex(expression);
			if (regex.IsMatch(emailaddress))
			{
				isValid = true;
			}
			return isValid;
		}
	}
}