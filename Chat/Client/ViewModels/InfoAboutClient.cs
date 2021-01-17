namespace Client.ViewModels
{
    using Prism.Mvvm;

    public class InfoAboutClient : BindableBase
    {
        #region Fields

        private bool _isSelectedClient;
        private string _activityClient;

        #endregion Fields

        #region Properties

        public string NameClient { get; }

        public string ActivityClientChanged
        {
            get => _activityClient;
            set => SetProperty(ref _activityClient, value);
        }
        public bool IsSelectedClient
        {
            get => _isSelectedClient;
            set => SetProperty(ref _isSelectedClient, value);
        }

        #endregion Properties

        #region Constructors

        public InfoAboutClient(string nameClient, string activityClient)
        {
            IsSelectedClient = false;

            NameClient = nameClient;
            ActivityClientChanged = activityClient;
        }

        #endregion Constructors
    }
}
