using Firebase.Database;
using Firebase.Database.Query;
using Miar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Miar.Manager
{
    public class RealStateManager
    {
        public FirebaseClient FirebaseClient { get; }

        public RealStateManager(string FirebaseClient, string Token)
        {
            this.FirebaseClient = new FirebaseClient(FirebaseClient,
               new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(Token) });
        }
        public RealStateManager(string FirebaseClient)
        {
            this.FirebaseClient = new FirebaseClient(FirebaseClient);
        }

        public async Task AddState(RealState State)
        {
            try
            {
                await FirebaseClient
                 .Child("States")
                 .PostAsync(State);
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); }


        }

        public async Task EditState(RealState State,string Key)
        {
            try
            {
                await FirebaseClient
                 .Child("States")
                 .Child(Key)
                 .PutAsync(State);
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); }


        }

        public async Task<List<RealState>> GetAllStates()
        {
            try
            {

                var GetPatients = await FirebaseClient
                 .Child("States")
                 .OrderByKey()
                 .OnceAsync<RealState>();

                return (GetPatients.Select(a => new RealState
                {
                    Discription = a.Object.Discription,
                    ImgName = a.Object.ImgName,
                    Key = a.Key,
                    Title = a.Object.Title,
                  
                }).Reverse().ToList());
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); return null; }


        }


        public async Task<RealState> GetOneState(string key)
        {
            try
            {
                var GetMessages = await FirebaseClient
                 .Child("States")
                 .Child(key)
                 .OrderByKey()
                 .OnceSingleAsync<RealState>();
                return GetMessages;
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); return null; }




        }

        public async Task Delete(string Key)
        {
            try
            {
                await FirebaseClient
                .Child("States")
                .Child(Key)
                .DeleteAsync();
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); }



        }


    }
}
