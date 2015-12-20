namespace Receptsamlingen.Repository.Interfaces
{
	public interface IRatingRepository
	{
		bool UserHasVoted(string username, string guid);
		int GetAvarage(string guid);
		bool Save(string guid, string username, int rating);
		void Delete(string guid);
	}
}