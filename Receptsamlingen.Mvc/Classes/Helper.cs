using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace Receptsamlingen.Mvc.Classes
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

		public static void Logout()
		{
			SessionHandler.User = null;
			SessionHandler.IsAuthenticated = false;
			HttpContext.Current.Session.Clear();
			HttpContext.Current.Session.Abandon();
		}

		public static void GenerateMail(string emailaddress, string fullName, string username)
		{
			var message = new MailMessage();
			message.To.Add(new MailAddress(Globals.MailRecieverString));
			message.From = new MailAddress(emailaddress);
			message.Subject = Globals.MailSubjectString;
			message.Body = String.Format("Namn: {0}<br/>E-postadress: {1}<br/>Önskat användarnamn: {2}", fullName, emailaddress, username);
			message.IsBodyHtml = true;
			SendMail(message);
		}

		public static void SendMail(MailMessage message)
		{
			var smtp = new SmtpClient(Globals.MailServerString);
			smtp.Send(message);
		}
    }
}