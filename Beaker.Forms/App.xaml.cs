using Prism.Unity;
using Beaker.Forms.Views;
using System;
using Prism.Modularity;

namespace Beaker.Forms
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override void OnInitialized()
		{
			InitializeComponent();

			NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterTypeForNavigation<MainPage>();
		}

		protected override void ConfigureModuleCatalog()
		{
			Type commonModule = typeof(Beaker.Module.Common.Module);
			ModuleCatalog.AddModule(
			  new ModuleInfo()
			  {
				  ModuleName = commonModule.Name,
				  ModuleType = commonModule,
			  });
		}
	}
}

