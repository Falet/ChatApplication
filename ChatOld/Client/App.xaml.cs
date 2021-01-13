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
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IClientInfo, ClientInfo>();
            containerRegistry.RegisterSingleton<ITransportClient, WsClient>();
            containerRegistry.RegisterSingleton<IHandlerResponseFromServer, HandlerResponseFromServer>();
            containerRegistry.RegisterSingleton<IHandlerConnection, HandlerConnection>();
            containerRegistry.RegisterSingleton<IHandlerMessages, HandlerMessages>();
            containerRegistry.RegisterSingleton<IHandlerChats, HandlerChats>();
            //containerRegistry.Register<ControlNavigationChatsViewModel>();
            //containerRegistry.Register<ChatViewModel>();
            //containerRegistry.Register<ClientsAtChatViewModel>();
            containerRegistry.Register<LoginMenuViewModel>();
            //containerRegistry.Register<CreateChatViewModel>();
            //containerRegistry.Register<EventLogViewModel>();
            containerRegistry.Register<ControlChatMenuViewModel>();
            //containerRegistry.Register<AllClientViewModel>(); 
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            //BindViewModelToView<ControlNavigationChatsViewModel, ControlNavigationChats>();
            BindViewModelToView<LoginMenuViewModel, LoginMenu>();
            //BindViewModelToView<ChatViewModel, Chat>();
            BindViewModelToView<ControlChatMenuViewModel, ControlChatMenu>();
            //BindViewModelToView<AllClientViewModel, AllClient>();
            //BindViewModelToView<CreateChatViewModel, CreateChat>();
            //BindViewModelToView<ClientsAtChatViewModel, ClientsAtChat>();
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
