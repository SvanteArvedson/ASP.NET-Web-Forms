using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MusicLibrary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Booklets", "Nothaften", "~/Pages/BookletPages/BookletsList.aspx");
            routes.MapPageRoute("BookletCreate", "Nothaften/Skapa-nytt-nothafte", "~/Pages/BookletPages/BookletCreate.aspx");
            routes.MapPageRoute("BookletDetails", "Nothaften/{id}", "~/Pages/BookletPages/BookletDetails.aspx");
            routes.MapPageRoute("BookletEdit", "Nothaften/{id}/Redirega", "~/Pages/BookletPages/BookletEdit.aspx");
            routes.MapPageRoute("BookletDelete", "Nothaften/{id}/Radera", "~/Pages/BookletPages/BookletDelete.aspx");
            routes.MapPageRoute("Default", "", "~/Pages/Shared/Start.aspx");
            routes.MapPageRoute("Error", "Fel", "~/Pages/Shared/Error.aspx");
            routes.MapPageRoute("Error404", "Fel404", "~/Pages/Shared/Error404.aspx");
        }
    }
}