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
        public const string ErrorSavingVote = "Det gick inte att rösta, försök igen senare.";
        public const string ErrorUserHasVoted = "Du har redan röstat.";
        public const string ErrorDeletingRecipe = "Det gick inte att radera receptet.";
        public const string ErrorUpdatingRecipe = "Det gick inte att uppdatera receptet.";
        public const string ErrorSavingRecipe = "Det gick inte att spara receptet, försök igen senare.";
        public const string ErrorValidatingRecipe = "Du måste ange ett namn på receptet och välja kategori.";
        public const string InfoVoteSaved = "Tack, din röst är sparad.";
        public const string InfoRecipeUpdated = "Tack, receptet är uppdaterat.";
        public const string InfoRecipeSaved = "Tack, receptet är sparat.";
	    public const string InfoRecipeDeleted = "Receptet är raderat.";
	    public const string NoSearchResults = "Sökningen gav inga resultat.";
		public const string ErrorOnlyOneBoxCanBeSelected = "Du kan bara välja en av rutorna.";
		public const string ErrorMustChooseBox = "Du måste välja en av rutorna.";
		public const string ErrorNoAccountGranted = "Du får inget konto.";
		public const string ErrorInvalidEmail = "Ogiltig e-postadress.";
	    public const string ErrorMustGiveInfo = "Du måste ange e-postadress, namn och önskat användarnamn.";
	    public const string InfoApplyApproved = "Tack, din ansökan är skickad. Svar kommer till angiven e-postadress.";

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