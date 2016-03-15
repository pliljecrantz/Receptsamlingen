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
		public const string CurrentIdString = "CurrentId";
		public const string IdListString = "IdList";
		public const string FailedLoginSessionString = "FailedLogin";

        // Mailsettings
        public static readonly string MailSubjectString = ConfigKey("MailSubject", "Konto skapat - receptsamlingen.net");
        public static readonly string MailSenderString = ConfigKey("MailReceiver", "no-reply@receptsamlingen.net");
        public static readonly string MailServerString = ConfigKey("MailServer", "smtp.gmail.com");

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
		public const string ErrorLogin = "Fel användaruppgifter.";
        public const string ErrorCreatingAccount = "Det gick inte att skapa ditt konto. Försök gärna igen senare.";

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