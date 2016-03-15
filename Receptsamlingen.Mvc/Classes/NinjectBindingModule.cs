using Ninject.Modules;
using Receptsamlingen.Repository;
using Receptsamlingen.Repository.Interfaces;

namespace Receptsamlingen.Mvc.Classes
{
	public class NinjectBindingModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IRecipeRepository>().To<RecipeRepository>();
			Bind<IRatingRepository>().To<RatingRepository>();
			Bind<IUserRepository>().To<UserRepository>();
		}
	}
}