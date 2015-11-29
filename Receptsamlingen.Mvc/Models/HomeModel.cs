using System.Collections.Generic;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Models
{
    public class HomeModel
    {
		public IList<Recipe> Recipes { get; set; }
    }
}