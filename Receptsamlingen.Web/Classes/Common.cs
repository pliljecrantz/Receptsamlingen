using System;
using System.Web;
using System.Net.Mail;

namespace Receptsamlingen.Web.Classes
{
    public static class Common
    {
        public static void Logout()
        {
			SessionHandler.User = null;
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

		public static void ClearRecipeSessions()
		{
			SessionHandler.CurrentId = 0;
			SessionHandler.CurrentGuid = null;
		}
    }
}