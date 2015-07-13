using Microsoft.ProjectOxford.Face;
using Microsoft.SharePoint.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GAPAND.WebJob.ComparationFace.Helper
{
   public static class Function
    {

 

       public static async void  DetecFacesAndDisplayResult(string url, string urlComparation, string id ,string campo, int value )
       {
           var subscriptionKey = "idSupscription";
           try
           {
               
      
               var client = new FaceServiceClient(subscriptionKey);

               var faces1 = await client.DetectAsync(url, false, true, true);
    
       var faces2 = await client.DetectAsync(urlComparation, false, true, true);
     
              
               if (faces1 == null || faces2 == null)
               {
                   UpdateSharePoint(id, 0,campo,value);
                  
               }
               if (faces1.Count() == 0 || faces2.Count() == 0)
               {
                  UpdateSharePoint(id, 0,campo,value); }
               if (faces1.Count() > 1 || faces2.Count() > 1)
               {
                   UpdateSharePoint(id, 0,campo,value);
               }
               var res = await client.VerifyAsync(faces1[0].FaceId, faces2[0].FaceId);
               double score = 0;
               if (res.IsIdentical)
                   score = 100;
               else
               {
                   score = Math.Round((res.Confidence / 0.5) * 100);
               }
               UpdateSharePoint(id, score,campo,value);
             

           }
           catch (Exception exception)
           {
               UpdateSharePoint(id, 0,campo,value);
               Console.WriteLine(exception.ToString());
           }
       }

       public static void UpdateSharePoint(string id, double similar,string campo, int value)
       {
           using (var context = new ClientContext(Constants.URL))
           {
               context.Credentials = new SharePointOnlineCredentials(Constants.User, Program.GetPassSecure(Constants.Pass));
               List twitterList = context.Web.Lists.GetByTitle(Constants.ListTwitter);
               ListItem listItem = twitterList.GetItemById(id);

               listItem[campo] = similar;
              
               listItem.Update();

               context.ExecuteQuery();  
               Console.WriteLine("Actualizado");
               if (value == 3) Environment.Exit(0);
              // Microsoft.VisualBasic.Devices.Keyboard keyboard = new Microsoft.VisualBasic.Devices.Keyboard();
              
           }
       }
       
    }
}
