using System.Collections.Generic;
using Ninject;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Repository;
using Receptsamlingen.Repository.Interfaces;

namespace Receptsamlingen.Mvc.Models.Partials
{
	public class ToplistModel
	{
		[Inject]
		public IRecipeRepository RecipeRepository { get; set; }

		public ToplistModel()
		{
			var kernel = new StandardKernel(new NinjectBindingModule());
			kernel.Inject(this);
		}

		private IList<Recipe> _source;
		public IList<Recipe> Source
		{
			get
			{
				return _source ?? (_source = RecipeRepository.GetToplist());
			}
		}
	}
}