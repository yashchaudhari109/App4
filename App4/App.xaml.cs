using App4.Services;
using App4.Views;
using Firebase.Database;
using Firebase.Database.Query;
using Plugin.FirebasePushNotification;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4
{
    public partial class App : Application
    {

        public App()
        {
            FirebaseClient firebaseClient = new FirebaseClient("Paste Database URL from firebase real time database"); 
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new registration();

            CrossFirebasePushNotification.Current.RegisterForPushNotifications();

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>{
                System.Diagnostics.Debug.Print($"TOKEN : {p.Token}");

                var data=new Tokenmodel();
                data.UserTokens = p.Token;
                data.Phone_no = "9000000000";
                data.Name = "Lankesh";
                firebaseClient.Child("UserTokens").PostAsync(data);

            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }


            };
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

    public class Tokenmodel {
        public string UserTokens { get; set; }
        public string Phone_no { get; set; }

        public string Name { get; set; }
    }
}
