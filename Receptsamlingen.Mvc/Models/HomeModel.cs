using System.Collections.Generic;
using Receptsamlingen.Repository.Domain;

namespace Receptsamlingen.Mvc.Models
{
    public class HomeModel
    {
        public IList<Recipe> Recipes;
    }
}