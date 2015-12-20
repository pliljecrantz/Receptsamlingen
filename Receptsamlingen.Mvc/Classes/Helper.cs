using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;
using Receptsamlingen.Repository.Interfaces;

namespace Receptsamlingen.Mvc.Classes
{
	public interface IHelper
	{
		string CapitalizeFirstLetter(string text);
		bool ValidateEmail(string emailaddress);
		void Logout();
		void GenerateMail(string emailaddress, string fullName, string username);
		void SendMail(MailMessage message);
		IList<Special> GetSelectedSpecials(PostedSpecials postedSpecials);

	}

    public class Helper : IHelper
    {
	    private readonly IRecipeRepository _recipeRepository;

	    public Helper(IRecipeRepository recipeRepository)
	    {
		    _recipeRepository = recipeRepository;
	    }

        public string CapitalizeFirstLetter(string text)
        {
            var textFirstLetter = text.Substring(0, 1);
            var textFirstLetterUpper = textFirstLetter.ToUpper();
            var textRestOfLetters = text.Substring(1, text.Length - 1);
            var result = textFirstLetterUpper + textRestOfLetters;
            return result;
        }

        public bool ValidateEmail(string emailaddress)
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

		public void Logout()
		{
			SessionHandler.User = null;
			SessionHandler.IsAuthenticated = false;
			HttpContext.Current.Session.Clear();
			HttpContext.Current.Session.Abandon();
		}

		public void GenerateMail(string emailaddress, string fullName, string username)
		{
			var message = new MailMessage();
			message.To.Add(new MailAddress(Globals.MailRecieverString));
			message.From = new MailAddress(emailaddress);
			message.Subject = Globals.MailSubjectString;
			message.Body = String.Format("Namn: {0}<br/>E-postadress: {1}<br/>Önskat användarnamn: {2}", fullName, emailaddress, username);
			message.IsBodyHtml = true;
			SendMail(message);
		}

		public void SendMail(MailMessage message)
		{
			var smtp = new SmtpClient(Globals.MailServerString);
			smtp.Send(message);
		}

		public IList<Special> GetSelectedSpecials(PostedSpecials postedSpecials)
		{
			IList<Special> selectedSpecials = new List<Special>();
			var postedSpecialIds = new string[0];

			if (postedSpecials == null)
			{
				postedSpecials = new PostedSpecials();
			}

			if (postedSpecials.Ids != null && postedSpecials.Ids.Any())
			{
				postedSpecialIds = postedSpecials.Ids;
			}

			if (postedSpecialIds.Any())
			{
				selectedSpecials = _recipeRepository.GetAllSpecials().Where(x => postedSpecialIds.Any(s => x.Id.ToString().Equals(s))).ToList();
			}
			return selectedSpecials;
		}
    }
}