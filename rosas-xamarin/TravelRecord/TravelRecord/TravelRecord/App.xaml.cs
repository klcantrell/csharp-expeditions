﻿using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using TravelRecord.Helpers;
using TravelRecord.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecord
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public static MobileServiceClient MobileService = new MobileServiceClient(Constants.APP_SERVICE_URL);

        public static IMobileServiceSyncTable<Post> postsTable;

        public static Users user = new Users(); 

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation = databaseLocation;

            var store = new MobileServiceSQLiteStore(databaseLocation);
            store.DefineTable<Post>();

            MobileService.SyncContext.InitializeAsync(store);
            postsTable = MobileService.GetSyncTable<Post>();
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
