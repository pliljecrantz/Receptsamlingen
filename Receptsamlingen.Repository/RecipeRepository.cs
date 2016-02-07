using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Receptsamlingen.Repository.Interfaces;

namespace Receptsamlingen.Repository
{
	public class RecipeRepository : BaseRepository, IRecipeRepository
	{

		#region Interface members

		public IList<Recipe> GetLatest()
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var query = (from w in context.Recipes
							 orderby w.Date descending
							 select w).Take(15);

				return query.ToList();
			}
		}

		public Recipe GetById(int id)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return context.Recipes.Where(x => x.Id == id).ToList().FirstOrDefault();
			}
		}

		public IList<Recipe> GetAll()
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return (from r in context.Recipes select r).ToList();
			}
		}

		public IList<DishType> GetAllDishTypes()
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return (from d in context.DishTypes select d).ToList();
			}
		}

		public string GetDishTypeById(int id)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return context.DishTypes.Where(x => x.Id == id).Select(x => x.Name).ToList().FirstOrDefault();
			}
		}

		public IList<Category> GetAllCategories()
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return (from c in context.Categories select c).ToList();
			}
		}

		public string GetCategoryById(int id)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return context.Categories.Where(x => x.Id == id).Select(x => x.Name).ToList().FirstOrDefault();
			}
		}

		public IList<Special> GetAllSpecials()
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return (from s in context.Specials select s).ToList();
			}
		}

		public IList<SpecialAssign> GetSpecialsForRecipe(string guid)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return (from s in context.SpecialAssigns where s.RecipeGuid == guid select s).ToList();
			}
		}

		public IList<int> GetAllIds()
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return (from r in context.Recipes select r.Id).ToList();
			}
		}

		public bool Save(Recipe recipe)
		{
			var result = false;
			try
			{
				using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
				{
					if (recipe.Id == 0)
					{
						var newRecipe = new Recipe
						{
							Name = recipe.Name,
							Ingredients = recipe.Ingredients,
							Description = recipe.Description,
							Portions = recipe.Portions,
							CategoryId = recipe.CategoryId,
							DishTypeId = recipe.DishTypeId,
							Date = DateTime.Now,
							Guid = recipe.Guid
						};
						context.Recipes.InsertOnSubmit(newRecipe);
					}
					else
					{
						var r = (from c in context.Recipes where c.Id == recipe.Id select c).FirstOrDefault();
						r.Name = recipe.Name;
						r.Ingredients = recipe.Ingredients;
						r.Description = recipe.Description;
						r.Portions = recipe.Portions;
						r.CategoryId = recipe.CategoryId;
						r.DishTypeId = recipe.DishTypeId;
						r.Date = DateTime.Now;
						r.Guid = recipe.Guid;
						r.Id = recipe.Id;
					}
					context.SubmitChanges();
					result = true;
				}
			}
			catch (Exception ex)
			{
				// TODO: log error
			}
			return result;
		}

		public bool SaveSpecial(string guid, int specialId, bool isUpdate = false)
		{
			var result = false;
			try
			{
				using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
				{
					if (isUpdate)
					{
						var assigned = (from c in context.SpecialAssigns where c.RecipeGuid == guid select c).FirstOrDefault();
						if (assigned != null)
						{
							assigned.SpecialId = specialId;
							assigned.RecipeGuid = guid;
						}
					}
					else
					{
						var assign = new SpecialAssign
						{
							SpecialId = specialId,
							RecipeGuid = guid
						};
						context.SpecialAssigns.InsertOnSubmit(assign);
					}
					context.SubmitChanges();
					result = true;
				}
			}
			catch (Exception ex)
			{
				// TODO: log error
			}
			return result;
		}

		public bool Delete(string guid)
		{
			var result = false;
			try
			{
				using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
				{
					var q = context.Recipes.Where(x => x.Guid == guid).ToList().FirstOrDefault();
					if (q != null)
					{
						context.Recipes.DeleteOnSubmit(q);
						context.SubmitChanges();
						result = true;
					}
				}
			}
			catch(Exception ex)
			{
				// TODO: log error
			}
			return result;
		}

		public bool DeleteSpecials(string guid)
		{
			var result = false;
			try
			{
				using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
				{
					var query = context.SpecialAssigns.FirstOrDefault(x => x.RecipeGuid == guid);
					if (query != null)
					{
						context.SpecialAssigns.DeleteOnSubmit(query);
						context.SubmitChanges();
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				// TODO: log error
			}
			return result;
		}

		public IList<Recipe> GetToplist()
		{
			var toplist = new List<Recipe>();
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var query = context.Votes.GroupBy(x => x.RecipeGuid)
										 .Select(x => new { Total = x.Sum(z => z.Rating), Id = x.Key })
										 .OrderByDescending(y => y.Total).Take(5).ToList();

				toplist.AddRange(query.Select(q => GetByGuid(q.Id)));
			}
			return toplist;
		}

		public IList<Recipe> Search(string text)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return context.Recipes.Where(r => r.Name.Contains(text)).OrderBy(r => r.Date).ToList();
			}
		}

		public IList<Recipe> Search(string query, int categoryId, int dishTypeId, IList<Special> specials)
		{
			var recipeList = new List<Recipe>();
			var recipesFilteredForSpecials = new List<Recipe>();
			bool searchQuery = false, searchCategory = false, searchDishType = false;
			if (!string.IsNullOrWhiteSpace(query))
			{
				searchQuery = true;
			}
			if (categoryId != 0)
			{
				searchCategory = true;
			}
			if (dishTypeId != 0)
			{
				searchDishType = true;
			}

			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				if (searchQuery && searchCategory && searchDishType)
				{
					recipeList = context.Recipes.Where(r => r.Name.Contains(query) && r.CategoryId.Equals(categoryId) && r.DishTypeId.Equals(dishTypeId))
												.OrderBy(r => r.Date)
												.ToList();
				}
				else if (searchQuery && searchCategory)
				{
					recipeList = context.Recipes.Where(r => r.Name.Contains(query) && r.CategoryId.Equals(categoryId))
												.OrderBy(r => r.Date)
												.ToList();
				}
				else if (searchQuery && searchDishType)
				{
					recipeList = context.Recipes.Where(r => r.Name.Contains(query) && r.DishTypeId.Equals(dishTypeId))
												.OrderBy(r => r.Date)
												.ToList();
				}
				else if (searchCategory && searchDishType)
				{
					recipeList = context.Recipes.Where(r => r.CategoryId.Equals(categoryId) && r.DishTypeId.Equals(dishTypeId))
												.OrderBy(r => r.Date)
												.ToList();
				}
				else if (searchCategory)
				{
					recipeList = context.Recipes.Where(r => r.CategoryId.Equals(categoryId))
												.OrderBy(r => r.Date)
												.ToList();
				}
				else if (searchDishType)
				{
					recipeList = context.Recipes.Where(r => r.DishTypeId.Equals(dishTypeId))
												.OrderBy(r => r.Date)
												.ToList();
				}
				else if (searchQuery)
				{
					recipeList = context.Recipes.Where(r => r.Name.Contains(query)).OrderBy(r => r.Date).ToList();
				}

				if (specials.Count > 0)
				{
					if (recipeList.Count > 0)
					{
						recipesFilteredForSpecials = (from recipeCopy in recipeList
													  from special in specials
													  let specialCopy = special
													  select (from sa in context.SpecialAssigns
															  where sa.RecipeGuid == recipeCopy.Guid && sa.SpecialId == specialCopy.Id
															  select sa.RecipeGuid).FirstOrDefault()
															  into r
													  select (from rr in recipeList where rr.Guid == r select rr)).Cast<Recipe>().ToList();
					}
					else
					{
						foreach (var special in specials)
						{
							recipesFilteredForSpecials = (from r in context.Recipes
														  join s in context.SpecialAssigns on r.Guid equals s.RecipeGuid
														  where s.SpecialId == special.Id
														  select r).ToList();
						}
					}
				}

				return recipesFilteredForSpecials.Count > 0 ? recipesFilteredForSpecials : recipeList;
			}
		}

		#endregion

		#region Private methods

		private static Recipe GetByGuid(string guid)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return context.Recipes.Where(x => x.Guid == guid).ToList().FirstOrDefault();
			}
		}

		#endregion

	}
}