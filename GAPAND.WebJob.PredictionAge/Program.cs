using GAPAND.WebJob.PredictionAge.Helper;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GAPAND.WebJob.PredictionAge
{
   public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ClientContext(Constants.URL))
            {
                context.Credentials = new SharePointOnlineCredentials(Constants.User, GetPassSecure(Constants.Pass));
                // Assume that the web has a list named "Announcements". 
                List twitterList = context.Web.Lists.GetByTitle(Constants.ListTwitter);
             
                var query= new CamlQuery{
                    ViewXml = @"<View>
                                <Query>
                               <Where><IsNull><FieldRef Name='Sexo' /></IsNull>
                                </Where>
                                </Query>
                                <RowLimit>1</RowLimit>
                                </View>"
                };

               var collection= twitterList.GetItems(query);
                context.Load(collection);
                context.ExecuteQuery();
           
                foreach(var item in collection)
                {
                   Function.DetecFacesAndDisplayResult(item["Foto"].ToString(),item.Id.ToString());
                
                }


                Console.ReadLine();
                Console.WriteLine("Actualizado");
           
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
