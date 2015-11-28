using System;
using System.Configuration;

namespace Receptsamlingen.Mvc.Classes
{
    public static class Globals
    {
		// Session strings
		public const string UserSessionString = "User";
		public const string IsAuthenticatedSessionString = "IsAuthenticated";
		public const string CurrentGuidString = "CurrentGuid";

        // Mailsettings
        public static readonly string MailSubjectString = ConfigKey("MailSubject", "Ansökan om konto");
        public static readonly string MailRecieverString = ConfigKey("MailReceiver", "ansok@liljecrantz.nu");
        public static readonly string MailServerString = ConfigKey("MailServer", "mail.liljecrantz.nu");

        // Other strings
        public const string MainCourseString = "Huvudrätt";
        public const string AppetizerString = "Förrätt";
        public const string HeaderString = "Lägg till recept";
        public const string HeaderEditString = "Redigera";
	    public const string UserSessionKeyString = "User";

        // Error- and infomessages
        public const string ErrorSavingVote = "Det gick inte att rösta, försök igen senare";
        public const string ErrorUserHasVoted = "Du har redan röstat en gång";
        public const string ErrorDeletingRecipe = "Det gick inte att radera receptet.";
        public const string ErrorUpdatingRecipe = "Det gick inte att uppdatera receptet.";
        public const string ErrorSavingRecipe = "Det gick inte att spara receptet, försök igen senare.";
        public const string ErrorValidatingRecipe = "Du måste skriva ett namn på receptet och välja kategori.";
        public const string InfoVoteSaved = "Tack, din röst är sparad";
        public const string InfoRecipeUpdated = "Tack, receptet är uppdaterat.";
        public const string InfoRecipeSaved = "Tack, receptet är sparat.";
	    public const string InfoRecipeDeleted = "Receptet är raderat.";

        // URLs
        public const string DefaultUrl = "~/start";

        #region Internal Functions

        internal static string ConfigKey(string key, string defaultvalue)
        {
            var value = ConfigurationManager.AppSettings[key];
            return String.IsNullOrEmpty(value) ? defaultvalue : value;
        }

        #endregion
    }
}