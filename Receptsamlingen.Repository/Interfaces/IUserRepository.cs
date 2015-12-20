namespace Receptsamlingen.Repository.Interfaces
{
	public interface IUserRepository
	{
		User Get(string username, string password);
	}
}