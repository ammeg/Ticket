using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Helpers
{
    public static  class TicketHelpers
    {
        public static string Script(this UrlHelper instance, string contentPath)
        {
            var random = new Random().Next();

            return instance.Content(contentPath + "?" + random.ToString());
        }
    }
}