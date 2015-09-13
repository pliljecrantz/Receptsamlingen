using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Receptsamlingen.Repository
{
	public class RecipeRepository
	{
		private const string ConnectionString = "connectionString";

		#region Singleton

		private static RecipeRepository _instance;

		private RecipeRepository() { }

		public static RecipeRepository Instance
		{
			get { return _instance ?? (_instance = new RecipeRepository()); }
		}

		#endregion

		public IList<Recipe> GetLatest()
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var query = (from w in context.Recipes
							 orderby w.Date descending
							 select w).Take(10);

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

		private static Recipe GetByGuid(string guid)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				return context.Recipes.Where(x => x.Guid == guid).ToList().FirstOrDefault();
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

		public bool Update(Recipe recipe)
		{
			bool result;
			try
			{
				using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
				{
					var oldRecipe = (from r in context.Recipes where r.Id == recipe.Id select r).FirstOrDefault();
					if (oldRecipe != null)
					{
						oldRecipe.Name = recipe.Name;
						oldRecipe.Ingredients = recipe.Ingredients;
						oldRecipe.Description = recipe.Description;
						oldRecipe.Portions = recipe.Portions;
						oldRecipe.CategoryId = recipe.CategoryId;
						oldRecipe.DishTypeId = recipe.DishTypeId;
						oldRecipe.Date = DateTime.Now;
						oldRecipe.Id = recipe.Id;
						oldRecipe.Guid = recipe.Guid;
					}
					context.SubmitChanges();
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public bool Save(Recipe recipe)
		{
			bool result;
			try
			{
				using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
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
					context.SubmitChanges();
					result = true;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public void SaveSpecial(string guid, int specialId)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var assign = new SpecialAssign
						{
							SpecialId = specialId,
							RecipeGuid = guid
						};
				context.SpecialAssigns.InsertOnSubmit(assign);
				context.SubmitChanges();
			}
		}

		public void Delete(string guid)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var q = context.Recipes.Where(x => x.Guid == guid).ToList().FirstOrDefault();
				context.Recipes.DeleteOnSubmit(q);
				context.SubmitChanges();
			}
		}

		public void DeleteSpecials(string guid)
		{
			using (var context = new ReceptsamlingenDataContext(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString))
			{
				var query = context.SpecialAssigns.FirstOrDefault(x => x.RecipeGuid == guid);
				if (query != null)
				{
					context.SpecialAssigns.DeleteOnSubmit(query);
					context.SubmitChanges();
				}
			}
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
				return context.Recipes.Where(r => r.Name.Contains(text) || r.Ingredients.Contains(text) || r.Description.Contains(text))
									  .OrderBy(r => r.Date)
									  .ToList();
			}
		}
	}
}