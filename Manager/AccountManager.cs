using Firebase.Database;
using Miar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Firebase.Auth;

namespace Miar.Manager
{
    public class AccountManager
    {
        public FirebaseClient FirebaseClient { get; }

        public AccountManager(string FirebaseClient)
        {
            this.FirebaseClient = new FirebaseClient(FirebaseClient);
        }

        public AccountManager()
        {
        }
        public async Task<string> Login(Account User)
         {

            try
            {
                var GetUsers = await FirebaseClient
                     .Child("Users")
                     .OnceAsync<Account>();
                var Auth = GetUsers.ToList().FirstOrDefault(a => a.Object.Email == User.Email && a.Object.Password == User.Password);

                if (Auth != null)
                {
                    string UID = await SignInFirebase();
                    return (UID);

                }
                else
                {
                    return ("NotFound");

                }

            }
            catch (Exception e)
            {
                new LogWriter(e.Message+" /n  "+e.Source);
                return ("NotFound");
            }

        }

        public async Task AddPatient()
        {

            //هنا ابقى اعمل كلاينت جديد ب AuthTokenAsyncFactory
            Account s = new Account {Email="User@gmail.com",Password="123456" };
            await FirebaseClient
             .Child("Users")
             .PostAsync(s);


        }

        public async Task<string> SignInFirebase() {

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyAzT0Q3dU863CDa_OdERUV4MmmL9Ytqr4k")); //Original
                var auth = await authProvider.SignInWithEmailAndPasswordAsync("admin@gmail.com", "123456");
                return auth.FirebaseToken;
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); return null; }
        }
    }
}

