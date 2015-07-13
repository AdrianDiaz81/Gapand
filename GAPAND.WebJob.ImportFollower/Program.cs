using GAPAND.WebJob.ImportFollower.Helper;
using GAPAND.WebJob.ImportFollower.Model;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GAPAND.WebJob.ImportFollower
{
    public class Program
    {
        public static List<TwitterFollower> result;
        static void Main(string[] args)
        {
           var followerCollection=  Function.GetFollowers();
           result = new List<TwitterFollower>();
           ClearList();
           foreach (var follower in followerCollection)
           {
               InsertFollower(follower);
              
           }
           
        }

        private static void ClearList()
        {
            try
            {
                Console.WriteLine("Init Eliminación de lista la lista");
            
            using (var context = new ClientContext(Constants.URL))
            {
                context.Credentials = new SharePointOnlineCredentials(Constants.User, GetPassSecure(Constants.Pass));
                // Assume that the web has a list named "Announcements". 
                List twitterList = context.Web.Lists.GetByTitle(Constants.ListTwitter);
                var camelQuery=CamlQuery.CreateAllItemsQuery(1000);
                var followerCollection = twitterList.GetItems(camelQuery);
                context.Load(followerCollection);
                context.ExecuteQuery();
                var count = followerCollection.Count;
                for (var i = 0; i < count;i++ )
                {
                    followerCollection[0].DeleteObject();
                    Console.WriteLine("Eliminando el elemento de la lista " +i);
                }
                context.ExecuteQuery();
                Console.WriteLine("Eliminada la lista");
            }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        private static void InsertFollower(Tweetinvi.Core.Interfaces.IUser follower)
        {
            using (var context = new ClientContext(Constants.URL))
            {
                context.Credentials = new SharePointOnlineCredentials(Constants.User,GetPassSecure(Constants.Pass));
                // Assume that the web has a list named "Announcements". 
                List twitterList = context.Web.Lists.GetByTitle(Constants.ListTwitter);

                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem newItem = twitterList.AddItem(itemCreateInfo);
                newItem[Constants.FieldName] = follower.Name;
                newItem[Constants.FieldDescription] = follower.Description;
                newItem[Constants.FieldPhoto] = follower.ProfileImageFullSizeUrl;
                newItem.Update();

                context.ExecuteQuery();

                Console.WriteLine("Insertado el seguido:" + follower.Name);
            }
        }

        private static SecureString GetPassSecure(string pass)
        {
            var securePassword = new SecureString();
            foreach (char c in pass.ToCharArray())
            {
                securePassword.AppendChar(c);
            }
     
            return securePassword;
        }
  

    }
}
