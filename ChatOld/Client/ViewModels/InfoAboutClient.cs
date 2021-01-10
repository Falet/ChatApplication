﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ViewModels
{
    public class InfoAboutClient : BindableBase
    {
        private bool _isSelectedClient;
        public string NameClient { get; }
        public bool ActivityClient { get; set; }
        public bool IsSelectedClient
        {
            get => _isSelectedClient;
            set => SetProperty(ref _isSelectedClient, value);
        }
        public InfoAboutClient(string nameClient, bool activityClient)
        {
            IsSelectedClient = false;

            NameClient = nameClient;
            ActivityClient = activityClient;
        }

    }
}