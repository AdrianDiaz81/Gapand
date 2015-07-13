using GAPAND.WebJob.PredictionAge.Model;
using Microsoft.ProjectOxford.Face;
using Microsoft.SharePoint.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GAPAND.WebJob.PredictionAge.Helper
{
   public static class Function
    {
       /// <summary>
       /// Detecta las Caras que hay en una foto y muestra la edad estimada y el sexo
       /// </summary>
       /// <param name="url">Url donde esta la imagén</param>
       /// <returns></returns>
       public static async Task<ResultFace> DetecFacesAndDisplayResult(string url)
       {
           var subscriptionKey = "";
           try
           {
               ResultFace result = new ResultFace();
               var client = new FaceServiceClient(subscriptionKey);
               
               var faces =await  client.DetectAsync(url, false, true, true);
               Console.WriteLine(" > " + faces.Length + " detected.");
               foreach (var face in faces)
               {
                   Console.WriteLine(" >> age: " + face.Attributes.Age + " gender:" + face.Attributes.Gender);
                   result.Age = face.Attributes.Age;
                   result.Gender = face.Attributes.Gender;
               }

               return result;
           }
           catch (Exception exception)
           {
               return   new ResultFace();;
               Console.WriteLine(exception.ToString());
           }

       }
 

       public static async void  DetecFacesAndDisplayResult(string url, string id )
       {
           var subscriptionKey = "";
           try
           {
               
      
               var client = new FaceServiceClient(subscriptionKey);

               var faces = await client.DetectAsync(url, false, true, true);
               Console.WriteLine(" > " + faces.Length + " detected.");
               if (faces.Length == 0) UpdateSharePoint(id, "0", "Sin identificar");
               foreach (var face in faces)
               {
                   Console.WriteLine( " >> age: " + face.Attributes.Age + " gender:" + face.Attributes.Gender);
                   
                   UpdateSharePoint(id, face.Attributes.Age.ToString(), face.Attributes.Gender);
               }

           }
           catch (Exception exception)
           {
               UpdateSharePoint(id, "0", "Sin identificar");
               Console.WriteLine(exception.ToString());
           }
       }

       public static void UpdateSharePoint(string id, string age, string gender)
       {
           using (var context = new ClientContext(Constants.URL))
           {
               context.Credentials = new SharePointOnlineCredentials(Constants.User, Program.GetPassSecure(Constants.Pass));
               // Assume that the web has a list named "Announcements". 
               List twitterList = context.Web.Lists.GetByTitle(Constants.ListTwitter);
               ListItem listItem = twitterList.GetItemById(id);

               listItem["Edad"] = age;
               listItem["Sexo"] = gender;
               listItem.Update();

               context.ExecuteQuery();  
               Console.WriteLine("Actualizado");
               Microsoft.VisualBasic.Devices.Keyboard keyboard = new Microsoft.VisualBasic.Devices.Keyboard();
               Environment.Exit(0);
           }
       }
       
    }
}
