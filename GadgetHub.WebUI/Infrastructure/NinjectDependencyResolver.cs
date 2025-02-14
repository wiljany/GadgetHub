using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Description;
using System.Web.Mvc;
using Ninject;
using Moq;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Concrete;

namespace GadgetHub.WebUI.Infrastructure
{

	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel mykernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			mykernel = kernelParam;
			AddBinding();
		}

		public object GetService(Type myserviceType)
		{
			return mykernel.GetService(myserviceType);
		}

		public IEnumerable<object> GetServices(Type myserviceType)
		{
			return mykernel.GetAll(myserviceType);
		}

		private void AddBinding()
		{
			//Mock<IGadgetRepository> myMock = new Mock<IGadgetRepository>();
			//myMock.Setup(m => m.Gadgets).Returns(new List<Gadgets>
			//{
			//	new Gadgets { Name = "IPhone", Brand = "Apple", Price = 150, Description = "This is a phone made by Apple" },
			//	new Gadgets{ Name = "Computer", Brand = "Microsoft", Price = 300, Category = "Electronics" },
			//	new Gadgets{ Name = "Fitbit", Price = 95, Category = "Electronic Watches" }
			//});

			//mykernel.Bind<IGadgetRepository>().ToConstant(myMock.Object);

			mykernel.Bind<IGadgetRepository>().To<EFGadgetRepository>();
		}
	}
}