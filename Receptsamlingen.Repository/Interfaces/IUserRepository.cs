namespace Receptsamlingen.Repository.Interfaces
{
	public interface IUserRepository
	{
		User Get(string username, string password);
        bool Create(string username, string password, string fullname, string email)
    }
}