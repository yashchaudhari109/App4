using Firebase.Database;
using Firebase.Database.Query;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class registration : ContentPage
    {
        
        public registration()
        {
            InitializeComponent();
        }
        public void OnButtonClicked(object sender, EventArgs args)
        {
            string d = username.Text;
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) => {
                FirebaseClient firebaseClient = new FirebaseClient("https://oxybills-22af4-default-rtdb.firebaseio.com/");
                System.Diagnostics.Debug.Print($"TOKEN : {p.Token}");
                var data = new Tokenmodel();
                data.UserTokens = p.Token;
                data.Phone_no = "9512345678";
                data.Name = d;
                firebaseClient.Child("UserTokens").PostAsync(data);

            };
        }
        public class Tokenmodel
        {
            public string UserTokens { get; set; }
            public string Phone_no { get; set; }

            public string Name { get; set; }
        }

    }
    
}