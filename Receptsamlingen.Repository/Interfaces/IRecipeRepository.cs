using System.Collections.Generic;

namespace Receptsamlingen.Repository.Interfaces
{
	public interface IRecipeRepository
	{
		IList<Recipe> GetLatest();
		Recipe GetById(int id);
		IList<Recipe> GetAll();
		IList<DishType> GetAllDishTypes();
		string GetDishTypeById(int id);
		IList<Category> GetAllCategories();
		string GetCategoryById(int id);
		IList<Special> GetAllSpecials();
		IList<SpecialAssign> GetSpecialsForRecipe(string guid);
		IList<int> GetAllIds();
		bool Save(Recipe recipe);
		bool SaveSpecial(string guid, int specialId);
		bool Delete(string guid);
		bool DeleteSpecials(string guid);
		IList<Recipe> GetToplist();
		IList<Recipe> Search(string text);
		IList<Recipe> Search(string query, int categoryId, int dishTypeId, IList<Special> specials);
	}
}