using System.Configuration;
using System.Linq;
using Receptsamlingen.Repository.Interfaces;
using System;
using Logger;

namespace Receptsamlingen.Repository
{
	public class UserRepository : BaseRepository, IUserRepository
	{
		public User Get(string username, string password)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return context.Users.Where(x => x.Username == username && x.Password == password).ToList().FirstOrDefault();
			}
		}

        public bool Create(string userName, string password, string fullName, string emailAddress)
        {
            var result = false;
            try
            {
                using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
                {
                    var user = new User
                    {
                        Username = userName,
                        Password = password,
                        FullName = fullName,
                        Email = emailAddress,
                        Role = 1
                    };
                    context.Users.InsertOnSubmit(user);
                    context.SubmitChanges();
                    result = true;
                }
            }
            catch
            {
                LogHandler.Log(LogType.Error, string.Format("Could not create user {0} with e-mail {1}", userName, emailAddress));
            }
            return result;
        }
	}
}