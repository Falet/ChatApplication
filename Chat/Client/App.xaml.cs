namespace Client
{
    using System.Windows;
    using Client.Model;
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
        #region Methods

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IClientInfo, ClientInfo>();
            containerRegistry.RegisterSingleton<ITransportClient, WsClient>();
            containerRegistry.RegisterSingleton<IHandlerResponseFromServer, HandlerResponseFromServer>();
            containerRegistry.RegisterSingleton<IHandlerConnection, HandlerConnection>();
            containerRegistry.RegisterSingleton<IHandlerMessages, HandlerMessages>();
            containerRegistry.RegisterSingleton<IHandlerChats, HandlerChats>();
            containerRegistry.Register<LoginMenuViewModel>();
            containerRegistry.Register<ControlChatMenuViewModel>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            BindViewModelToView<LoginMenuViewModel, LoginMenu>();
            BindViewModelToView<ControlChatMenuViewModel, ControlChatMenu>();
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

        #endregion Methods
    }
}
