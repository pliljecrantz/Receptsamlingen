using System;
using System.Configuration;

namespace Receptsamlingen.Mvc.Classes
{
    public static class Globals
    {
		// Session strings
		public const string CurrentRecipeIdSessionString = "CurrentRecipeId";
		public const string RecipeIdListSessionString = "RecipeIdList";
		public const string UserSessionString = "User";
		public const string IsAuthenticatedSessionString = "IsAuthenticated";

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
        public const string ERROR_SAVING_VOTE = "Det gick inte att rösta, försök igen senare";
        public const string ERROR_USER_HAS_VOTED = "Du har redan röstat en gång";
        public const string ERROR_DELETING_RECIPE = "Det gick inte att radera receptet.";
        public const string ERROR_UPDATING_RECIPE = "Det gick inte att uppdatera receptet.";
        public const string ERROR_SAVING_RECIPE = "Det gick inte att spara receptet, försök igen senare.";
        public const string ERROR_VALIDATING_RECIPE = "Du måste skriva ett namn på receptet och välja kategori.";
        public const string INFO_VOTE_SAVED = "Tack, din röst är sparad";
        public const string INFO_RECIPE_UPDATED = "Tack, receptet är uppdaterat.";
        public const string INFO_RECIPE_SAVED = "Tack, receptet är sparat.";

        // URLs
        public const string DEFAULT_URL = "~/start";

        #region Internal Functions

        internal static string ConfigKey(string key, string defaultvalue)
        {
            var value = ConfigurationManager.AppSettings[key];
            return String.IsNullOrEmpty(value) ? defaultvalue : value;
        }

        #endregion
    }
}