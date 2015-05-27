using System;
using System.Configuration;

namespace Receptsamlingen.Mvc.Classes
{
    public class Globals
    {
        // Mailsettings
        public static readonly string MailSubjectString = ConfigKey("MailSubject", "Ansökan om konto");
        public static readonly string MailRecieverString = ConfigKey("MailReceiver", "ansok@liljecrantz.nu");
        public static readonly string MailServerString = ConfigKey("MailServer", "mail.liljecrantz.nu");

        // Other strings
        public static string MainCourseString = "Huvudrätt";
        public static string AppetizerString = "Förrätt";
        public static string HeaderString = "Lägg till recept";
        public static string HeaderEditString = "Redigera";

        // Propertystrings
        public static string NameProperty = "Name";
        public static string IDProperty = "ID";

        // Error- and infomessages
        public static string ERROR_SAVING_VOTE = "Det gick inte att rösta, försök igen senare";
        public static string ERROR_USER_HAS_VOTED = "Du har redan röstat en gång";
        public static string ERROR_DELETING_RECIPE = "Det gick inte att radera receptet.";
        public static string ERROR_UPDATING_RECIPE = "Det gick inte att uppdatera receptet.";
        public static string ERROR_SAVING_RECIPE = "Det gick inte att spara receptet, försök igen senare.";
        public static string ERROR_VALIDATING_RECIPE = "Du måste skriva ett namn på receptet och välja kategori.";
        public static string INFO_VOTE_SAVED = "Tack, din röst är sparad";
        public static string INFO_RECIPE_UPDATED = "Tack, receptet är uppdaterat.";
        public static string INFO_RECIPE_SAVED = "Tack, receptet är sparat.";

        // URLs
        public static string DEFAULT_URL = "~/start";

        #region Internal Functions

        internal static string ConfigKey(string key, string defaultvalue)
        {
            var value = ConfigurationManager.AppSettings[key];
            return String.IsNullOrEmpty(value) ? defaultvalue : value;
        }

        #endregion
    }
}