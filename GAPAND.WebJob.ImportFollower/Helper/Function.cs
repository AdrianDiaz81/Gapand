using GAPAND.WebJob.ImportFollower.Model;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Interfaces;

namespace GAPAND.WebJob.ImportFollower.Helper
{
   public static class Function
    {
    
       /// <summary>
       /// Obtiene los Seguidores 
       /// </summary>
       /// <returns></returns>
       public static IEnumerable<IUser> GetFollowers()
       {
           var accessToken = "";
           var accessTokenSecret = "";
           var consumerKey = "";
           var consumerSecret = "";

           
           var creds = TwitterCredentials.CreateCredentials(accessToken, accessTokenSecret, consumerKey, consumerSecret);
           TwitterCredentials.SetCredentials(creds);
           
           var loggedUser = User.GetLoggedUser(creds);
           var followers = loggedUser.GetFollowers(loggedUser.FollowersCount);
           return followers;
       }

  
    }
}
