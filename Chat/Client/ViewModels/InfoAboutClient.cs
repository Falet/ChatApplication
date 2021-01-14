using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ViewModels
{
    public class InfoAboutClient : BindableBase
    {
        private bool _isSelectedClient;
        private bool _activityClient;
        public string NameClient { get; }
        
        public bool ActivityClientChanged
        {
            get => _activityClient;
            set => SetProperty(ref _activityClient, value);
        }
        public bool IsSelectedClient
        {
            get => _isSelectedClient;
            set => SetProperty(ref _isSelectedClient, value);
        }
        public InfoAboutClient(string nameClient, bool activityClient)
        {
            IsSelectedClient = false;

            NameClient = nameClient;
            ActivityClientChanged = activityClient;
        }

    }
}
