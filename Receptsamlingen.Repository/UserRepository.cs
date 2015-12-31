using System.Configuration;
using System.Linq;
using Receptsamlingen.Repository.Interfaces;

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
	}
}