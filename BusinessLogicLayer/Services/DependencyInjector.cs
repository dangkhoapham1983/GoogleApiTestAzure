﻿using Unity;
using Unity.Lifetime;

namespace BIVALE.BLL.Services
{
	public static class DependencyInjector
	{
		private static readonly UnityContainer UnityContainer = new UnityContainer();
		public static void Register<I, T>() where T : I
		{
			UnityContainer.RegisterType<I, T>(new ContainerControlledLifetimeManager());
		}
		public static void InjectStub<I>(I instance)
		{
			UnityContainer.RegisterInstance(instance, new ContainerControlledLifetimeManager());
		}
		public static T Retrieve<T>()
		{
			return UnityContainer.Resolve<T>();
		}

		public static T Retrieve<T>(string connectionString)
		{
			return UnityContainer.Resolve<T>(connectionString);
		}

		public static T Retrieve<T>(object p)
		{
			return UnityContainer.Resolve<T>();
		}
	}
}
