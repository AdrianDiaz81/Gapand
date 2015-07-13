using GAPAND.WebJob.ComparationFace.Helper;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GAPAND.WebJob.ComparationFace
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var context = new ClientContext(Constants.URL))
            {
                context.Credentials = new SharePointOnlineCredentials(Constants.User, GetPassSecure(Constants.Pass));
                List twitterList = context.Web.Lists.GetByTitle(Constants.ListTwitter);

                var query = new CamlQuery
                {
                    ViewXml = @"<View>
                                <Query>
                               <Where>
                                        <And>
                                        <IsNotNull><FieldRef Name='Sexo' /></IsNotNull>
                                        <IsNull><FieldRef Name='SimilarCarmen' /></IsNull>
                                        </And>
                                    </Where>
                                </Query>
                                <RowLimit>1</RowLimit>
                                </View>"
                };

                var collection = twitterList.GetItems(query);
                context.Load(collection);
                context.ExecuteQuery();

                foreach (var item in collection)
                {
                    var foto=item["Foto"].ToString();                   
                    Function.DetecFacesAndDisplayResult(foto,Constants.UrlComparation, item.Id.ToString(),"SimilarCarmen",1);
                    Function.DetecFacesAndDisplayResult(foto, Constants.UrlComparationFemale, item.Id.ToString(), "Similar",2);
                    Function.DetecFacesAndDisplayResult(foto, Constants.UrlComparationMale, item.Id.ToString(), "SimilarChuck",3);
                    
                }


                Console.ReadLine();
                Console.WriteLine("Actualizado");
            }
        }

        public static SecureString GetPassSecure(string pass)
        {
            var securePassword = new SecureString();
            foreach (char c in pass)
            {
                securePassword.AppendChar(c);
            }

            return securePassword;
        }
    }
}
