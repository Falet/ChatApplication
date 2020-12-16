namespace Client
{
    using System.Windows;
    using Client.ViewModels;
    using Client.Views;
    using Prism.Ioc;
    using Prism.Mvvm;
    using Prism.Unity;

    using Unity;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<ITextMessagesController, TextMessagesController>();
            containerRegistry.Register<ControlNavigationChatsViewModel>();
            containerRegistry.Register<ChatViewModel>();
            containerRegistry.Register<EventLogViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            BindViewModelToView<ControlNavigationChatsViewModel, ControlNavigationChats>();
            BindViewModelToView<ChatViewModel, Chat>();
        }

        protected override Window CreateShell()
        {
            var mainView = Container.Resolve<MainWindow>();
            return mainView;
        }

        private void BindViewModelToView<TViewModel, TView>()
        {
            ViewModelLocationProvider.Register(typeof(TView).ToString(), () => Container.Resolve<TViewModel>());
        }

    }
}
