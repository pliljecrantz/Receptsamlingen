using System.Collections.Generic;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Models.Partials
{
	public class ToplistModel
	{
		private IList<Recipe> _source;
		public IList<Recipe> Source
		{
			get
			{
				return _source ?? (_source = RecipeRepository.Instance.GetToplist());
			}
		}
	}
}