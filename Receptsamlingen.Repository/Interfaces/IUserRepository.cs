namespace Receptsamlingen.Repository.Interfaces
{
	public interface IUserRepository
	{
		User Get(string username, string password);
        User Get(string username);
        bool Create(string username, string password, string fullname, string email);
    }
}