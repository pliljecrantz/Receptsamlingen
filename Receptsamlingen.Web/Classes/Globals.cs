using System;
using System.Configuration;

namespace Receptsamlingen.Web.Classes
{
	public class Globals
	{
		// Mailsettings
		public static readonly string MailSubjectString = ConfigKey("MailSubject", "Ansökan om konto");
		public static readonly string MailRecieverString = ConfigKey("MailReceiver", "p.liljecrantz@live.com");
		public static readonly string MailServerString = ConfigKey("MailServer", "smtp.live.com");
		
		// Other strings
		public static string MainCourseString = "Huvudrätt";
		public static string AppetizerString = "Förrätt";
		public static string HeaderString = "Lägg till recept";
		public static string HeaderEditString = "Redigera";

		// Propertystrings
		public static string NameProperty = "Name";
		public static string IdProperty = "Id";

		// Error- and infomessages
		public static string ErrorSavingVote = "Det gick inte att rösta, försök igen senare";
		public static string ErrorUserHasVoted = "Du har redan röstat en gång";
		public static string ErrorDeletingRecipe = "Det gick inte att radera receptet.";
		public static string ErrorUpdatingRecipe = "Det gick inte att uppdatera receptet.";
		public static string ErrorSavingRecipe = "Det gick inte att spara receptet, försök igen senare.";
		public static string ErrorValidatingRecipe = "Du måste skriva ett namn på receptet och välja kategori.";
		public static string InfoVoteSaved = "Tack, din röst är sparad";
		public static string InfoRecipeUpdated = "Tack, receptet är uppdaterat.";
		public static string InfoRecipeSaved = "Tack, receptet är sparat.";

		// URLs
		public static string DefaultUrl = "~/start";

		#region Internal Functions

		internal static string ConfigKey(string key, string defaultvalue)
		{
			var value = ConfigurationManager.AppSettings[key];
			return String.IsNullOrEmpty(value) ? defaultvalue : value;
		}

		#endregion

	}
}