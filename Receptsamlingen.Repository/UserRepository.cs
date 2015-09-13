using System.Configuration;
using System.Linq;

namespace Receptsamlingen.Repository
{
	public class UserRepository
	{
		private const string ConnectionString = "connectionString";

		#region Singleton

		private static UserRepository _instance;

		private UserRepository() { }

		public static UserRepository Instance
		{
			get { return _instance ?? (_instance = new UserRepository()); }
		}

		#endregion

		public User Get(string username, string password)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return context.Users.Where(x => x.Username == username && x.Password == password).ToList().FirstOrDefault();
			}
		}
	}
}