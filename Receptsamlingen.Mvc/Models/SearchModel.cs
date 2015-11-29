using System.Collections.Generic;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Models
{
	public class SearchModel
	{
		public string Query { get; set; }
		public Recipe Recipe { get; set; }
		public IList<Recipe> SearchResult { get; set; }
		public bool SearchPerformed { get; set; }
	}
}