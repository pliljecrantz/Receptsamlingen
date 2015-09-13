using System;
using System.Configuration;
using System.Linq;

namespace Receptsamlingen.Repository
{
	public class RatingRepository
	{
		#region Singleton

		private const string ConnectionString = "connectionString";
		private static RatingRepository _instance;

		private RatingRepository() { }

		public static RatingRepository Instance
		{
			get { return _instance ?? (_instance = new RatingRepository()); }
		}

		#endregion

		public bool UserHasVoted(string username, string guid)
		{
			var userHasVoted = false;

			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var query = context.Votes.FirstOrDefault(x => x.Username == username && x.RecipeGuid == guid);

				if (query != null)
				{
					userHasVoted = true;
				}
			}

			return userHasVoted;
		}

		public int GetAvarage(string guid)
		{
			var avarageRating = 0;
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var query = (from v in context.Votes
							 join r in context.Recipes on v.RecipeGuid equals r.Guid
							 where r.Guid == guid
							 select v.Rating).Average();
				if (query.HasValue)
				{
					avarageRating = Convert.ToInt32(query.Value);
				}
			}
			return avarageRating;
		}

		public bool Save(string guid, string username, int rating)
		{
			bool voteSaved;
			try
			{
				using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
				{
					var newVote = new Vote
					{
						Username = username,
						Rating = rating,
						RecipeGuid = guid
					};
					context.Votes.InsertOnSubmit(newVote);
					context.SubmitChanges();
					voteSaved = true;
				}
			}
			catch (Exception)
			{
				voteSaved = false;
			}
			return voteSaved;
		}

		public void Delete(string guid)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var query = context.Votes.FirstOrDefault(x => x.RecipeGuid == guid);
				if (query != null)
				{
					context.Votes.DeleteOnSubmit(query);
					context.SubmitChanges();
				}
			}
		}
	}
}