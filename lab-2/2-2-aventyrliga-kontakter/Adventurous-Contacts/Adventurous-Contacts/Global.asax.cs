using System;
using System.Web.UI;

namespace Adventurous_Contacts
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var jQuery = new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-2.1.0.min.js",
                DebugPath = "~/Scripts/jquery-2.1.0.js",
                CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-2.1.0.min.js",
                CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-2.1.0.js"
            };

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jQuery);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}