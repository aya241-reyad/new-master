using Firebase.Database;
using Firebase.Database.Query;
using Miar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Miar.Manager
{
    public class MessageManger
    {
        public FirebaseClient FirebaseClient { get; }

        public MessageManger(string FirebaseClient, string Token)
        {
            this.FirebaseClient = new FirebaseClient(FirebaseClient,
               new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(Token) });
        }
        public MessageManger(string FirebaseClient)
        {
            this.FirebaseClient = new FirebaseClient(FirebaseClient);
        }

        public async Task AddMessage(Messages Message)
        {
            try
            {
                await FirebaseClient
                 .Child("Messages")
                 .PostAsync(Message);
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); }


        }

        public async Task<List<Messages>> GetAllMessages()
        {
            try
            {

                var GetPatients = await FirebaseClient
                 .Child("Messages")
                 .OrderByKey()
                 .OnceAsync<Messages>();

                return (GetPatients.Select(a => new Messages
                {
                    Message = a.Object.Message,
                    Name = a.Object.Name,
                    Key = a.Key,
                    Tele = a.Object.Tele,
                    EnterDate  = a.Object.EnterDate

                }).Reverse().ToList());
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); return null; }


        }

        public async Task<Messages> GetOneMessages(string key)
        {
            try
            {
                var GetMessages = await FirebaseClient
                 .Child("Messages")
                 .Child(key)
                 .OrderByKey()
                 .OnceSingleAsync<Messages>();
                return GetMessages;
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); return null; }




        }

        public async Task Delete(string Key)
        {
            try
            {
                await FirebaseClient
                .Child("Messages")
                .Child(Key)
                .DeleteAsync();
            }
            catch (Exception e) { new LogWriter(e.Message + " /n  " + e.Source); }



        }

    }
}
