using System.Web.Mvc;
using Ninject;

namespace Receptsamlingen.Mvc.Classes
{
	public static class NinjectHelper
	{
		private static IKernel Kernel { get; set; }

		public static void Inject(this Controller controller)
		{
			Kernel = new StandardKernel(new NinjectBindingModule());
			Kernel.Inject(controller);
		}
	}
}