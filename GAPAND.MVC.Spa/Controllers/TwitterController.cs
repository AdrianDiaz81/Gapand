using GAPAND.MVC.Spa.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace GAPAND.MVC.Spa.Controllers
{
    public class TwitterController : ApiController
    {
        [ResponseType(typeof(IList<TwitterFollower>))]
        public async Task<IHttpActionResult> Get()
        {

            List<TwitterFollower> twitterFollower =  this.GetTwitterFollowerAsync();

            if (twitterFollower == null)
            {
                return this.NotFound();
            }

            return this.Ok(twitterFollower);
        }

        private List<TwitterFollower> GetTwitterFollowerAsync()
        {
            try
            {


                List<TwitterFollower> result = new List<TwitterFollower>();
                using (var context = new ClientContext(Constants.URL))
                {
                    context.Credentials = new SharePointOnlineCredentials(Constants.User, GetPassSecure(Constants.Pass));
                    List twitterList = context.Web.Lists.GetByTitle(Constants.ListTwitter);
                    var camelQuery = CamlQuery.CreateAllItemsQuery(1000);
                    var followerCollection = twitterList.GetItems(camelQuery);
                    context.Load(followerCollection);
                    context.ExecuteQuery();
                    var count = followerCollection.Count;
                    foreach (var item in followerCollection)
                    {
                        try
                        {
                            result.Add(new TwitterFollower
                            {
                                Name = item["Title"].ToString(),
                                Age = Convert.ToDouble(item["Edad"].ToString()),
                                Gender = item["Sexo"].ToString(),
                                UrlImage = item["Foto"].ToString(),
                                Similar= (item["Similar"]==null)?0:Convert.ToDouble(item["Similar"].ToString()),
                                SimilarChuck= (item["SimilarChuck"]==null)?0:Convert.ToDouble(item["SimilarChuck"].ToString()),
                                SimilarCarmen = (item["SimilarCarmen"] == null) ? 0 : Convert.ToDouble(item["SimilarCarmen"].ToString())

                            });
                        }
                        catch (Exception ex) { }

                    }
                    context.ExecuteQuery();


                }
                return result.OrderByDescending(x=>x.Age).ToList();
            }
            catch (Exception exception)
            {
                return null;
            }
        
    }

        public static SecureString GetPassSecure(string pass)
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
