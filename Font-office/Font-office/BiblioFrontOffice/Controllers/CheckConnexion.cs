using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiblioFrontOffice.Controllers
{
    public class CheckConnexion:Controller
    {
        //verification si le parametre de session est null
        public  static async  Task v(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Session.GetString("AdherentId")) )
            {
                context.Response.Redirect("/Adherents/Login");
               await Task.CompletedTask;
                return;
            }
           
        }
    }
}
