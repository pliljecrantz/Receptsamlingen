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
        public const string ForceReloadSessionString = "ForceReload";

        // Mailsettings
        public static readonly string MailSubjectString = GetConfigKey("MailSubject", "Konto skapat - receptsamlingen.net");
        public static readonly string MailReceiverString = GetConfigKey("MailReceiver", "p.liljecrantz@gmail.com");
        public static readonly string MailSenderString = GetConfigKey("MailSender", "no-reply@receptsamlingen.net");
        public static readonly string MailServerString = GetConfigKey("MailServer", "-");
        public static readonly string MailUserString = GetConfigKey("MailUser", "-");
        public static readonly string MailPasswordString = GetConfigKey("MailPassword", "-");

        // Other strings
        public const string MainCourseString = "Huvudrätt";
        public const string AppetizerString = "Förrätt";
        public const string HeaderString = "Lägg till recept";
        public const string HeaderEditString = "Redigera";
	    public const string UserSessionKeyString = "User";

        // Error- and infomessages
        public const string ErrorSavingVote = "Det gick inte att rösta, försök igen senare.";
        public const string ErrorUserHasVoted = "Du har redan röstat en gång tidigare.";
        public const string ErrorDeletingRecipe = "Det gick inte att radera receptet.";
        public const string ErrorUpdatingRecipe = "Det gick inte att uppdatera receptet.";
        public const string ErrorSavingRecipe = "Det gick inte att spara receptet, försök igen senare.";
        public const string ErrorValidatingRecipe = "Du måste ange ett namn på receptet och välja kategori.";
        public const string InfoVoteSaved = "Tack, din röst är sparad.";
        public const string InfoRecipeSaved = "Tack, receptet är sparat.";
	    public const string InfoRecipeDeleted = "Receptet är raderat.";
	    public const string NoSearchResults = "Sökningen gav inga resultat.";
		public const string ErrorOnlyOneBoxCanBeSelected = "Du kan bara välja en av rutorna.";
		public const string ErrorMustChooseBox = "Du måste välja en av rutorna.";
		public const string ErrorNoAccountGranted = "Du får inget konto.";
		public const string ErrorInvalidEmail = "Ogiltig e-postadress.";
	    public const string ErrorMustGiveInfo = "Du måste ange e-postadress, namn och önskat användarnamn.";
	    public const string InfoApplyApproved = "Tack, ditt konto är skapat.";
		public const string ErrorLogin = "Fel användaruppgifter.";
        public const string ErrorCreatingAccount = "Det gick inte att skapa kontot. Försök igen senare eller prova med ett annat användarnamn.";

        // URLs
        public const string DefaultUrl = "~/start";

        #region Internal Functions

        internal static string GetConfigKey(string key, string defaultvalue)
        {
            var value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(value) ? defaultvalue : value;
        }

        #endregion
    }
}